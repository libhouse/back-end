using Ardalis.GuardClauses;
using LibHouse.Business.Monads;
using LibHouse.Infrastructure.Authentication.Context;
using LibHouse.Infrastructure.Authentication.Token.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LibHouse.Infrastructure.Authentication.Token.Services.RefreshTokens
{
    public class IdentityRefreshTokenService : IRefreshTokenService
    {
        private readonly AuthenticationContext _authenticationContext;

        public IdentityRefreshTokenService(AuthenticationContext authenticationContext)
        {
            _authenticationContext = authenticationContext;
        }

        public async Task<bool> CheckIfRefreshTokenIsRevokedBasedOnAccessTokenIdAsync(string accessTokenId)
        {
            Guard.Against.NullOrWhiteSpace(accessTokenId, nameof(accessTokenId), "O id do token é obrigatório.");
            RefreshToken refreshToken = await _authenticationContext.RefreshTokens.FirstAsync(r => r.JwtId == accessTokenId);
            if (refreshToken is null) return false;
            return refreshToken.IsRevoked;
        }

        public async Task<RefreshToken> GetRefreshTokenByValueAsync(string value)
        {
            Guard.Against.NullOrWhiteSpace(value, nameof(value), "O valor do token é obrigatório.");
            return await _authenticationContext.RefreshTokens.FirstOrDefaultAsync(r => r.Token == value);
        }

        public async Task<Result> MarkRefreshTokenAsRevokedAsync(RefreshToken refreshToken)
        {
            Guard.Against.Null(refreshToken, nameof(refreshToken), "O token é obrigatório.");
            refreshToken.MarkAsRevoked();
            bool refreshTokenRevoked = await _authenticationContext.SaveChangesAsync() > 0;
            return refreshTokenRevoked ? Result.Success() : Result.Fail("Falha ao revogar o refresh token");
        }

        public async Task<Result> MarkRefreshTokenAsRevokedAsync(string refreshTokenValue)
        {
            Guard.Against.NullOrEmpty(refreshTokenValue, nameof(refreshTokenValue), "O valor do token é obrigatório.");
            RefreshToken refreshToken = await GetRefreshTokenByValueAsync(refreshTokenValue);
            refreshToken.MarkAsRevoked();
            bool refreshTokenRevoked = await _authenticationContext.SaveChangesAsync() > 0;
            return refreshTokenRevoked ? Result.Success() : Result.Fail("Falha ao revogar o refresh token");
        }

        public async Task<Result> MarkRefreshTokenAsUsedAsync(RefreshToken refreshToken)
        {
            Guard.Against.Null(refreshToken, nameof(refreshToken), "O token é obrigatório.");
            refreshToken.MarkAsUsed();
            bool refreshTokenUsed = await _authenticationContext.SaveChangesAsync() > 0;
            return refreshTokenUsed ? Result.Success() : Result.Fail("Falha ao marcar o refresh token como usado");
        }
    }
}