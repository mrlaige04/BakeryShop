using Ardalis.Result;
using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Products;
using BakeryShop.Domain.Products;
using Microsoft.Extensions.Logging;

namespace BakeryShop.Application.Staff.Products.DeleteProduct;
internal sealed class DeleteProductCommandHandler(
    IProductRepository productRepository,
    ILogger<DeleteProductCommandHandler> logger)
    : ICommandHandler<DeleteProductCommand>
{
    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteProductCommand: Started.");

        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product is null)
        {
            logger.LogInformation("DeleteProductCommand: Failed. Product not found.");
            return Result.NotFound(ProductErrors.NotFound);
        }
            
        await productRepository.DeleteAsync(product, cancellationToken);

        logger.LogInformation("DeleteProductCommand: Success.");

        return Result.Success();
    }
}
