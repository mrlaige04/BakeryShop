using Ardalis.Result.AspNetCore;
using BakeryShop.Application.Products.SearchProducts;
using MediatR;

namespace BakeryShop.Api.Endpoints;
public static class ProductEndpoints
{
    private const string Tag = "Products";
    public static void MapProducts(this IEndpointRouteBuilder routeBuilder)
    {
        var productsGroup = routeBuilder
            .MapGroup(Tag)
            .WithTags(Tag);

        productsGroup.MapGet("", async ([AsParameters] SearchProductsQuery query, ISender sender) =>
        {
            var result = await sender.Send(query);

            return result.ToMinimalApiResult();
        });
    }
}