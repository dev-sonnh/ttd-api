using TTDesign.API.Domain.Services.Communication;

namespace TTDesign.API.Domain.Services
{
    public interface IAuthenticationService
    {
        Task<TokenResponse> CreateAccessTokenAsync(string username, string password);
        Task<TokenResponse> RefreshTokenAsync(string refreshToken, string username);
        void RevokeRefreshToken(string refreshToken);
    }
}
