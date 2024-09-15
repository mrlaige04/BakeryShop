using BakeryShop.Application.Common.Abstractions;

namespace BakeryShop.Application.Users.Orders.CreateOrder;
public record CreateOrderCommand(
    string City,
    string Street, 
    string HouseNumber,
    string? AdditionalInfo,
    DateTime? DeliveryDate) : ICommand<ShortOrderDto>;
