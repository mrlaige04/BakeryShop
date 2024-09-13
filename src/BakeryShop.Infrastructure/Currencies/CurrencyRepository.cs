using BakeryShop.Domain.Products;
using BakeryShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BakeryShop.Infrastructure.Currencies;
public class CurrencyRepository(ApplicationDbContext dbContext) : ICurrencyRepository
{
    private DbSet<Currency> Currencies => dbContext.Currencies;

    public async Task<IEnumerable<Currency>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await Currencies.ToListAsync(cancellationToken);
    }

    public async Task<Currency?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Currencies.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}
