using Ardalis.Result;
using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Domain.Products;

namespace BakeryShop.Application.Products.CreateProduct;
internal sealed class CreateProductCommandHandler(IProductRepository productRepository, ICurrencyRepository currencyRepository)
    : ICommandHandler<CreateProductCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var currency = await currencyRepository.GetByIdAsync(request.CurrencyId, cancellationToken);
        if (currency is null)
            return Result.Error("Currency not found.");

        var product = Product.Create(
            request.Title, 
            request.Price, 
            request.Description, 
            currency, 
            request.Quantity, 
            request.QuantityType);

        return await productRepository.InsertAsync(product, cancellationToken);
    }
}
