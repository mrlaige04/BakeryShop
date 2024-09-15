using BakeryShop.Application.Common.Abstractions;

namespace BakeryShop.Application.Cart.AddToCart;
public record AddToCartCommand(Guid Id, double Quantity) : ICommand;
