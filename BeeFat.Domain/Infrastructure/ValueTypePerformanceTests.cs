using System.Diagnostics;
using NUnit.Framework;

namespace BeeFat.Domain.Infrastructure;

[TestFixture]
public class ValueTypePerformanceTests
{
    public class PersonNameWithHandcodedHashCode : ValueType<PersonName>
    {
        public PersonNameWithHandcodedHashCode(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public override int GetHashCode()
        {
            return unchecked((FirstName == null ? 0 : FirstName.GetHashCode()) * 16777619 +
                             (LastName == null ? 0 : LastName.GetHashCode()));
        }

        public string FirstName { get; }
        public string LastName { get; }
    }

    [Test]
    public void GetHashCodePerformance()
    {
        // x20 - x40 times difference should be! 
        // But can be optimized to x2 slowdown with Expression Trees and their compilation. 
        // See Expression.Lambda(...).Compile()
        var count = 500000;
        new PersonName("", "").GetHashCode();
        new PersonNameWithHandcodedHashCode("", "").GetHashCode();
        var people1 = Enumerable.Range(1, count)
            .Select(i => new PersonName(new string('f', i % 10), new string('s', i % 10))).ToList();
        var people2 = Enumerable.Range(1, count).Select(i =>
            new PersonNameWithHandcodedHashCode(new string('f', i % 10), new string('s', i % 10))).ToList();
        var sw = Stopwatch.StartNew();
        foreach (var person in people1)
            person.GetHashCode();
        Console.WriteLine("ValueType<T> GetHashCode: " + sw.Elapsed);
        sw.Restart();
        foreach (var person in people2)
            person.GetHashCode();
        Console.WriteLine("Hand coded GetHashCode:   " + sw.Elapsed);
    }
}