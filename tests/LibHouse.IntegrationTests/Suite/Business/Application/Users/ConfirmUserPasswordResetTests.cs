using LibHouse.Business.Application.Users;
using LibHouse.Business.Application.Users.Gateways;
using LibHouse.Business.Application.Users.Inputs;
using LibHouse.Business.Application.Users.Outputs;
using LibHouse.Business.Notifiers;
using LibHouse.Infrastructure.Authentication.Context;
using LibHouse.Infrastructure.Authentication.Password;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LibHouse.IntegrationTests.Suite.Business.Application.Users
{
    public class ConfirmUserPasswordResetTests
    {
        [Fact]
        public async Task ExecuteAsync_UserThatRequestedPasswordReset_ShouldConfirmUserPasswordReset()
        {
            string userEmail = "paulo.antunes@hotmail.com";
            string userNewPassword = "Senh@1234567";
            string userPasswordResetToken = Guid.NewGuid().ToString();
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
            };
            Notifier notifier = new();
            AuthenticationContext authenticationContext = new(new DbContextOptionsBuilder<AuthenticationContext>().UseInMemoryDatabase("InMemoryAuthentication").Options);
            IUserStore<IdentityUser> userStore = new UserStore<IdentityUser>(authenticationContext);
            Mock<UserManager<IdentityUser>> userManager = new(userStore, null, new PasswordHasher<IdentityUser>(), null, null, null, null, null, null);
            userManager.Setup(u => u.FindByEmailAsync(userEmail)).ReturnsAsync(identityUser);
            userManager.Setup(u => u.ResetPasswordAsync(identityUser, userPasswordResetToken, userNewPassword)).ReturnsAsync(IdentityResult.Success);
            IConfirmUserPasswordResetGateway confirmUserPasswordResetGateway = new IdentityConfirmUserPasswordResetGateway(userManager.Object);
            ConfirmUserPasswordReset confirmUserPasswordReset = new(notifier, confirmUserPasswordResetGateway);
            OutputConfirmUserPasswordReset output = await confirmUserPasswordReset.ExecuteAsync(
                new InputConfirmUserPasswordReset(userEmail, userNewPassword, userPasswordResetToken)
            );
            Assert.True(output.IsSuccess);
        }

        [Fact]
        public void ExecuteAsync_UserThatNotRequestedPasswordReset_ShouldNotConfirmUserPasswordReset()
        {
            string userEmail = "leila.garcia@hotmail.com";
            string userNewPassword = "Senh@1234567";
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
            };
            Notifier notifier = new();
            AuthenticationContext authenticationContext = new(new DbContextOptionsBuilder<AuthenticationContext>().UseInMemoryDatabase("InMemoryAuthentication").Options);
            IUserStore<IdentityUser> userStore = new UserStore<IdentityUser>(authenticationContext);
            Mock<UserManager<IdentityUser>> userManager = new(userStore, null, new PasswordHasher<IdentityUser>(), null, null, null, null, null, null);
            userManager.Setup(u => u.FindByEmailAsync(userEmail)).ReturnsAsync(identityUser);
            userManager.Setup(u => u.ResetPasswordAsync(identityUser, string.Empty, userNewPassword)).ReturnsAsync(
                IdentityResult.Failed(new IdentityError() { Description = "O usuário não solicitou a troca de senha" }));
            IConfirmUserPasswordResetGateway confirmUserPasswordResetGateway = new IdentityConfirmUserPasswordResetGateway(userManager.Object);
            ConfirmUserPasswordReset confirmUserPasswordReset = new(notifier, confirmUserPasswordResetGateway);
            Assert.ThrowsAnyAsync<Exception>(async() => await confirmUserPasswordReset.ExecuteAsync(
                new InputConfirmUserPasswordReset(userEmail, userNewPassword, PasswordResetToken: string.Empty)
            ));
        }
    }
}