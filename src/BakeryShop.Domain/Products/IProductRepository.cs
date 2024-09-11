namespace BakeryShop.Domain.Products;

public interface IProductRepository
{
    Task<Product?> GetById(Guid id, CancellationToken cancellationToken = default);
    
}