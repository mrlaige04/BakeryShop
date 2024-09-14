using Ardalis.Result;
using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Products;
using BakeryShop.Domain.Products;

namespace BakeryShop.Application.Staff.Products.UpdateProduct;
internal sealed class UpdateProductCommandHandler(IProductRepository productRepository)
    : ICommandHandler<UpdateProductCommand, ProductDto>
{
    public async Task<Result<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product is null)
            return Result.NotFound(ProductErrors.NotFound);

        product = UpdateProduct(product, request);
        await productRepository.Update(product);

        ProductDto dto = product;

        return dto;
    }

    private static Product UpdateProduct(Product product, UpdateProductCommand request)
    {
        product.Update(request.Title, request.Price, request.Description, request.Quantity, request.QuantityType, request.Information);

        return product;
    }
}
