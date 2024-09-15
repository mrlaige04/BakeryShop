using Ardalis.Result;
using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Common.Extensions;
using BakeryShop.Application.Common.Models;
using BakeryShop.Application.Identity;
using BakeryShop.Application.Products;
using BakeryShop.Domain.Orders;
using BakeryShop.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace BakeryShop.Application.Users.Orders.GetOrders;
internal sealed class GetOrdersQueryHandler(
    IOrderRepository orderRepository,
    ICurrentUser currentUser
    ) 
    : IQueryHandler<GetOrdersQuery, PaginatedList<FullOrderDto>>
{
    public async Task<Result<PaginatedList<FullOrderDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = orderRepository.Source
            .Include(o => o.Items)
                .ThenInclude(i => i.Product)
            .Include(o => o.DeliveryInfo)
            .Where(o => true);

        if (!Guid.TryParse(currentUser.Id?.ToString(), out var userId))
        {
            return Result.Unauthorized();
        }

        orders = orders.Where(u => u.UserId == userId);

        var pageNumber = ParseIntOrDefault(request.PageNumber?.ToString(), 1);
        var pageSize = ParseIntOrDefault(request.PageSize?.ToString(), 10);

        if (pageNumber < 0)
            return Result.Error("PageNumber cannot be a negative number");
        if (pageSize < 0)
            return Result.Error("PageSize cannot be a negative number");

        var result = await orders
            .Select(order =>
                new FullOrderDto()
                {
                    DeliveryInfo = order.DeliveryInfo,
                    Id = order.Id,
                    Status = order.Status,
                    Items = order.Items
                        .Select(i => new OrderItemDto { 
                            Product = i.Product, 
                            Quantity = i.Quantity 
                        })
                }
            )
            .PaginatedListAsync(pageNumber, pageSize);

        return result;
    }

    private static int ParseIntOrDefault(string? value, int defaultValue) =>
       int.TryParse(value, out var result) ? result : defaultValue;
}
