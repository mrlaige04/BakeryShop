namespace BakeryShop.Domain.Abstractions;

public abstract class Entity : IEntity<Guid>, IAuditableEntity
{
    public Guid Id { get; init; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ModifiedAt { get; set; }
}