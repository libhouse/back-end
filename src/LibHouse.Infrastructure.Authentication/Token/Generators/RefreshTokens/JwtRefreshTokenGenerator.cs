using LibHouse.Infrastructure.Authentication.Context;
using LibHouse.Infrastructure.Authentication.Helpers.String;
using LibHouse.Infrastructure.Authentication.Token.Models;
using LibHouse.Infrastructure.Authentication.Token.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace LibHouse.Infrastructure.Authentication.Token.Generators.RefreshTokens
{
    public class JwtRefreshTokenGenerator : IRefreshTokenGenerator
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AuthenticationContext _authenticationContext;
        private readonly RefreshTokenSettings _refreshTokenSettings;

        public JwtRefreshTokenGenerator(
            UserManager<IdentityUser> userManager,
            AuthenticationContext authenticationContext,
            IOptions<RefreshTokenSettings> refreshTokenSettings)
        {
            _userManager = userManager;
            _authenticationContext = authenticationContext;
            _refreshTokenSettings = refreshTokenSettings.Value;
        }

        public async Task<RefreshToken> GenerateRefreshTokenAsync(string userEmail, string accessTokenId)
        {
            IdentityUser user = await _userManager.FindByEmailAsync(userEmail);

            if (!user.EmailConfirmed || user.LockoutEnd is not null)
            {
                throw new InvalidOperationException("O usuário não está apto para obter um refresh token");
            }

            string tokenValue = GenerateTokenSequence();

            DateTime tokenCreatedAt = DateTime.UtcNow;

            DateTime tokenExpiresIn = DateTime.UtcNow.AddMonths(_refreshTokenSettings.ExpiresInMonths);

            RefreshToken refreshToken = new(tokenValue, accessTokenId, user.Id, tokenCreatedAt, tokenExpiresIn);

            await _authenticationContext.RefreshTokens.AddAsync(refreshToken);

            await _authenticationContext.SaveChangesAsync();

            return refreshToken;
        }

        private string GenerateTokenSequence()
        {
            return RandomStringGenerator.GenerateRandomString(_refreshTokenSettings.TokenLength);
        }
    }
}