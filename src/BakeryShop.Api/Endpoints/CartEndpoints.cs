using Ardalis.Result.AspNetCore;
using BakeryShop.Application.Cart.AddToCart;
using BakeryShop.Application.Cart.RemoveFromCart;
using BakeryShop.Application.Users.Cart.GetItems;
using MediatR;

namespace BakeryShop.Api.Endpoints;

public static class CartEndpoints
{
    private const string Tag = "Carts";

    public static void MapCarts(this IEndpointRouteBuilder builder)
    {
        var cartsGroup = builder.MapGroup(Tag)
            .RequireAuthorization()
            .WithTags(Tag);

        cartsGroup.MapGet("items", async (ISender sender) =>
        {
            var query = new GetCartItemsQuery();
            var result = await sender.Send(query);

            return result.ToMinimalApiResult();
        });

        cartsGroup.MapPost("items", async (AddToCartCommand command, ISender sender) => 
        {
            var result = await sender.Send(command);
            return result.ToMinimalApiResult();
        });
        
        cartsGroup.MapPut("items", async (RemoveFromCartCommand command, ISender sender) => 
        {
            var result = await sender.Send(command);
            return result.ToMinimalApiResult();
        });
    }
}