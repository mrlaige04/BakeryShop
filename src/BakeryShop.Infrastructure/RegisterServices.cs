using BakeryShop.Application.Identity;
using BakeryShop.Domain.Consts;
using BakeryShop.Domain.Orders;
using BakeryShop.Domain.Products;
using BakeryShop.Domain.Users;
using BakeryShop.Infrastructure.Carts;
using BakeryShop.Infrastructure.Data;
using BakeryShop.Infrastructure.Data.Interceptors;
using BakeryShop.Infrastructure.Identity;
using BakeryShop.Infrastructure.Orders;
using BakeryShop.Infrastructure.Products;
using BakeryShop.Infrastructure.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BakeryShop.Infrastructure;

public static class RegisterServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddAppIdentity(configuration);
        
        return services;
    }

    private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        ArgumentException.ThrowIfNullOrEmpty(connectionString, "Connection string must be specified.");

        services.AddScoped<AuditableEntityInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, opt) =>
        {
            opt.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            opt.UseSqlServer(connectionString);
        });

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }

    private static void AddAppIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 5;

                opt.ClaimsIdentity.RoleClaimType = "roles";
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        var jwtOptions = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();
        ArgumentNullException.ThrowIfNull(jwtOptions);
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(cfg =>
            {
                cfg.TokenValidationParameters = jwtOptions.ToTokenValidationParameters();
            });

        services.AddAuthorizationBuilder()
            .AddPolicy(Policy.Admin, policy =>
            {
                policy.RequireRole(Policy.Admin);
            });

        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<ITokenService, TokenService>();
    }
}