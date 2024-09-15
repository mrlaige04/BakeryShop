using BakeryShop.Domain.Users;
using BakeryShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BakeryShop.Infrastructure.Users;
public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
{
    private DbSet<User> Users => dbContext.Users;

    public async Task<User?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await Users
            .Include(u => u.Orders)
            .Include(u => u.Cart)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task Update(User user, CancellationToken cancellationToken = default)
    {
        Users.Update(user);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
