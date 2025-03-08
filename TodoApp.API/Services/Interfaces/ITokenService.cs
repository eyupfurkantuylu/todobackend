using TodoApp.API.Models.Identity;

namespace TodoApp.API.Services.Interfaces;

public interface ITokenService
{
    Task<(string AccessToken, string RefreshToken)> GenerateTokensAsync(ApplicationUser user);
    Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string accessToken, string refreshToken);
    Task RevokeRefreshTokenAsync(string refreshToken);
    bool ValidateAccessToken(string accessToken);
} 