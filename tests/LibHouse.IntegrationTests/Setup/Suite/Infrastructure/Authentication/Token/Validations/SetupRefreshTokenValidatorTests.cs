using LibHouse.Infrastructure.Authentication.Context;
using LibHouse.Infrastructure.Authentication.Token.Generators.AccessTokens;
using LibHouse.Infrastructure.Authentication.Token.Generators.RefreshTokens;
using LibHouse.Infrastructure.Authentication.Token.Models;
using LibHouse.Infrastructure.Authentication.Token.Settings;
using LibHouse.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace LibHouse.IntegrationTests.Setup.Suite.Infrastructure.Authentication.Token.Validations
{
    internal static class SetupRefreshTokenValidatorTests
    {
        private static readonly string _tokenKey = Guid.NewGuid().ToString();
        private static readonly string _tokenIssuer = "LibHouse";
        private static readonly string _tokenValidIn = "https://localhost";
        private static readonly int _refreshTokenExpiresInMonths = 3;
        private static readonly int _refreshTokenLength = 35;

        internal static TokenValidationParameters SetupTokenValidationParameters()
        {
            return new()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenKey)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = _tokenIssuer,
                ValidAudience = _tokenValidIn,
            };
        }

        internal static RefreshToken SetupRefreshToken(
            SecurityToken validatedToken,
            IdentityUser userWhoOwnsTheToken,
            DateTime? createdAt = null,
            DateTime? expiresIn = null,
            string jwtId = null,
            bool isUsed = false,
            bool isRevoked = false)
        {
            return new(
                token: Guid.NewGuid().ToString(), 
                jwtId: jwtId ?? validatedToken.Id, 
                isUsed: isUsed, 
                isRevoked: isRevoked, 
                createdAt: createdAt ?? DateTime.UtcNow,
                expiresIn: expiresIn ?? DateTime.UtcNow.AddDays(3), 
                userId: userWhoOwnsTheToken.Id
            );
        }

        internal static IAccessTokenGenerator SetupTokenGenerator(
            IdentityUser userWhoOwnsTheToken,
            AuthenticationContext authenticationContext,
            int tokenExpirationInSeconds)
        {
            UserManager<IdentityUser> userManager = SetupUserManager(userWhoOwnsTheToken);

            AccessTokenSettings tokenSettings = SetupAccessTokenSettings(tokenExpirationInSeconds);

            IRefreshTokenGenerator refreshTokenGenerator = SetupRefreshTokenGenerator(userManager, authenticationContext);

            return new JwtAccessTokenGenerator(
                userManager, 
                tokenSettings,
                refreshTokenGenerator
            );
        }

        private static IRefreshTokenGenerator SetupRefreshTokenGenerator(
            UserManager<IdentityUser> userManager,
            AuthenticationContext authenticationContext)
        {
            RefreshTokenSettings refreshTokenSettings = SetupRefreshTokenSettings();

            return new JwtRefreshTokenGenerator(userManager, authenticationContext, refreshTokenSettings);
        }

        private static AccessTokenSettings SetupAccessTokenSettings(int tokenExpirationInSeconds)
        {
            return new AccessTokenSettings()
            {
                Secret = _tokenKey,
                ExpiresInSeconds = tokenExpirationInSeconds,
                Issuer = _tokenIssuer,
                ValidIn = _tokenValidIn
            };
        }

        private static RefreshTokenSettings SetupRefreshTokenSettings()
        {
            return new RefreshTokenSettings()
            {
                ExpiresInMonths = _refreshTokenExpiresInMonths,
                TokenLength = _refreshTokenLength
            };
        }

        private static UserManager<IdentityUser> SetupUserManager(IdentityUser userWhoOwnsTheToken)
        {
            Mock<UserManager<IdentityUser>> userManager = MockHelper.CreateMockForUserManager();

            userManager.Setup(u => u.FindByEmailAsync(userWhoOwnsTheToken.Email)).ReturnsAsync(userWhoOwnsTheToken);

            userManager.Setup(u => u.GetClaimsAsync(userWhoOwnsTheToken)).ReturnsAsync(new List<Claim>());

            userManager.Setup(u => u.GetRolesAsync(userWhoOwnsTheToken)).ReturnsAsync(new List<string>());

            return userManager.Object;
        }
    }
}