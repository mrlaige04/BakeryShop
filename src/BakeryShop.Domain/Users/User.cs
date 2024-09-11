using BakeryShop.Domain.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace BakeryShop.Domain.Users;

public class User : IdentityUser<Guid>, IEntity<Guid>
{
    
}