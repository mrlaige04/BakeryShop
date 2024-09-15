using Ardalis.Result;
using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Identity;
using BakeryShop.Application.Products;
using BakeryShop.Domain.Products;
using BakeryShop.Domain.Users;
using Microsoft.Extensions.Logging;

namespace BakeryShop.Application.Cart.AddToCart;
internal sealed class AddToCartCommandHandler(
    ICurrentUser currentUser,
    ICartRepository cartRepository,
    IProductRepository productRepository,
    ILogger<AddToCartCommandHandler> logger
    )
    : ICommandHandler<AddToCartCommand>
{
    public async Task<Result> Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("AddToCartCommand: Started.");

        if (!Guid.TryParse(currentUser.Id?.ToString(), out var userId))
        {
            logger.LogInformation("AddToCartCommand: Failed. User unauthorized.");
            return Result.Unauthorized();
        }

        var cart = await cartRepository.GetByUserId(userId, cancellationToken);
        if (cart is null)
        {
            logger.LogInformation("AddToCartCommand: Failed. Cart not found.");
            return Result.NotFound();
        }
            
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product is null)
        {
            logger.LogInformation("AddToCartCommand: Failed. Product not found.");
            return Result.Error(ProductErrors.NotFound);
        }

        await cartRepository.AddProductToCart(cart, product, request.Quantity, cancellationToken);
        logger.LogInformation("AddToCartCommand: Success.");

        return Result.Success();
    }
}
