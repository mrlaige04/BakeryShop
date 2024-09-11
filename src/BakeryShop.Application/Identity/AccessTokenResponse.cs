namespace BakeryShop.Application.Identity;

public record AccessTokenResponse(string AccessToken, DateTime ExpiresAt);