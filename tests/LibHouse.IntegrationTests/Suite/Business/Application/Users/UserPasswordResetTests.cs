using LibHouse.Business.Application.Users;
using LibHouse.Business.Application.Users.Gateways;
using LibHouse.Business.Application.Users.Outputs;
using LibHouse.Business.Application.Users.Senders;
using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Users;
using LibHouse.Business.Notifiers;
using LibHouse.Data.Context;
using LibHouse.Data.Repositories.Users;
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
    [Collection("Business.Application")]
    public class UserPasswordResetTests
    {
        [Fact]
        public async Task ExecuteAsync_RegisteredUser_ShouldRequestPasswordReset()
        {
            string userCpf = "79215284060";
            string userEmail = "jose-vessoni@gmail.com";
            Notifier notifier = new();
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseInMemoryDatabase("InMemoryLibHouse").Options);
            IUserRepository userRepository = new UserRepository(libHouseContext);
            AuthenticationContext authenticationContext = new(new DbContextOptionsBuilder<AuthenticationContext>().UseInMemoryDatabase("InMemoryAuthentication").Options);
            IUserStore<IdentityUser> userStore = new UserStore<IdentityUser>(authenticationContext);
            Mock<UserManager<IdentityUser>> userManager = new(userStore, null, new PasswordHasher<IdentityUser>(), null, null, null, null, null, null);
            userManager.Setup(u => u.FindByEmailAsync(userEmail)).ReturnsAsync(new IdentityUser
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
            });
            userManager.Setup(u => u.GeneratePasswordResetTokenAsync(It.IsAny<IdentityUser>())).ReturnsAsync(Guid.NewGuid().ToString());
            IUserPasswordResetGateway userPasswordResetGateway = new IdentityUserPasswordResetGateway(userManager.Object);
            Mock<IUserPasswordResetSender> userPasswordResetSender = new();
            userPasswordResetSender.Setup(u => u.SendUserPasswordResetRequestAsync(It.IsAny<InputUserPasswordResetSender>())).ReturnsAsync(new OutputUserPasswordResetSender(IsSuccess: true));
            UserPasswordReset userPasswordReset = new(notifier, userRepository, userPasswordResetGateway, userPasswordResetSender.Object);
            Resident registeredUser = new("José", "Vessoni", new DateTime(1970, 2, 10), Gender.Male, "(11) 99951-8087", userEmail, userCpf);
            registeredUser.Activate();
            await libHouseContext.Users.AddAsync(registeredUser);
            await libHouseContext.SaveChangesAsync();
            OutputUserPasswordReset output = await userPasswordReset.ExecuteAsync(new(userCpf));
            Assert.True(output.IsSuccess);
        }

        [Fact]
        public async Task ExecuteAsync_UnregisteredUser_ShouldNotRequestPasswordReset()
        {
            string userCpf = "27736365032";
            Notifier notifier = new();
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseInMemoryDatabase("InMemoryLibHouse").Options);
            IUserRepository userRepository = new UserRepository(libHouseContext);
            AuthenticationContext authenticationContext = new(new DbContextOptionsBuilder<AuthenticationContext>().UseInMemoryDatabase("InMemoryAuthentication").Options);
            IUserStore<IdentityUser> userStore = new UserStore<IdentityUser>(authenticationContext);
            Mock<UserManager<IdentityUser>> userManager = new(userStore, null, new PasswordHasher<IdentityUser>(), null, null, null, null, null, null);
            IUserPasswordResetGateway userPasswordResetGateway = new IdentityUserPasswordResetGateway(userManager.Object);
            Mock<IUserPasswordResetSender> userPasswordResetSender = new();
            UserPasswordReset userPasswordReset = new(notifier, userRepository, userPasswordResetGateway, userPasswordResetSender.Object);
            OutputUserPasswordReset output = await userPasswordReset.ExecuteAsync(new(userCpf));
            Assert.False(output.IsSuccess);
        }
    }
}