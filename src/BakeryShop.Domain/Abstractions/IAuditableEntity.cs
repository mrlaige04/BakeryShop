namespace BakeryShop.Domain.Abstractions;

public interface IAuditableEntity
{
    DateTimeOffset CreatedAt { get; set; }
    DateTimeOffset ModifiedAt { get; set; }
}