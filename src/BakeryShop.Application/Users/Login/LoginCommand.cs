using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Identity;

namespace BakeryShop.Application.Users.Login;

public record LoginCommand(string Email, string Password) : ICommand<AccessTokenResponse>;