using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Common.Models;
using BakeryShop.Domain.Orders;

namespace BakeryShop.Application.Users.Orders.GetOrders;
public record GetOrdersQuery(
    int? PageNumber,
    int? PageSize) : IQuery<PaginatedList<FullOrderDto>>;