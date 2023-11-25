namespace BeeFat.Domain.Infrastructure;

public class BaseEntity: IEntity
{
    public Guid Id { get; set; }
}