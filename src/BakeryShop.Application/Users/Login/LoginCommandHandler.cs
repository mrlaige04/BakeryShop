using Ardalis.Result;
using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Identity;
using Microsoft.Extensions.Logging;

namespace BakeryShop.Application.Users.Login;

internal sealed class LoginCommandHandler(
    IIdentityService identityService,
    IPasswordService passwordService,
    ITokenService tokenService,
    ILogger<LoginCommandHandler> logger)
    : ICommandHandler<LoginCommand, AccessTokenResponse>
{
    public async Task<Result<AccessTokenResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("LoginCommand: Started.");

        if (await identityService.FindByEmailAsync(request.Email, cancellationToken) is not { } user)
        {
            logger.LogInformation("LoginCommand: Failed. User not found.");
            return Result.NotFound(UserErrors.NotFound);
        }

        if (!await passwordService.VerifyPasswordAsync(user, request.Password, cancellationToken))
        {
            logger.LogInformation("LoginCommand: Error. Invalid password.");
            return Result.Unauthorized();
        }

        var accessToken = await tokenService.CreateTokenAsync(user, cancellationToken);

        logger.LogInformation("LoginCommand: Success.");

        return accessToken;
    }
}