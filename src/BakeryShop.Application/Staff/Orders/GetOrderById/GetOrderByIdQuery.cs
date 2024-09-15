using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Users.Orders;

namespace BakeryShop.Application.Staff.Orders.GetOrderById;
public record GetOrderByIdQuery(Guid Id) : IQuery<FullOrderDto>;