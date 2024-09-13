namespace BakeryShop.Domain.Products;
public interface ICurrencyRepository
{
    Task<Currency?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Currency>> GetAllAsync(CancellationToken cancellationToken = default);
}
