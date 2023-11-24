namespace BeeFat.Domain.Infrastructure;

public abstract class Entity
{
    public readonly string Id;

    protected Entity(string id) => Id = id;

    private bool Equals(Entity other)
    {
        return EqualityComparer<string>.Default.Equals(Id, other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        if (obj.GetType() != this.GetType())
            return false;
        return Equals((Entity)obj);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<string>.Default.GetHashCode(Id);
    }

    public override string ToString()
    {
        return $"{GetType().Name}({nameof(Id)}: {Id})";
    }
}