using System.Diagnostics.CodeAnalysis;

namespace BeeFat.Domain.Infrastructure;

public class Entity<TId> where TId : struct
{
    [SetsRequiredMembers]
    protected Entity(TId id) => Id = id;

    public required TId Id { get; init; }

    protected bool Equals(Entity<TId> other)
    {
        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj))
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        if (obj.GetType() != this.GetType())
            return false;
        return Equals((Entity<TId>)obj);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<TId>.Default.GetHashCode(Id);
    }

    public override string ToString()
    {
        return $"{GetType().Name}({nameof(Id)}: {Id})";
    }
}

public class Entity : Entity<Guid>
{
    [SetsRequiredMembers]
    protected Entity() : base(Guid.NewGuid()) { }
}