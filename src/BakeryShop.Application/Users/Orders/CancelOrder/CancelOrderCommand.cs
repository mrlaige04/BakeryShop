using BakeryShop.Application.Common.Abstractions;

namespace BakeryShop.Application.Users.Orders.CancelOrder;
public record CancelOrderCommand(Guid Id) : ICommand;
