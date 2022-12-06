using LibHouse.Business.Application.Users;
using LibHouse.Business.Application.Users.Gateways;
using LibHouse.Business.Application.Users.Inputs;
using LibHouse.Business.Application.Users.Outputs;
using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Shared;
using LibHouse.Business.Entities.Users;
using LibHouse.Business.Notifiers;
using LibHouse.Data.Context;
using LibHouse.Data.Extensions.Context;
using LibHouse.Data.Repositories.Users;
using LibHouse.Data.Transactions;
using LibHouse.Infrastructure.Authentication.Context;
using LibHouse.Infrastructure.Authentication.Register;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LibHouse.IntegrationTests.Suite.Business.Application.Users
{
    [Collection("Business.Application")]
    public class ConfirmUserRegistrationTests
    {
        private readonly IConfiguration _testsConfiguration;

        public ConfirmUserRegistrationTests()
        {
            _testsConfiguration = new ConfigurationBuilder().AddJsonFile("appsettings.Tests.json").Build();
        }

        [Fact]
        public async Task ExecuteAsync_UserWithoutConfirmation_ShouldConfirmUserRegistration()
        {
            string userEmail = "antonio-pereira@gmail.com";
            Notifier notifier = new();
            string connectionString = _testsConfiguration.GetSection("ConnectionStrings:LibHouseBusiness").Value;
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseSqlServer(connectionString).Options);
            await libHouseContext.CleanContextDataAsync();
            IUserRepository userRepository = new UserRepository(libHouseContext);
            IUnitOfWork unitOfWork = new UnitOfWork(libHouseContext);
            AuthenticationContext authenticationContext = new(new DbContextOptionsBuilder<AuthenticationContext>().UseInMemoryDatabase("InMemoryAuthentication").Options);
            IUserStore<IdentityUser> userStore = new UserStore<IdentityUser>(authenticationContext);
            Mock<UserManager<IdentityUser>> userManager = new(userStore, null, new PasswordHasher<IdentityUser>(), null, null, null, null, null, null);
            userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(new IdentityUser(userEmail));
            userManager.Setup(u => u.ConfirmEmailAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            IConfirmUserRegistrationGateway confirmUserRegistrationGateway = new IdentityConfirmUserRegistrationGateway(userManager.Object);
            ConfirmUserRegistration confirmUserRegistration = new(notifier, unitOfWork, confirmUserRegistrationGateway);
            authenticationContext.Users.Add(new IdentityUser() { 
                Id = Guid.NewGuid().ToString(),
                Email = userEmail,
                EmailConfirmed = false,
                NormalizedEmail = userEmail.ToUpper(),
                UserName = userEmail,
                NormalizedUserName = userEmail.ToUpper(),
                PasswordHash = "AQAAAAEAACcQAAAAEGGXbR8FPdApGw6YRB6r95b1wYvOrJkdFCAhpL5t6a3f6E/NtB2YWLifYx5sBpyltw==",
                SecurityStamp = "74LC5CVBBAZCQKTVJFW7VTZTF6UJMQZN",
                ConcurrencyStamp = "97d52be0-5c20-41a4-9ccd-c36f75eb8926",
                LockoutEnabled = true
            });
            await authenticationContext.SaveChangesAsync();
            User user = new Resident("Antonio", "Pereira", new DateTime(2000, 8, 12), Gender.Male, "11975678421", userEmail, "16677665038");
            user.Inactivate();
            await libHouseContext.Users.AddAsync(user);
            await libHouseContext.SaveChangesAsync();
            InputConfirmUserRegistration input = new(RegistrationToken: Guid.NewGuid().ToString(), UserEmail: userEmail, UserId: user.Id);
            OutputConfirmUserRegistration output = await confirmUserRegistration.ExecuteAsync(input);
            Assert.True(output.IsSuccess);
        }

        [Fact]
        public async Task ExecuteAsync_UserAlreadyConfirmed_ShouldConfirmUserRegistration()
        {
            Notifier notifier = new();
            string connectionString = _testsConfiguration.GetSection("ConnectionStrings:LibHouseBusiness").Value;
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseSqlServer(connectionString).Options);
            await libHouseContext.CleanContextDataAsync();
            IUserRepository userRepository = new UserRepository(libHouseContext);
            IUnitOfWork unitOfWork = new UnitOfWork(libHouseContext);
            AuthenticationContext authenticationContext = new(new DbContextOptionsBuilder<AuthenticationContext>().UseInMemoryDatabase("InMemoryAuthentication").Options);
            IUserStore<IdentityUser> userStore = new UserStore<IdentityUser>(authenticationContext);
            Mock<UserManager<IdentityUser>> userManager = new(userStore, null, new PasswordHasher<IdentityUser>(), null, null, null, null, null, null);
            IConfirmUserRegistrationGateway confirmUserRegistrationGateway = new IdentityConfirmUserRegistrationGateway(userManager.Object);
            ConfirmUserRegistration confirmUserRegistration = new(notifier, unitOfWork, confirmUserRegistrationGateway);
            string userEmail = "leila-araujo@gmail.com";
            authenticationContext.Users.Add(new IdentityUser()
            {
                Id = Guid.NewGuid().ToString(),
                Email = userEmail,
                EmailConfirmed = false,
                NormalizedEmail = userEmail.ToUpper(),
                UserName = userEmail,
                NormalizedUserName = userEmail.ToUpper(),
                PasswordHash = "AQAAAAEAACcQAAAAEGGXbR8FPdApGw6YRB6r95b1wYvOrJkdFCAhpL5t6a3f6E/NtB2YWLifYx5sBpyltw==",
                SecurityStamp = "74LC5CVBBAZCQKTVJFW7VTZTF6UJMQZN",
                ConcurrencyStamp = "97d52be0-5c20-41a4-9ccd-c36f75eb8926",
                LockoutEnabled = true
            });
            await authenticationContext.SaveChangesAsync();
            User user = new Resident("Leila", "Araujo", new DateTime(2005, 9, 20), Gender.Female, "21975678421", userEmail, "26419782023");
            user.Activate();
            await libHouseContext.Users.AddAsync(user);
            await libHouseContext.SaveChangesAsync();
            InputConfirmUserRegistration input = new(RegistrationToken: Guid.NewGuid().ToString(), UserEmail: userEmail, UserId: user.Id);
            OutputConfirmUserRegistration output = await confirmUserRegistration.ExecuteAsync(input);
            Assert.True(output.IsSuccess);
        }

        [Fact]
        public async Task ExecuteAsync_UserNotRegistered_ShouldNotConfirmUserRegistration()
        {
            Notifier notifier = new();
            string connectionString = _testsConfiguration.GetSection("ConnectionStrings:LibHouseBusiness").Value;
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseSqlServer(connectionString).Options);
            await libHouseContext.CleanContextDataAsync();
            IUserRepository userRepository = new UserRepository(libHouseContext);
            IUnitOfWork unitOfWork = new UnitOfWork(libHouseContext);
            AuthenticationContext authenticationContext = new(new DbContextOptionsBuilder<AuthenticationContext>().UseInMemoryDatabase("InMemoryAuthentication").Options);
            IUserStore<IdentityUser> userStore = new UserStore<IdentityUser>(authenticationContext);
            Mock<UserManager<IdentityUser>> userManager = new(userStore, null, new PasswordHasher<IdentityUser>(), null, null, null, null, null, null);
            IConfirmUserRegistrationGateway confirmUserRegistrationGateway = new IdentityConfirmUserRegistrationGateway(userManager.Object);
            ConfirmUserRegistration confirmUserRegistration = new(notifier, unitOfWork, confirmUserRegistrationGateway);
            string userEmail = "emilio-morgado@hotmail.com";
            InputConfirmUserRegistration input = new(RegistrationToken: Guid.NewGuid().ToString(), UserEmail: userEmail, UserId: Guid.NewGuid());
            OutputConfirmUserRegistration output = await confirmUserRegistration.ExecuteAsync(input);
            Assert.False(output.IsSuccess);
        }

        [Fact]
        public async Task ExecuteAsync_UserWithInvalidRegistrationToken_ShouldNotConfirmUserRegistration()
        {
            string userEmail = "jaqueline.silva@gmail.com";
            Notifier notifier = new();
            string connectionString = _testsConfiguration.GetSection("ConnectionStrings:LibHouseBusiness").Value;
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseSqlServer(connectionString).Options);
            await libHouseContext.CleanContextDataAsync();
            IUserRepository userRepository = new UserRepository(libHouseContext);
            IUnitOfWork unitOfWork = new UnitOfWork(libHouseContext);
            AuthenticationContext authenticationContext = new(new DbContextOptionsBuilder<AuthenticationContext>().UseInMemoryDatabase("InMemoryAuthentication").Options);
            IUserStore<IdentityUser> userStore = new UserStore<IdentityUser>(authenticationContext);
            Mock<UserManager<IdentityUser>> userManager = new(userStore, null, new PasswordHasher<IdentityUser>(), null, null, null, null, null, null);
            userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(new IdentityUser(userEmail));
            userManager.Setup(u => u.ConfirmEmailAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed(new IdentityError() { Description = "Token não encontrado" }));
            IConfirmUserRegistrationGateway confirmUserRegistrationGateway = new IdentityConfirmUserRegistrationGateway(userManager.Object);
            ConfirmUserRegistration confirmUserRegistration = new(notifier, unitOfWork, confirmUserRegistrationGateway);
            authenticationContext.Users.Add(new IdentityUser()
            {
                Id = Guid.NewGuid().ToString(),
                Email = userEmail,
                EmailConfirmed = false,
                NormalizedEmail = userEmail.ToUpper(),
                UserName = userEmail,
                NormalizedUserName = userEmail.ToUpper(),
                PasswordHash = "AQAAAAEAACcQAAAAEGGXbR8FPdApGw6YRB6r95b1wYvOrJkdFCAhpL5t6a3f6E/NtB2YWLifYx5sBpyltw==",
                SecurityStamp = "74LC5CVBBAZCQKTVJFW7VTZTF6UJMQZN",
                ConcurrencyStamp = "97d52be0-5c20-41a4-9ccd-c36f75eb8926",
                LockoutEnabled = true
            });
            await authenticationContext.SaveChangesAsync();
            User user = new Resident("Jaqueline", "Silva", new DateTime(1980, 5, 17), Gender.Female, "18973175431", userEmail, "42152552016");
            user.Inactivate();
            await libHouseContext.Users.AddAsync(user);
            await libHouseContext.SaveChangesAsync();
            InputConfirmUserRegistration input = new(RegistrationToken: Guid.NewGuid().ToString(), UserEmail: userEmail, UserId: user.Id);
            OutputConfirmUserRegistration output = await confirmUserRegistration.ExecuteAsync(input);
            Assert.False(output.IsSuccess);
        }
    }
}