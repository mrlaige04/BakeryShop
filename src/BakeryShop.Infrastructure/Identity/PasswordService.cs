using BakeryShop.Application.Identity;
using BakeryShop.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace BakeryShop.Infrastructure.Identity;

public class PasswordService(UserManager<User> userManager) : IPasswordService
{
    public async Task<bool> VerifyPasswordAsync(User user, string password, CancellationToken cancellationToken = default)
    {
        return await userManager.CheckPasswordAsync(user, password);
    }
}