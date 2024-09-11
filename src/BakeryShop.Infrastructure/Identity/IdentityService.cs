using Ardalis.Result;
using BakeryShop.Application.Identity;
using BakeryShop.Application.Users;
using BakeryShop.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace BakeryShop.Infrastructure.Identity;

public class IdentityService(UserManager<User> userManager)
    : IIdentityService
{
    public async Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await userManager.FindByEmailAsync(email);
    }

    public async Task<Result<UserDto>> CreateUserAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var user = User.Create(email);

        var registerIdentityResult = await userManager.CreateAsync(user, password);

        if (registerIdentityResult.Succeeded)
        {
            var dto = new UserDto(user.Id, email);
            return dto;
        }

        var errors = registerIdentityResult.Errors.Select(e => e.Description);
        var errorList = new ErrorList(errors);

        return Result.Error(errorList);
    }
}