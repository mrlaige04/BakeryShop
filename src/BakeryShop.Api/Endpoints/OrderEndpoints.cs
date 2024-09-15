using Ardalis.Result.AspNetCore;
using BakeryShop.Application.Users.Orders.CancelOrder;
using BakeryShop.Application.Users.Orders.CreateOrder;
using BakeryShop.Application.Users.Orders.GetOrders;
using MediatR;

namespace BakeryShop.Api.Endpoints;

public static class OrderEndpoints
{
    private const string Tag = "Orders";

    public static void MapOrders(this IEndpointRouteBuilder builder)
    {
        var ordersGroup = builder
            .MapGroup(Tag)
            .WithTags(Tag);

        ordersGroup.MapPost("", async (CreateOrderCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return result.ToMinimalApiResult();
        });

        ordersGroup.MapGet("", async ([AsParameters] GetOrdersQuery query, ISender sender) => 
        {
            var result = await sender.Send(query);
            return result.ToMinimalApiResult();
        });

        ordersGroup.MapDelete("{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new CancelOrderCommand(id));
            return result.ToMinimalApiResult();
        });
    }
}
