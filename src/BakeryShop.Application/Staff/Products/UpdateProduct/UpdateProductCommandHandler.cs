using Ardalis.Result;
using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Products;
using BakeryShop.Domain.Products;
using Microsoft.Extensions.Logging;

namespace BakeryShop.Application.Staff.Products.UpdateProduct;
internal sealed class UpdateProductCommandHandler(
    IProductRepository productRepository,
    ILogger<UpdateProductCommandHandler> logger)
    : ICommandHandler<UpdateProductCommand, ProductDto>
{
    public async Task<Result<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductCommand: Started.");

        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product is null)
        {
            logger.LogInformation("UpdateProductCommand: Failed. Product not found.");
            return Result.NotFound(ProductErrors.NotFound);
        }
            
        product = UpdateProduct(product, request);
        await productRepository.Update(product, cancellationToken);

        ProductDto dto = product;

        logger.LogInformation("UpdateProductCommand: Success.");

        return dto;
    }

    private static Product UpdateProduct(Product product, UpdateProductCommand request)
    {
        product.Update(request.Title, request.Price, request.Description, request.Quantity, request.QuantityType, request.Information);

        return product;
    }
}
