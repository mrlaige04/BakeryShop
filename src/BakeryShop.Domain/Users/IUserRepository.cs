namespace BakeryShop.Domain.Users;
public interface IUserRepository
{
    Task<User?> GetById(Guid id, CancellationToken cancellationToken = default);
    Task Update(User user, CancellationToken cancellationToken = default);
}
