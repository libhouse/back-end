using LibHouse.Business.Monads;
using LibHouse.Infrastructure.Authentication.Token.Models;
using System.Threading.Tasks;

namespace LibHouse.Infrastructure.Authentication.Token.Services.RefreshTokens
{
    public interface IRefreshTokenService
    {
        Task<RefreshToken> GetRefreshTokenByValueAsync(string value);
        Task<Result> MarkRefreshTokenAsUsedAsync(RefreshToken refreshToken);
        Task<Result> MarkRefreshTokenAsRevokedAsync(RefreshToken refreshToken);
        Task<Result> MarkRefreshTokenAsRevokedAsync(string refreshTokenValue);
        Task<bool> CheckIfRefreshTokenIsRevokedBasedOnAccessTokenIdAsync(string accessTokenId);
    }
}