using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Common.Models;
using BakeryShop.Application.Users.Orders;
using BakeryShop.Domain.Orders;

namespace BakeryShop.Application.Staff.Orders.GetOrders;
public record GetOrdersQuery(
    OrderStatus? OrderStatus,
    int? PageNumber,
    int? PageSize) : IQuery<PaginatedList<FullOrderDto>>;