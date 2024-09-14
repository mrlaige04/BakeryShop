using System.Linq.Expressions;

namespace BakeryShop.Domain.Products;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    IQueryable<Product> Search(Expression<Func<Product, bool>> predicate);
    IQueryable<Product> Source { get; }

    Task<Guid> InsertAsync(Product product, CancellationToken cancellationToken = default);

    Task DeleteAsync(Product product , CancellationToken cancellationToken = default);

    Task Update(Product product, CancellationToken cancellationToken = default);
}