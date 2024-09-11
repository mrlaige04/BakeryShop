namespace BakeryShop.Application.Identity;

public interface ICurrentUser
{
    Guid? Id { get; }
}