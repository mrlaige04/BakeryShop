using Ardalis.Result;
using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Identity;
using BakeryShop.Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace BakeryShop.Application.Users.Orders.CancelOrder;
internal sealed class CancelOrderCommandHandler(
    IOrderRepository orderRepository,
    ICurrentUser currentUser)
    : ICommandHandler<CancelOrderCommand>
{
    public async Task<Result> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(currentUser.Id?.ToString(), out var userId))
        {
            return Result.Unauthorized();
        }

        var order = await orderRepository.Source
            .FirstOrDefaultAsync(o => o.UserId == userId && o.Id == request.Id, cancellationToken);

        if (order is null)
            return Result.NotFound();

        order.Status = OrderStatus.Cancelled;

        await orderRepository.Update(order, cancellationToken);

        return Result.Success();
    }
}
