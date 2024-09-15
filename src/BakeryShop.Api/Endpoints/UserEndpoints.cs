using Ardalis.Result.AspNetCore;
using BakeryShop.Application.Users.Login;
using BakeryShop.Application.Users.Register;
using MediatR;

namespace BakeryShop.Api.Endpoints;

public static class UserEndpoints
{
    private const string Tag = "Users";

    public static void MapUsers(this IEndpointRouteBuilder routeBuilder)
    {
        var usersGroup = routeBuilder
            .MapGroup(Tag)
            .WithTags(Tag);

        var routeHandlerBuilder = usersGroup.MapPost("register", async (
            RegisterCommand command,
            ISender sender
        ) =>
        {
            var registerResult = await sender.Send(command);

            return registerResult.ToMinimalApiResult();
        });

        usersGroup.MapPost("login", async (
            LoginCommand command,
            ISender sender
            ) =>
        {
            var loginResult = await sender.Send(command);

            return loginResult.ToMinimalApiResult();
        });
    }
}