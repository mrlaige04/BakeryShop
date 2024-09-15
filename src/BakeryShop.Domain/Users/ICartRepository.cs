using BakeryShop.Domain.Products;

namespace BakeryShop.Domain.Users;
public interface ICartRepository
{
    Task<Cart?> GetByUserId(Guid userId, CancellationToken cancellationToken = default);

    Task AddProductToCart(Cart cart, Product product, double Quantity, CancellationToken cancellationToken = default);
    Task RemoveProductFromCart(Cart cart, Product product, double Quantity, CancellationToken cancellationToken = default);
}
