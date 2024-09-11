using System.Security.Claims;
using BakeryShop.Application.Identity;

namespace BakeryShop.Api.Services;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    public Guid? Id => Guid.TryParse(
        httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier),
        out var userId) ?  userId : null;
}