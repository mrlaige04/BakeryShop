using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Users.Orders;
using BakeryShop.Domain.Orders;

namespace BakeryShop.Application.Staff.Orders.EditOrder;
public record EditOrderCommand(
    OrderStatus Status,
    string City,
    string Street,
    string HouseNumber
    ) : ICommand<FullOrderDto>
{
    public Guid Id { get; set; }
}
