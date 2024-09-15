using Ardalis.Result;
using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Domain.Products;
using Microsoft.Extensions.Logging;

namespace BakeryShop.Application.Staff.Products.CreateProduct;
internal sealed class CreateProductCommandHandler(
    IProductRepository productRepository,
    ILogger<CreateProductCommandHandler> logger)
    : ICommandHandler<CreateProductCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateProductCommand: Started.");

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

        logger.LogInformation("CreateProductCommand: Success.");

        return await productRepository.InsertAsync(product, cancellationToken);
    }
}
