using Ardalis.Result;
using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Domain.Products;
using Microsoft.Extensions.Logging;

namespace BakeryShop.Application.Products.GetProductByIdQuery;
internal sealed class GetProductByIdQueryHandler(
    IProductRepository productRepository,
    ILogger<GetProductByIdQueryHandler> logger
    )
    : IQueryHandler<GetProductByIdQuery, ProductDto>
{
    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByIdQuery: Started.");
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product is null)
        {
            logger.LogInformation("GetProductByIdQuery: Failed. Product not found.");
            return Result.NotFound(ProductErrors.NotFound);
        }
            
        ProductDto dto = product;

        logger.LogInformation("GetProductByIdQuery: Success.");

        return dto;
    }
}
