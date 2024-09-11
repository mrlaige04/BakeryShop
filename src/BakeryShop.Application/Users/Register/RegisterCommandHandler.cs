using Ardalis.Result;
using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Identity;

namespace BakeryShop.Application.Users.Register;

internal sealed class RegisterCommandHandler(
    IIdentityService identityService
    )
    : ICommandHandler<RegisterCommand, UserDto>
{
    public async Task<Result<UserDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await identityService.FindByEmailAsync(request.Email, cancellationToken) is not null)
        {
            return Result.Conflict(UserErrors.EmailAlreadyExists);
        }

        var userCreationResult = await identityService.CreateUserAsync(request.Email, request.Password, cancellationToken);

        return userCreationResult;
    }
}