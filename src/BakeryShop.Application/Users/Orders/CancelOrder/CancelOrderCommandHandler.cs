using Ardalis.Result;
using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Identity;
using BakeryShop.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BakeryShop.Application.Users.Orders.CancelOrder;
internal sealed class CancelOrderCommandHandler(
    IOrderRepository orderRepository,
    ICurrentUser currentUser,
    ILogger<CancelOrderCommandHandler> logger)
    : ICommandHandler<CancelOrderCommand>
{
    public async Task<Result> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("CancelOrderCommand: Started.");

        if (!Guid.TryParse(currentUser.Id?.ToString(), out var userId))
        {
            logger.LogInformation("CancelOrderCommand: Failed. User unauthorized.");
            return Result.Unauthorized();
        }

        var order = await orderRepository.Source
            .FirstOrDefaultAsync(o => o.UserId == userId && o.Id == request.Id, cancellationToken);

        if (order is null)
        {
            logger.LogInformation("CancelOrderCommand: Failed. Order not found.");
            return Result.NotFound();
        }
            
        order.Status = OrderStatus.Cancelled;

        await orderRepository.Update(order, cancellationToken);

        logger.LogInformation("CancelOrderCommand: Success.");

        return Result.Success();
    }
}
