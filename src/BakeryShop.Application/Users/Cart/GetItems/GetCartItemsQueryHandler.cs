using Ardalis.Result;
using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Identity;
using BakeryShop.Domain.Users;
using Microsoft.Extensions.Logging;

namespace BakeryShop.Application.Users.Cart.GetItems;
internal sealed class GetCartItemsQueryHandler(
    ICartRepository cartRepository,
    ICurrentUser currentUser,
    ILogger<GetCartItemsQueryHandler> logger)
    : IQueryHandler<GetCartItemsQuery, IEnumerable<CartItemDto>>
{
    public async Task<Result<IEnumerable<CartItemDto>>> Handle(GetCartItemsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCartItemsQuery: Started.");

        if (!Guid.TryParse(currentUser.Id?.ToString(), out var userId))
        {
            logger.LogInformation("GetCartItemsQuery: Failed. User unauthorized.");
            return Result.Unauthorized();
        }

        var cart = await cartRepository.GetByUserId(userId, cancellationToken);
        if (cart is null)
        {
            logger.LogInformation("GetCartItemsQuery: Failed. Cart not found.");
            return Result.NotFound();
        }
            
        var items = cart.Items;
        if (items is null)
        {
            logger.LogInformation("GetCartItemsQuery: Error. Items not found.");
            return Result.Error("Items could not be found");
        }

        logger.LogInformation("GetCartItemsQuery: Success.");

        return items.Select(i => new CartItemDto(i.Product, i.Quantity)).ToList();
    }
}
