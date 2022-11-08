using LibHouse.Business.Application.Users;
using LibHouse.Business.Application.Users.Gateways;
using LibHouse.Business.Application.Users.Outputs;
using LibHouse.Business.Notifiers;
using LibHouse.Infrastructure.Authentication.Context;
using LibHouse.Infrastructure.Authentication.Login.Interfaces;
using LibHouse.Infrastructure.Authentication.Logout;
using LibHouse.Infrastructure.Authentication.Token.Models;
using LibHouse.Infrastructure.Authentication.Token.Services.RefreshTokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LibHouse.IntegrationTests.Suite.Business.Application.Users
{
    public class UserLogoutTests
    {
        [Fact]
        public async Task ExecuteAsync_LoggedUser_ShouldLogoutUser()
        {
            string userEmail = "harry-osborn@gmail.com";
            string userId = Guid.NewGuid().ToString();
            string userToken = Guid.NewGuid().ToString();
            Notifier notifier = new();
            AuthenticationContext authenticationContext = new(new DbContextOptionsBuilder<AuthenticationContext>().UseInMemoryDatabase("InMemoryAuthentication").Options);
            IRefreshTokenService refreshTokenService = new IdentityRefreshTokenService(authenticationContext);
            Mock<ILoggedUser> loggedUser = new();
            loggedUser.Setup(l => l.GetUserEmail()).Returns(userEmail);
            IUserLogoutGateway userLogoutGateway = new IdentityUserLogoutGateway(loggedUser.Object, refreshTokenService);
            UserLogout userLogout = new(notifier, userLogoutGateway);
            authenticationContext.Users.Add(new IdentityUser() {
                Email = userEmail,
                EmailConfirmed = true,
                Id = userId,
                NormalizedEmail = userEmail.ToUpper(),
                NormalizedUserName = userEmail.ToUpper(),
                UserName = userEmail,
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });
            authenticationContext.RefreshTokens.Add(new RefreshToken(
                token: userToken, 
                jwtId: Guid.NewGuid().ToString(), 
                userId, 
                createdAt: DateTime.UtcNow, 
                expiresIn: DateTime.UtcNow.AddDays(3)
            ));
            await authenticationContext.SaveChangesAsync();
            OutputUserLogout outputUserLogout = await userLogout.ExecuteAsync(new(userEmail, userToken));
            Assert.True(outputUserLogout.IsSuccess);
        }

        [Fact]
        public async Task ExecuteAsync_LogoutRequestedByAnotherUser_ShouldNotLogoutUser()
        {
            string userEmail = "maryjane@hotmail.com";
            string userId = Guid.NewGuid().ToString();
            string userToken = Guid.NewGuid().ToString();
            Notifier notifier = new();
            AuthenticationContext authenticationContext = new(new DbContextOptionsBuilder<AuthenticationContext>().UseInMemoryDatabase("InMemoryAuthentication").Options);
            IRefreshTokenService refreshTokenService = new IdentityRefreshTokenService(authenticationContext);
            Mock<ILoggedUser> loggedUser = new();
            loggedUser.Setup(l => l.GetUserEmail()).Returns("peterparker@gmail.com");
            IUserLogoutGateway userLogoutGateway = new IdentityUserLogoutGateway(loggedUser.Object, refreshTokenService);
            UserLogout userLogout = new(notifier, userLogoutGateway);
            authenticationContext.Users.Add(new IdentityUser()
            {
                Email = userEmail,
                EmailConfirmed = true,
                Id = userId,
                NormalizedEmail = userEmail.ToUpper(),
                NormalizedUserName = userEmail.ToUpper(),
                UserName = userEmail,
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });
            authenticationContext.RefreshTokens.Add(new RefreshToken(
                token: userToken,
                jwtId: Guid.NewGuid().ToString(),
                userId,
                createdAt: DateTime.UtcNow,
                expiresIn: DateTime.UtcNow.AddDays(3)
            ));
            await authenticationContext.SaveChangesAsync();
            OutputUserLogout outputUserLogout = await userLogout.ExecuteAsync(new(userEmail, userToken));
            Assert.False(outputUserLogout.IsSuccess);
        }

        [Fact]
        public async Task ExecuteAsync_UserAlreadyLoggedOut_ShouldNotLogoutUser()
        {
            string userEmail = "gwen-stacy@gmail.com";
            string userId = Guid.NewGuid().ToString();
            string userToken = Guid.NewGuid().ToString();
            Notifier notifier = new();
            AuthenticationContext authenticationContext = new(new DbContextOptionsBuilder<AuthenticationContext>().UseInMemoryDatabase("InMemoryAuthentication").Options);
            IRefreshTokenService refreshTokenService = new IdentityRefreshTokenService(authenticationContext);
            Mock<ILoggedUser> loggedUser = new();
            loggedUser.Setup(l => l.GetUserEmail()).Returns(userEmail);
            IUserLogoutGateway userLogoutGateway = new IdentityUserLogoutGateway(loggedUser.Object, refreshTokenService);
            UserLogout userLogout = new(notifier, userLogoutGateway);
            authenticationContext.Users.Add(new IdentityUser()
            {
                Email = userEmail,
                EmailConfirmed = true,
                Id = userId,
                NormalizedEmail = userEmail.ToUpper(),
                NormalizedUserName = userEmail.ToUpper(),
                UserName = userEmail,
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });
            authenticationContext.RefreshTokens.Add(new RefreshToken(
                token: userToken,
                jwtId: Guid.NewGuid().ToString(),
                userId,
                createdAt: DateTime.UtcNow.AddDays(-3),
                expiresIn: DateTime.UtcNow.AddDays(-1),
                isUsed: true,
                isRevoked: true,
                revokedAt: DateTime.UtcNow.AddDays(-1)
            ));
            await authenticationContext.SaveChangesAsync();
            OutputUserLogout outputUserLogout = await userLogout.ExecuteAsync(new(userEmail, userToken));
            Assert.False(outputUserLogout.IsSuccess);
        }
    }
}