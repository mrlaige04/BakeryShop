using BakeryShop.Domain.Consts;
using BakeryShop.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace BakeryShop.Api.Services;

public class AdminInitializer(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager, IConfiguration configuration)
{
    public async Task Initialize()
    {
        var email = configuration["Admin:Default:Email"];
        var password = configuration["Admin:Default:Password"];

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) 
            throw new ArgumentNullException(nameof(email), "Default admin must be specified");

        if (!await roleManager.RoleExistsAsync(Policy.Admin))
            await roleManager.CreateAsync(new IdentityRole<Guid>(Policy.Admin));

        if (await userManager.FindByEmailAsync(email) is { } user &&
            await userManager.IsInRoleAsync(user, Policy.Admin))
            return;

        var defaultAdmin = User.Create(email);

        var adminCreationResult = await userManager.CreateAsync(defaultAdmin, password);
        if (!adminCreationResult.Succeeded)
            throw new Exception("Admin creation failed.");

        var addingToAdminRoleResult = await userManager.AddToRoleAsync(defaultAdmin, Policy.Admin);
        if (!addingToAdminRoleResult.Succeeded)
            throw new Exception("Admin creation failed.");
    }
}
