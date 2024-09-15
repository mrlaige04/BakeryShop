using BakeryShop.Application.Common.Abstractions;

namespace BakeryShop.Application.Cart.RemoveFromCart;
public record RemoveFromCartCommand(Guid Id, double Quantity) : ICommand;
