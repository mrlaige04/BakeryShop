using Ardalis.Result;
using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Domain.Products;

namespace BakeryShop.Application.Staff.Products.CreateProduct;
internal sealed class CreateProductCommandHandler(IProductRepository productRepository)
    : ICommandHandler<CreateProductCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Product.Create(
            request.Title,
            request.Price,
            request.Description,
            request.Quantity,
            request.QuantityType);

        if (request.Information is not null && request.Information.Count > 0)
        {
            var information = request.Information.Select(i => new Information
            {
                Title = i.Title,
                Description = i.Description,
            });

            foreach (var info in information)
            {
                product.AddInformation(info);
            }
        }

        return await productRepository.InsertAsync(product, cancellationToken);
    }
}
