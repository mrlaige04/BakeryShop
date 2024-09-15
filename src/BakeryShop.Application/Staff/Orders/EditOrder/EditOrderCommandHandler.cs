using Ardalis.Result;
using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Users.Orders;
using BakeryShop.Domain.Orders;
using Microsoft.Extensions.Logging;

namespace BakeryShop.Application.Staff.Orders.EditOrder;
internal sealed class EditOrderCommandHandler(
    IOrderRepository orderRepository,
    ILogger<EditOrderCommandHandler> logger
    )
    : ICommandHandler<EditOrderCommand, FullOrderDto>
{
    public async Task<Result<FullOrderDto>> Handle(EditOrderCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("EditOrderCommand: Started.");

        var order = await orderRepository.GetById(request.Id, cancellationToken);

        if (order is null)
        {
            logger.LogInformation("EditOrderCommand: Failed. Order not found.");
            return Result.NotFound();
        }
            
        order = UpdateOrder(order, request);

        await orderRepository.Update(order, cancellationToken);

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

        logger.LogInformation("EditOrderCommand: Success.");

        return dto;
    }

    private static Order UpdateOrder(Order order, EditOrderCommand request)
    {
        order.Status = request.Status;

        var newDelivery = new DeliveryInfo
        {
            City = request.City,
            Street = request.Street,
            HouseNumber = request.HouseNumber
        };

        order.DeliveryInfo = newDelivery;

        return order;
    }
}
