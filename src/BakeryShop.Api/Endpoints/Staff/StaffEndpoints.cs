﻿using Ardalis.Result.AspNetCore;
using BakeryShop.Application.Staff.Orders.EditOrder;
using BakeryShop.Application.Staff.Orders.GetOrderById;
using BakeryShop.Application.Staff.Orders.GetOrders;
using BakeryShop.Application.Staff.Products.CreateProduct;
using BakeryShop.Application.Staff.Products.DeleteProduct;
using BakeryShop.Application.Staff.Products.SearchProducts;
using BakeryShop.Application.Staff.Products.UpdateProduct;
using BakeryShop.Domain.Consts;
using MediatR;

namespace BakeryShop.Api.Endpoints.Staff;

public static class StaffEndpoints
{
    private const string StaffTag = "Staff";
    private const string ProductsTag = "Products";
    private const string OrderTag = "Orders";

    public static void MapStaff(this IEndpointRouteBuilder builder)
    {
        var staffGroup = builder.MapGroup(StaffTag)
            .RequireAuthorization(Policy.Admin)
            .WithTags(StaffTag);

        staffGroup.MapProducts();
        staffGroup.MapOrders();
    }

    private static void MapProducts(this IEndpointRouteBuilder builder)
    {
        var productsGroup = builder.MapGroup(ProductsTag);

        productsGroup.MapGet("", async ([AsParameters] SearchProductQuery query, ISender sender) =>
        {
            var result = await sender.Send(query);
            return result.ToMinimalApiResult();
        });

        productsGroup.MapPost("", async (CreateProductCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return result.ToMinimalApiResult();
        });

        productsGroup.MapPut("{id:guid}", async (Guid id, UpdateProductCommand command, ISender sender) =>
        {
            command.Id = id;

            var result = await sender.Send(command);
            return result.ToMinimalApiResult();
        });

        productsGroup.MapDelete("{id:guid}", async (Guid id, ISender sender) =>
        {
            var command = new DeleteProductCommand(id);
            var result = await sender.Send(command);
            return result.ToMinimalApiResult();
        });
    }
    
    private static void MapOrders(this IEndpointRouteBuilder builder)
    {
        var ordersGroup = builder.MapGroup(OrderTag);

        ordersGroup.MapGet("", async ([AsParameters] GetOrdersQuery query, ISender sender) =>
        {
            var result = await sender.Send(query);
            return result.ToMinimalApiResult();
        });

        ordersGroup.MapGet("{id:guid}", async (Guid id, ISender sender) =>
        {
            var query = new GetOrderByIdQuery(id);
            var result = await sender.Send(query);

            return result.ToMinimalApiResult();
        });

        ordersGroup.MapPut("{id:guid}", async (Guid id, EditOrderCommand command, ISender sender) =>
        {
            command.Id = id;
            var result = await sender.Send(command);

            return result.ToMinimalApiResult();
        });
    }
}
