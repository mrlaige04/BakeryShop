﻿using Ardalis.Result;
using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Common.Extensions;
using BakeryShop.Application.Common.Models;
using BakeryShop.Application.Identity;
using BakeryShop.Application.Users.Orders;
using BakeryShop.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BakeryShop.Application.Staff.Orders.GetOrders;
internal sealed class GetOrdersQueryHandler(
    IOrderRepository orderRepository,
    ICurrentUser currentUser,
    ILogger<GetOrdersQueryHandler> logger)
    : IQueryHandler<GetOrdersQuery, PaginatedList<FullOrderDto>>
{
    public async Task<Result<PaginatedList<FullOrderDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetOrdersQuery: Started.");

        var orders = orderRepository.Source
            .Include(o => o.Items)
                .ThenInclude(i => i.Product)
            .Include(o => o.DeliveryInfo)
            .Where(o => true);

        if (!Guid.TryParse(currentUser.Id?.ToString(), out var userId))
        {
            logger.LogInformation("GetOrdersQuery: Failed. User unauthorized");
            return Result.Unauthorized();
        }

        orders = orders.Where(u => u.UserId == userId);

        orders = ApplyFilters(orders, request);

        var pageNumber = ParseIntOrDefault(request.PageNumber?.ToString(), 1);
        var pageSize = ParseIntOrDefault(request.PageSize?.ToString(), 10);

        if (pageNumber < 0)
        {
            logger.LogInformation("GetOrdersQuery: Error. Invalid page number");
            return Result.Error("PageNumber cannot be a negative number");
        }
            
        if (pageSize < 0)
        {
            logger.LogInformation("GetOrdersQuery: Error. Invalid page size");
            return Result.Error("PageSize cannot be a negative number");
        }
            
        var result = await orders
            .Select(order =>
                new FullOrderDto()
                {
                    DeliveryInfo = order.DeliveryInfo,
                    Id = order.Id,
                    Status = order.Status,
                    Items = order.Items
                        .Select(i => new OrderItemDto
                        {
                            Product = i.Product,
                            Quantity = i.Quantity
                        })
                }
            )
            .PaginatedListAsync(pageNumber, pageSize);

        logger.LogInformation("GetOrdersQuery: Success.");

        return result;
    }

    private static IQueryable<Order> ApplyFilters(IQueryable<Order> orders, GetOrdersQuery request)
    {
        if (request.OrderStatus.HasValue)
            orders = orders.Where(o => o.Status == request.OrderStatus.Value);

        return orders;
    }

    private static int ParseIntOrDefault(string? value, int defaultValue) =>
       int.TryParse(value, out var result) ? result : defaultValue;
}
