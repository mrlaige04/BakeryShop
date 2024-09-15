using Ardalis.Result;
using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Identity;
using BakeryShop.Application.Products;
using BakeryShop.Domain.Products;
using BakeryShop.Domain.Users;

namespace BakeryShop.Application.Cart.AddToCart;
internal sealed class AddToCartCommandHandler(
    ICurrentUser currentUser,
    ICartRepository cartRepository,
    IProductRepository productRepository)
    : ICommandHandler<AddToCartCommand>
{
    public async Task<Result> Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(currentUser.Id?.ToString(), out var userId))
        {
            return Result.Unauthorized();
        }

        var cart = await cartRepository.GetByUserId(userId, cancellationToken);
        if (cart is null)
            return Result.NotFound();

        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product is null)
            return Result.Error(ProductErrors.NotFound);

        await cartRepository.AddProductToCart(cart, product, request.Quantity, cancellationToken);

        return Result.Success();
    }
}
