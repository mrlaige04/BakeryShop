using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace BakeryShop.Application;

public static class RegisterServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(opt =>
            opt.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}