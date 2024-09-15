using BakeryShop.Application.Common.Abstractions;

namespace BakeryShop.Application.Users.Cart.GetItems;
public record GetCartItemsQuery : IQuery<IEnumerable<CartItemDto>>;
