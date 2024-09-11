using BakeryShop.Application.Common.Abstractions;

namespace BakeryShop.Application.Users.Register;

public record RegisterCommand(string Email, string Password) : ICommand<UserDto>;