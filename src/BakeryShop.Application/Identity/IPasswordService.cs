using BakeryShop.Domain.Users;

namespace BakeryShop.Application.Identity;

public interface IPasswordService
{
    Task<bool> VerifyPasswordAsync(User user, string password, CancellationToken cancellationToken = default);
}