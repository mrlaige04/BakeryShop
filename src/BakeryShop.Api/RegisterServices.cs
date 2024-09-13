using BakeryShop.Api.Services;
using BakeryShop.Application.Identity;
using BakeryShop.Infrastructure.Identity;
using Microsoft.OpenApi.Models;

namespace BakeryShop.Api;

public static class RegisterServices
{
    public static IServiceCollection LoadConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

        return services;
    }

    public static IServiceCollection AddWebUi(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(sw =>
        {
            sw.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "BakeryShop.Api",
                Version = "v1"
            });
            
            sw.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Enter jwt token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            
            sw.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        services.AddHttpContextAccessor();

        services.AddScoped<ICurrentUser, CurrentUser>();
        
        services.AddSingleton(TimeProvider.System);

        services.AddCors(opt =>
        {
            opt.AddDefaultPolicy(cors => 
                cors.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });

        services.AddScoped<AdminInitializer>();
        
        return services;
    }
}