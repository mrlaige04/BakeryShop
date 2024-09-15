using Ardalis.Result;
using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Products;
using BakeryShop.Domain.Products;

namespace BakeryShop.Application.Staff.Products.DeleteProduct;
internal sealed class DeleteProductCommandHandler(IProductRepository productRepository)
    : ICommandHandler<DeleteProductCommand>
{
    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product is null)
            return Result.NotFound(ProductErrors.NotFound);

        await productRepository.DeleteAsync(product, cancellationToken);

        return Result.Success();
    }
}
