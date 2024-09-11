using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BakeryShop.Infrastructure.Identity;

public class JwtOptions
{
    public static readonly string SectionName = "Jwt";

    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public string Key { get; init; } = null!;
    public long ExpiresInMinutes { get; init; }
}

public static class JwtOptionsExtensions
{
    public static TokenValidationParameters ToTokenValidationParameters(this JwtOptions options)
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = options.Issuer,
            ValidAudience = options.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key)),
        };
    }
}