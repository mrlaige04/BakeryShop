using Ardalis.Result;
using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Domain.Products;

namespace BakeryShop.Application.Products.GetProductByIdQuery;
internal sealed class GetProductByIdQueryHandler(IProductRepository productRepository)
    : IQueryHandler<GetProductByIdQuery, ProductDto>
{
    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product is null)
            return Result.NotFound(ProductErrors.NotFound);

        ProductDto dto = product;

        return dto;
    }
}
