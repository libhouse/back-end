using LibHouse.Business.Monads;
using LibHouse.Infrastructure.Authentication.Context;
using LibHouse.Infrastructure.Authentication.Token.Models;
using LibHouse.Infrastructure.Authentication.Token.Validations.RefreshTokens;
using LibHouse.IntegrationTests.Setup.Suite.Infrastructure.Authentication.Token.Validations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace LibHouse.IntegrationTests.Suite.Infrastructure.Authentication.Token.Validations
{
    public class RefreshTokenValidatorTests
    {
        private readonly IRefreshTokenValidator _refreshTokenValidator;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public RefreshTokenValidatorTests()
        {
            _refreshTokenValidator = new RefreshTokenValidator();
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        [Fact]
        public async Task CheckIfRefreshTokenCanBeUsedWithAccessToken_ValidRefreshTokenWithExpiredAccessToken_ReturnsSuccess()
        {
            IdentityUser userWhoOwnsTheToken = new() { Id = Guid.NewGuid().ToString(), Email = "lucas.dirani@gmail.com", EmailConfirmed = true };
            int tokenExpirationInSeconds = 1;
            AuthenticationContext authenticationContext = new(new DbContextOptionsBuilder<AuthenticationContext>().UseInMemoryDatabase("InMemoryAuthentication").Options);
            var tokenGenerator = SetupRefreshTokenValidatorTests.SetupTokenGenerator(userWhoOwnsTheToken, authenticationContext, tokenExpirationInSeconds);
            TokenValidationParameters tokenValidationParams = SetupRefreshTokenValidatorTests.SetupTokenValidationParameters();
            AccessToken userToken = await tokenGenerator.GenerateAccessTokenAsync(userWhoOwnsTheToken.Email);
            ClaimsPrincipal accessTokenClaims = _tokenHandler.ValidateToken(userToken.Value, tokenValidationParams, out var validatedToken);
            RefreshToken refreshToken = SetupRefreshTokenValidatorTests.SetupRefreshToken(validatedToken, userWhoOwnsTheToken);
            await AwaitForAccessTokenExpire(tokenExpirationInSeconds);
            Result refreshTokenValidation = _refreshTokenValidator.CheckIfRefreshTokenCanBeUsedWithAccessToken(refreshToken, validatedToken, accessTokenClaims);
            Assert.True(refreshTokenValidation.IsSuccess);
        }

        [Fact]
        public async Task CheckIfRefreshTokenCanBeUsedWithAccessToken_ValidRefreshTokenWithNonExpiredAccessToken_ReturnsFailure()
        {
            IdentityUser userWhoOwnsTheToken = new() { Id = Guid.NewGuid().ToString(), Email = "lucas.dirani@gmail.com", EmailConfirmed = true };
            int tokenExpirationInSeconds = 600;
            AuthenticationContext authenticationContext = new(new DbContextOptionsBuilder<AuthenticationContext>().UseInMemoryDatabase("InMemoryAuthentication").Options);
            var tokenGenerator = SetupRefreshTokenValidatorTests.SetupTokenGenerator(userWhoOwnsTheToken, authenticationContext, tokenExpirationInSeconds);
            TokenValidationParameters tokenValidationParams = SetupRefreshTokenValidatorTests.SetupTokenValidationParameters();
            AccessToken userToken = await tokenGenerator.GenerateAccessTokenAsync(userWhoOwnsTheToken.Email);
            ClaimsPrincipal accessTokenClaims = _tokenHandler.ValidateToken(userToken.Value, tokenValidationParams, out var validatedToken);
            RefreshToken refreshToken = SetupRefreshTokenValidatorTests.SetupRefreshToken(validatedToken, userWhoOwnsTheToken);
            Result refreshTokenValidation = _refreshTokenValidator.CheckIfRefreshTokenCanBeUsedWithAccessToken(refreshToken, validatedToken, accessTokenClaims);
            Assert.True(refreshTokenValidation.Failure);
        }

        [Fact]
        public async Task CheckIfRefreshTokenCanBeUsedWithAccessToken_UsedRefreshToken_ReturnsFailure()
        {
            IdentityUser userWhoOwnsTheToken = new() { Id = Guid.NewGuid().ToString(), Email = "lucas.dirani@gmail.com", EmailConfirmed = true };
            int tokenExpirationInSeconds = 1;
            AuthenticationContext authenticationContext = new(new DbContextOptionsBuilder<AuthenticationContext>().UseInMemoryDatabase("InMemoryAuthentication").Options);
            var tokenGenerator = SetupRefreshTokenValidatorTests.SetupTokenGenerator(userWhoOwnsTheToken, authenticationContext, tokenExpirationInSeconds);
            TokenValidationParameters tokenValidationParams = SetupRefreshTokenValidatorTests.SetupTokenValidationParameters();
            AccessToken userToken = await tokenGenerator.GenerateAccessTokenAsync(userWhoOwnsTheToken.Email);
            ClaimsPrincipal accessTokenClaims = _tokenHandler.ValidateToken(userToken.Value, tokenValidationParams, out var validatedToken);
            RefreshToken refreshToken = SetupRefreshTokenValidatorTests.SetupRefreshToken(validatedToken, userWhoOwnsTheToken, isUsed: true);
            await AwaitForAccessTokenExpire(tokenExpirationInSeconds);
            Result refreshTokenValidation = _refreshTokenValidator.CheckIfRefreshTokenCanBeUsedWithAccessToken(refreshToken, validatedToken, accessTokenClaims);
            Assert.True(refreshTokenValidation.Failure);
        }

        [Fact]
        public async Task CheckIfRefreshTokenCanBeUsedWithAccessToken_RevokedRefreshToken_ReturnsFailure()
        {
            IdentityUser userWhoOwnsTheToken = new() { Id = Guid.NewGuid().ToString(), Email = "lucas.dirani@gmail.com", EmailConfirmed = true };
            int tokenExpirationInSeconds = 1;
            AuthenticationContext authenticationContext = new(new DbContextOptionsBuilder<AuthenticationContext>().UseInMemoryDatabase("InMemoryAuthentication").Options);
            var tokenGenerator = SetupRefreshTokenValidatorTests.SetupTokenGenerator(userWhoOwnsTheToken, authenticationContext, tokenExpirationInSeconds);
            TokenValidationParameters tokenValidationParams = SetupRefreshTokenValidatorTests.SetupTokenValidationParameters();
            AccessToken userToken = await tokenGenerator.GenerateAccessTokenAsync(userWhoOwnsTheToken.Email);
            ClaimsPrincipal accessTokenClaims = _tokenHandler.ValidateToken(userToken.Value, tokenValidationParams, out var validatedToken);
            RefreshToken refreshToken = SetupRefreshTokenValidatorTests.SetupRefreshToken(validatedToken, userWhoOwnsTheToken, isRevoked: true);
            await AwaitForAccessTokenExpire(tokenExpirationInSeconds);
            Result refreshTokenValidation = _refreshTokenValidator.CheckIfRefreshTokenCanBeUsedWithAccessToken(refreshToken, validatedToken, accessTokenClaims);
            Assert.True(refreshTokenValidation.Failure);
        }

        [Fact]
        public async Task CheckIfRefreshTokenCanBeUsedWithAccessToken_RefreshTokenAndAccessTokenDontMatch_ReturnsFailure()
        {
            IdentityUser userWhoOwnsTheToken = new() { Id = Guid.NewGuid().ToString(), Email = "lucas.dirani@gmail.com", EmailConfirmed = true };
            int tokenExpirationInSeconds = 1;
            AuthenticationContext authenticationContext = new(new DbContextOptionsBuilder<AuthenticationContext>().UseInMemoryDatabase("InMemoryAuthentication").Options);
            var tokenGenerator = SetupRefreshTokenValidatorTests.SetupTokenGenerator(userWhoOwnsTheToken, authenticationContext, tokenExpirationInSeconds);
            TokenValidationParameters tokenValidationParams = SetupRefreshTokenValidatorTests.SetupTokenValidationParameters();
            AccessToken userToken = await tokenGenerator.GenerateAccessTokenAsync(userWhoOwnsTheToken.Email);
            ClaimsPrincipal accessTokenClaims = _tokenHandler.ValidateToken(userToken.Value, tokenValidationParams, out var validatedToken);
            RefreshToken refreshToken = SetupRefreshTokenValidatorTests.SetupRefreshToken(validatedToken, userWhoOwnsTheToken, jwtId: Guid.NewGuid().ToString());
            await AwaitForAccessTokenExpire(tokenExpirationInSeconds);
            Result refreshTokenValidation = _refreshTokenValidator.CheckIfRefreshTokenCanBeUsedWithAccessToken(refreshToken, validatedToken, accessTokenClaims);
            Assert.True(refreshTokenValidation.Failure);
        }

        [Fact]
        public async Task CheckIfRefreshTokenCanBeUsedWithAccessToken_ExpiredRefreshToken_ReturnsFailure()
        {
            IdentityUser userWhoOwnsTheToken = new() { Id = Guid.NewGuid().ToString(), Email = "lucas.dirani@gmail.com", EmailConfirmed = true };
            int tokenExpirationInSeconds = 1;
            AuthenticationContext authenticationContext = new(new DbContextOptionsBuilder<AuthenticationContext>().UseInMemoryDatabase("InMemoryAuthentication").Options);
            var tokenGenerator = SetupRefreshTokenValidatorTests.SetupTokenGenerator(userWhoOwnsTheToken, authenticationContext, tokenExpirationInSeconds);
            TokenValidationParameters tokenValidationParams = SetupRefreshTokenValidatorTests.SetupTokenValidationParameters();
            AccessToken userToken = await tokenGenerator.GenerateAccessTokenAsync(userWhoOwnsTheToken.Email);
            ClaimsPrincipal accessTokenClaims = _tokenHandler.ValidateToken(userToken.Value, tokenValidationParams, out var validatedToken);
            DateTime refreshTokenCreatedAt = DateTime.UtcNow.AddDays(-30);
            DateTime refreshTokenExpiresIn = DateTime.UtcNow.AddDays(-1);
            RefreshToken refreshToken = SetupRefreshTokenValidatorTests.SetupRefreshToken(validatedToken, userWhoOwnsTheToken, refreshTokenCreatedAt, refreshTokenExpiresIn);
            await AwaitForAccessTokenExpire(tokenExpirationInSeconds);
            Result refreshTokenValidation = _refreshTokenValidator.CheckIfRefreshTokenCanBeUsedWithAccessToken(refreshToken, validatedToken, accessTokenClaims);
            Assert.True(refreshTokenValidation.Failure);
        }

        private static async Task AwaitForAccessTokenExpire(double tokenExpiresIn)
        {
            await Task.Delay(TimeSpan.FromSeconds(tokenExpiresIn));
        }
    }
}