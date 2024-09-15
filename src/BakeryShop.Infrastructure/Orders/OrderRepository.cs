using BakeryShop.Domain.Orders;
using BakeryShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BakeryShop.Infrastructure.Orders;

public class OrderRepository(ApplicationDbContext dbContext) : IOrderRepository
{
    private DbSet<Order> Orders => dbContext.Orders;

    public IQueryable<Order> Source => Orders;

    public async Task Update(Order order, CancellationToken cancellationToken)
    {
        Orders.Update(order);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Order?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await Orders
            .Include(o => o.Items)
                .ThenInclude(i => i.Product)
            .Include(o => o.DeliveryInfo)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }
}