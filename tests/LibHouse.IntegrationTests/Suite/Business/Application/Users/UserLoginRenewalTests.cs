using LibHouse.Business.Application.Users;
using LibHouse.Business.Application.Users.Gateways;
using LibHouse.Business.Application.Users.Outputs;
using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Users;
using LibHouse.Business.Notifiers;
using LibHouse.Data.Context;
using LibHouse.Data.Repositories.Users;
using LibHouse.Infrastructure.Authentication.Context;
using LibHouse.Infrastructure.Authentication.Login;
using LibHouse.Infrastructure.Authentication.Roles;
using LibHouse.Infrastructure.Authentication.Token.Generators.AccessTokens;
using LibHouse.Infrastructure.Authentication.Token.Generators.RefreshTokens;
using LibHouse.Infrastructure.Authentication.Token.Models;
using LibHouse.Infrastructure.Authentication.Token.Services.RefreshTokens;
using LibHouse.Infrastructure.Authentication.Token.Settings;
using LibHouse.Infrastructure.Authentication.Token.Validations.RefreshTokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibHouse.IntegrationTests.Suite.Business.Application.Users
{
    public class UserLoginRenewalTests
    {
        [Fact]
        public async Task ExecuteAsync_ExpiredLogin_ShouldRenewLogin()
        {
            string userEmail = "ben-johnson@gmail.com";
            string accessTokenSecret = "AccessTokenSecretForIntegrationTest";
            string accessTokenIssuer = "LibHouseAPI";
            string accessTokenAudience = "https://localhost";
            int accessTokenExpiresInSeconds = 5;
            IdentityUser identityUser = new()
            {
                Id = Guid.NewGuid().ToString(),
                Email = userEmail,
                EmailConfirmed = true,
                NormalizedEmail = userEmail.ToUpper(),
                UserName = userEmail,
                NormalizedUserName = userEmail.ToUpper(),
                PasswordHash = "AQAAAAEAACcQAAAAEGGXbR8FPdApGw6YRB6r95b1wYvOrJkdFCAhpL5t6a3f6E/NtB2YWLifYx5sBpyltw==",
                SecurityStamp = "74LC5CVBBAZCQKTVJFW7VTZTF6UJMQZN",
                ConcurrencyStamp = "97d52be0-5c20-41a4-9ccd-c36f75eb8926",
                LockoutEnabled = true
            };
            Notifier notifier = new();
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseInMemoryDatabase("InMemoryLibHouse").Options);
            IUserRepository userRepository = new UserRepository(libHouseContext);
            TokenValidationParameters tokenParameters = new()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(accessTokenSecret)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidAudience = accessTokenAudience,
                ValidIssuer = accessTokenIssuer,
            };
            AuthenticationContext authenticationContext = new(new DbContextOptionsBuilder<AuthenticationContext>().UseInMemoryDatabase("InMemoryAuthentication").Options);
            IUserStore<IdentityUser> userStore = new UserStore<IdentityUser>(authenticationContext);
            Mock<UserManager<IdentityUser>> userManager = new(userStore, null, new PasswordHasher<IdentityUser>(), null, null, null, null, null, null);
            userManager.Setup(u => u.FindByEmailAsync(userEmail)).ReturnsAsync(identityUser);
            userManager.Setup(u => u.GetClaimsAsync(identityUser)).ReturnsAsync(new List<Claim>());
            userManager.Setup(u => u.GetRolesAsync(identityUser)).ReturnsAsync(new List<string>() { LibHouseUserRole.User, LibHouseUserRole.Resident });
            AccessTokenSettings accessTokenSettings = new() { ExpiresInSeconds = accessTokenExpiresInSeconds, Issuer = accessTokenIssuer, Secret = accessTokenSecret, ValidIn = accessTokenAudience };
            RefreshTokenSettings refreshTokenSettings = new() { ExpiresInMonths = 3, TokenLength = 35 };
            IRefreshTokenGenerator refreshTokenGenerator = new JwtRefreshTokenGenerator(userManager.Object, authenticationContext, refreshTokenSettings);
            IAccessTokenGenerator accessTokenGenerator = new JwtAccessTokenGenerator(userManager.Object, accessTokenSettings, refreshTokenGenerator);
            IUserLoginRenewalGateway userLoginRenewalGateway = new IdentityUserLoginRenewalGateway(
                tokenParameters, 
                accessTokenGenerator, 
                new IdentityRefreshTokenService(authenticationContext),
                new RefreshTokenValidator()
            );
            UserLoginRenewal userLoginRenewal = new(notifier, userRepository, userLoginRenewalGateway);

            authenticationContext.Users.Add(identityUser);
            await authenticationContext.SaveChangesAsync();
            User user = new Resident("Ben", "Johnson", new DateTime(1987, 8, 21), Gender.Male, "15995211490", userEmail, "13515271007");
            user.Activate();
            await libHouseContext.AddAsync(user);
            await libHouseContext.SaveChangesAsync();
            AccessToken userExpiredAccessToken = await accessTokenGenerator.GenerateAccessTokenAsync(userEmail);
            await Task.Delay(((int)TimeSpan.FromSeconds(accessTokenExpiresInSeconds).TotalMilliseconds));
            OutputUserLoginRenewal output = await userLoginRenewal.ExecuteAsync(new(
                userEmail, 
                userExpiredAccessToken.Value, 
                userExpiredAccessToken.RefreshToken.Token)
            );

            Assert.True(output.IsSuccess);
        }

        [Fact]
        public async Task ExecuteAsync_ActiveLogin_ShouldNotRenewLogin()
        {
            string userEmail = "ayrton-lucena@gmail.com";
            string accessTokenSecret = "AccessTokenSecretForIntegrationTest";
            string accessTokenIssuer = "LibHouseAPI";
            string accessTokenAudience = "https://localhost";
            int accessTokenExpiresInSeconds = 60;
            IdentityUser identityUser = new()
            {
                Id = Guid.NewGuid().ToString(),
                Email = userEmail,
                EmailConfirmed = true,
                NormalizedEmail = userEmail.ToUpper(),
                UserName = userEmail,
                NormalizedUserName = userEmail.ToUpper(),
                PasswordHash = "AQAAAAEAACcQAAAAEGGXbR8FPdApGw6YRB6r95b1wYvOrJkdFCAhpL5t6a3f6E/NtB2YWLifYx5sBpyltw==",
                SecurityStamp = "74LC5CVBBAZCQKTVJFW7VTZTF6UJMQZN",
                ConcurrencyStamp = "97d52be0-5c20-41a4-9ccd-c36f75eb8926",
                LockoutEnabled = true
            };
            Notifier notifier = new();
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseInMemoryDatabase("InMemoryLibHouse").Options);
            IUserRepository userRepository = new UserRepository(libHouseContext);
            TokenValidationParameters tokenParameters = new()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(accessTokenSecret)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidAudience = accessTokenAudience,
                ValidIssuer = accessTokenIssuer,
            };
            AuthenticationContext authenticationContext = new(new DbContextOptionsBuilder<AuthenticationContext>().UseInMemoryDatabase("InMemoryAuthentication").Options);
            IUserStore<IdentityUser> userStore = new UserStore<IdentityUser>(authenticationContext);
            Mock<UserManager<IdentityUser>> userManager = new(userStore, null, new PasswordHasher<IdentityUser>(), null, null, null, null, null, null);
            userManager.Setup(u => u.FindByEmailAsync(userEmail)).ReturnsAsync(identityUser);
            userManager.Setup(u => u.GetClaimsAsync(identityUser)).ReturnsAsync(new List<Claim>());
            userManager.Setup(u => u.GetRolesAsync(identityUser)).ReturnsAsync(new List<string>() { LibHouseUserRole.User, LibHouseUserRole.Resident });
            AccessTokenSettings accessTokenSettings = new() { ExpiresInSeconds = accessTokenExpiresInSeconds, Issuer = accessTokenIssuer, Secret = accessTokenSecret, ValidIn = accessTokenAudience };
            RefreshTokenSettings refreshTokenSettings = new() { ExpiresInMonths = 3, TokenLength = 35 };
            IRefreshTokenGenerator refreshTokenGenerator = new JwtRefreshTokenGenerator(userManager.Object, authenticationContext, refreshTokenSettings);
            IAccessTokenGenerator accessTokenGenerator = new JwtAccessTokenGenerator(userManager.Object, accessTokenSettings, refreshTokenGenerator);
            IUserLoginRenewalGateway userLoginRenewalGateway = new IdentityUserLoginRenewalGateway(
                tokenParameters,
                accessTokenGenerator,
                new IdentityRefreshTokenService(authenticationContext),
                new RefreshTokenValidator()
            );
            UserLoginRenewal userLoginRenewal = new(notifier, userRepository, userLoginRenewalGateway);

            AccessToken userActiveAccessToken = await accessTokenGenerator.GenerateAccessTokenAsync(userEmail);
            OutputUserLoginRenewal output = await userLoginRenewal.ExecuteAsync(new(
                userEmail,
                userActiveAccessToken.Value,
                userActiveAccessToken.RefreshToken.Token)
            );

            Assert.False(output.IsSuccess);
        }
    }
}