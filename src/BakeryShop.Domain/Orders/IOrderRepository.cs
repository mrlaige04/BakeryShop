namespace BakeryShop.Domain.Orders;

public interface IOrderRepository
{
    IQueryable<Order> Source { get; }

    Task<Order?> GetById(Guid id, CancellationToken cancellationToken = default);

    Task Update(Order order, CancellationToken cancellationToken);
}