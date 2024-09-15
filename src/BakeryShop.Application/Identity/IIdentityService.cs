using Ardalis.Result;
using BakeryShop.Application.Users;
using BakeryShop.Domain.Users;

namespace BakeryShop.Application.Identity;

public interface IIdentityService
{
    Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<Result<UserDto>> CreateUserAsync(string email, string password, CancellationToken cancellationToken = default);
}