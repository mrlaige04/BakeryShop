using Ardalis.Result;
using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Identity;
using Microsoft.Extensions.Logging;

namespace BakeryShop.Application.Users.Register;

internal sealed class RegisterCommandHandler(
    IIdentityService identityService,
    ILogger<RegisterCommandHandler> logger)
    : ICommandHandler<RegisterCommand, UserDto>
{
    public async Task<Result<UserDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("RegisterCommand: Started.");

        if (await identityService.FindByEmailAsync(request.Email, cancellationToken) is not null)
        {
            logger.LogInformation("RegisterCommand: Conflict. User with such email already exists.");
            return Result.Conflict(UserErrors.EmailAlreadyExists);
        }

        var userCreationResult = await identityService.CreateUserAsync(request.Email, request.Password, cancellationToken);

        logger.LogInformation("RegisterCommand: Success.");

        return userCreationResult;
    }
}