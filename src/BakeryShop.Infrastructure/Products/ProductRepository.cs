using BakeryShop.Domain.Products;
using BakeryShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BakeryShop.Infrastructure.Products;

public class ProductRepository(ApplicationDbContext dbContext) : IProductRepository
{
    public IQueryable<Product> Source => Products;

    private DbSet<Product> Products => dbContext.Products;

    public async Task DeleteAsync(Product product, CancellationToken cancellationToken = default)
    {
        Products.Remove(product);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Products
            .Include(p => p.Information)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Guid> InsertAsync(Product product, CancellationToken cancellationToken = default)
    {
        await Products.AddAsync(product, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return product.Id;
    }

    public IQueryable<Product> Search(Expression<Func<Product, bool>> predicate)
    {
        return Products.Where(predicate);
    }

    public async Task Update(Product product, CancellationToken cancellationToken = default)
    {
        Products.Update(product);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}