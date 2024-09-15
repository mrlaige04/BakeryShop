using Ardalis.Result;
using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Identity;
using BakeryShop.Domain.Users;

namespace BakeryShop.Application.Users.Cart.GetItems;
internal sealed class GetCartItemsQueryHandler(
    ICartRepository cartRepository,
    ICurrentUser currentUser
    )
    : IQueryHandler<GetCartItemsQuery, IEnumerable<CartItemDto>>
{
    public async Task<Result<IEnumerable<CartItemDto>>> Handle(GetCartItemsQuery request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(currentUser.Id?.ToString(), out var userId))
        {
            return Result.Unauthorized();
        }

        var cart = await cartRepository.GetByUserId(userId, cancellationToken);
        if (cart is null)
            return Result.NotFound();

        var items = cart.Items;
        if (items is null)
            return Result.Error("Items could not be found");

        return items.Select(i => new CartItemDto(i.Product, i.Quantity)).ToList();
    }
}
