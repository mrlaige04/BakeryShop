namespace BakeryShop.Domain.Abstractions;

public interface IAuditableEntity
{
    DateTimeOffset CreatedAt { get; }
    DateTimeOffset ModifiedAt { get; }
}