using BakeryShop.Application.Common.Models;

namespace BakeryShop.Application.Users;

public static class UserErrors
{
    public const string NotFound = "User was not found.";
    public const string InvalidPassword = "The password is invalid.";
    public const string EmailAlreadyExists = "User with such email already exists.";
}