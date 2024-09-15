using System.Security.Claims;
using System.Text;
using BakeryShop.Application.Identity;
using BakeryShop.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace BakeryShop.Infrastructure.Identity;

public class TokenService(IOptions<JwtOptions> jwtOptions, IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory)
    : ITokenService
{
    public async Task<AccessTokenResponse> CreateTokenAsync(User user, CancellationToken cancellationToken = default)
    {
        var options = jwtOptions.Value;

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = await userClaimsPrincipalFactory.CreateAsync(user);
        var claimsIdentity = new ClaimsIdentity(claims.Claims);

        var now = DateTime.Now;
        var expires = now.AddMinutes(options.ExpiresInMinutes);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Expires = expires,
            SigningCredentials = credentials,
            Issuer = options.Issuer,
            Audience = options.Audience
        };

        var handler = new JsonWebTokenHandler();

        var token = handler.CreateToken(tokenDescriptor);

        return new AccessTokenResponse(token, expires);
    }
}