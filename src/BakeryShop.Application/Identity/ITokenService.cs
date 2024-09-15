using BakeryShop.Domain.Users;

namespace BakeryShop.Application.Identity;

public interface ITokenService
{
    Task<AccessTokenResponse> CreateTokenAsync(User user, CancellationToken cancellationToken = default);
}