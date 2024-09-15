using Ardalis.Result;
using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Users.Orders;
using BakeryShop.Domain.Orders;
using Microsoft.Extensions.Logging;

namespace BakeryShop.Application.Staff.Orders.GetOrderById;
internal sealed class GetOrderByIdQueryHandler(
    IOrderRepository orderRepository,
    ILogger<GetOrderByIdQueryHandler> logger)
    : IQueryHandler<GetOrderByIdQuery, FullOrderDto>
{
    public async Task<Result<FullOrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetOrderByIdQuery: Started.");

        var order = await orderRepository.GetById(request.Id, cancellationToken);

        if (order is null)
        {
            logger.LogInformation("GetOrderByIdQuery: Failed. Order not found.");
            return Result.NotFound();
        }
            
        var dto = new FullOrderDto()
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
        };

        logger.LogInformation("GetOrderByIdQuery: Success.");

        return dto;
    }
}
