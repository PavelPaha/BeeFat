using System.Linq.Expressions;
using System.Reflection;

namespace BeeFat.Domain.Infrastructure;

public class ValueType<T> where T : ValueType<T>
{
    private const int FnvPrime = 16777619;

    private static readonly List<Property> properties;
    private static readonly Func<T, int> hashCodeCalculator;

    static ValueType()
    {
        var propertyInfos = typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .OrderBy(p => p.Name);

        properties = new List<Property>();

        var instanceParameter = Expression.Parameter(typeof(T), "instance");
        Expression calculatorExpression = null!;

        foreach (var propertyInfo in propertyInfos)
        {
            properties.Add(GetProperty(instanceParameter, propertyInfo));
            calculatorExpression = MergeExpressions(instanceParameter, propertyInfo, calculatorExpression);
        }

        hashCodeCalculator = Expression.Lambda<Func<T, int>>(calculatorExpression, instanceParameter).Compile();
    }

    public bool Equals(T other) => Equals((object)other);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;
        if (typeof(T) != obj?.GetType())
            return false;
        var other = (T)obj;
        var thisValues = properties.Select(p => p.Getter((T)this));
        var otherValues = properties.Select(p => p.Getter(other));
        return thisValues.SequenceEqual(otherValues);
    }

    public override int GetHashCode() => hashCodeCalculator((T)this);

    public override string ToString()
    {
        var representations = properties
            .Select(p => $"{p.Name}: {p.Getter((T)this)}")
            .ToArray();
        return $"{typeof(T).Name}({string.Join("; ", representations)})";
    }

    private static Property GetProperty(
        ParameterExpression instanceParameter,
        PropertyInfo propertyInfo)
    {
        var name = propertyInfo.Name;
        var getter = Expression.Lambda<Func<T, object>>(
            Expression.Convert(
                Expression.Call(
                    instanceParameter,
                    propertyInfo.GetMethod!),
                typeof(object)),
            instanceParameter).Compile();
        return new Property(name, getter);
    }

    private static Expression MergeExpressions(
        ParameterExpression instanceParameter,
        PropertyInfo propertyInfo,
        Expression? calculatorExpression) => Expression.ExclusiveOr(
        Expression.Multiply(
            calculatorExpression ?? Expression.Constant(0),
            Expression.Constant(FnvPrime)),
        Expression.Call(
            Expression.Call(
                instanceParameter,
                propertyInfo.GetMethod!),
            typeof(object).GetMethod("GetHashCode")!));

    private record struct Property(string Name, Func<T, object> Getter);
}