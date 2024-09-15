using Ardalis.Result;
using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Identity;
using BakeryShop.Domain.Orders;
using BakeryShop.Domain.Users;
using Microsoft.Extensions.Logging;

namespace BakeryShop.Application.Users.Orders.CreateOrder;
internal sealed class CreateOrderCommandHandler(
    ICurrentUser currentUser,
    ICartRepository cartRepository,
    IUserRepository userRepository,
    ILogger<CreateOrderCommandHandler> logger)
    : ICommandHandler<CreateOrderCommand, ShortOrderDto>
{
    public async Task<Result<ShortOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateOrderCommand: Started.");

        if (!Guid.TryParse(currentUser.Id?.ToString(), out var userId))
        {
            logger.LogInformation("CreateOrderCommand: Failed. User unauthorized.");
            return Result.Unauthorized();
        }

        var user = await userRepository.GetById(userId, cancellationToken);
        if (user is null)
        {
            logger.LogInformation("CreateOrderCommand: Failed. User unauthorized.");
            return Result.Unauthorized();
        }
            
        var cart = await cartRepository.GetByUserId(userId, cancellationToken);
        if (cart is null)
        {
            logger.LogInformation("CreateOrderCommand: Failed. Cart not found.");
            return Result.NotFound();
        }
            
        var deliveryInfo = new DeliveryInfo
        {
            City = request.City,
            Street = request.Street,
            HouseNumber = request.HouseNumber,
            DeliveryDate = request.DeliveryDate
        };

        var items = cart.Items
            .Select(ci => OrderItem.Create(ci.Product, ci.Quantity))
            .ToList();

        var order = Order.Create(items, deliveryInfo);
        order.AdditionalInfo = request.AdditionalInfo;

        user.AddOrder(order);
        user.ClearCart();

        await userRepository.Update(user, cancellationToken);

        var dto = new ShortOrderDto { 
            Status = order.Status,
            DeliveryInfo = order.DeliveryInfo
        };

        logger.LogInformation("CreateOrderCommand: Success.");

        return dto;
    }
}
