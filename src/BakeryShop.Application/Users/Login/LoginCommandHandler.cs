using Ardalis.Result;
using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Common.Models;
using BakeryShop.Application.Identity;

namespace BakeryShop.Application.Users.Login;

internal sealed class LoginCommandHandler(
    IIdentityService identityService,
    IPasswordService passwordService,
    ITokenService tokenService
    )
    : ICommandHandler<LoginCommand, AccessTokenResponse>
{
    public async Task<Result<AccessTokenResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (await identityService.FindByEmailAsync(request.Email, cancellationToken) is not { } user)
        {
            return Result.NotFound(UserErrors.NotFound);
        }

        if (!await passwordService.VerifyPasswordAsync(user, request.Password, cancellationToken))
        {
            return Result.Unauthorized();
        }

        var accessToken = await tokenService.CreateTokenAsync(user, cancellationToken);

        return accessToken;
    }
}