using Ardalis.Result;
using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Identity;
using BakeryShop.Application.Products;
using BakeryShop.Domain.Products;
using BakeryShop.Domain.Users;
using Microsoft.Extensions.Logging;

namespace BakeryShop.Application.Cart.RemoveFromCart;
internal sealed class RemoveFromCartCommandHandler
    (ICurrentUser currentUser,
    ICartRepository cartRepository,
    IProductRepository productRepository,
    ILogger<RemoveFromCartCommandHandler> logger)
    : ICommandHandler<RemoveFromCartCommand>
{
    public async Task<Result> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(currentUser.Id?.ToString(), out var userId))
        {
            logger.LogInformation("RemoveFromCartCommand: Started.");
            return Result.Unauthorized();
        }

        var cart = await cartRepository.GetByUserId(userId, cancellationToken);
        if (cart is null)
        {
            logger.LogInformation("RemoveFromCartCommand: Failed. Cart not found.");
            return Result.NotFound();
        }
            
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product is null)
        {
            logger.LogInformation("RemoveFromCartCommand: Failed. Product not found");
            return Result.Error(ProductErrors.NotFound);
        }
            
        await cartRepository.RemoveProductFromCart(cart, product, request.Quantity, cancellationToken);
        logger.LogInformation("RemoveFromCartCommand: Success.");

        return Result.Success();
    }
}
