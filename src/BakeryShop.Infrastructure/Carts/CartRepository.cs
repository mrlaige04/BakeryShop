using BakeryShop.Domain.Products;
using BakeryShop.Domain.Users;
using BakeryShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace BakeryShop.Infrastructure.Carts;
public class CartRepository(ApplicationDbContext dbContext) : ICartRepository
{
    private DbSet<Cart> Carts => dbContext.Carts;

    public async Task AddProductToCart(Cart cart, Product product, double quantity, CancellationToken cancellationToken = default)
    {
        var item = CartItem.Create(product, quantity);
        item.ProductId = product.Id;

        cart.AddItem(item);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Cart?> GetByUserId(Guid userId, CancellationToken cancellationToken = default)
    {
        return await Carts
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.OwnerId == userId, cancellationToken);
    }

    public async Task RemoveProductFromCart(Cart cart, Product product, double quantity, CancellationToken cancellationToken = default)
    {
        var item = CartItem.Create(product, quantity);
        item.ProductId = product.Id;

        cart.RemoveItem(item);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
