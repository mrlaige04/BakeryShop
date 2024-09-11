using BakeryShop.Domain.Products;

namespace BakeryShop.Infrastructure.Products;

public class ProductRepository : IProductRepository
{
    public Task<Product?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}