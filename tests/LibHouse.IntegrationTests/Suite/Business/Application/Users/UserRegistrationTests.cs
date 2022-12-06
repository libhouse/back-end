﻿using LibHouse.Business.Application.Users;
using LibHouse.Business.Application.Users.Gateways;
using LibHouse.Business.Application.Users.Inputs;
using LibHouse.Business.Application.Users.Outputs;
using LibHouse.Business.Application.Users.Senders;
using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Shared;
using LibHouse.Business.Entities.Users;
using LibHouse.Business.Notifiers;
using LibHouse.Business.Validations.Users;
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
    public class UserRegistrationTests
    {
        private readonly IConfiguration _testsConfiguration;

        public UserRegistrationTests()
        {
            _testsConfiguration = new ConfigurationBuilder().AddJsonFile("appsettings.Tests.json").Build();
        }

        [Fact]
        public async Task ExecuteAsync_NewUser_ShouldRegisterUser()
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
            userManager.Setup(u => u.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            userManager.Setup(u => u.AddToRoleAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()));
            userManager.Setup(u => u.GenerateEmailConfirmationTokenAsync(It.IsAny<IdentityUser>())).ReturnsAsync(Guid.NewGuid().ToString());
            IUserRegistrationGateway userRegistrationGateway = new IdentityUserRegistrationGateway(userManager.Object);
            UserRegistrationValidator userValidator = new(userRepository);
            Mock<IUserRegistrationSender> userRegistrationSender = new();
            userRegistrationSender.Setup(u => u.SendUserRegistrationDataAsync(It.IsAny<InputUserRegistrationSender>())).ReturnsAsync(new OutputUserRegistrationSender(IsSuccess: true));
            UserRegistration userRegistration = new(notifier, unitOfWork, userRegistrationGateway, userRegistrationSender.Object, userValidator);
            InputUserRegistration input = new("Lucas", "Dirani", new DateTime(1998, 8, 12), "Male", "11985267981", "lucas.dirani@gmail.com", "45010453029", "Resident", "Senh@123456");
            OutputUserRegistration output = await userRegistration.ExecuteAsync(input);
            Assert.True(output.IsSuccess);
        }

        [Fact]
        public async Task ExecuteAsync_RepeatedUser_ShouldNotRegisterUser()
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
            userManager.Setup(u => u.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            userManager.Setup(u => u.AddToRoleAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()));
            userManager.Setup(u => u.GenerateEmailConfirmationTokenAsync(It.IsAny<IdentityUser>())).ReturnsAsync(Guid.NewGuid().ToString());
            IUserRegistrationGateway userRegistrationGateway = new IdentityUserRegistrationGateway(userManager.Object);
            UserRegistrationValidator userValidator = new(userRepository);
            Mock<IUserRegistrationSender> userRegistrationSender = new();
            userRegistrationSender.Setup(u => u.SendUserRegistrationDataAsync(It.IsAny<InputUserRegistrationSender>())).ReturnsAsync(new OutputUserRegistrationSender(IsSuccess: true));
            UserRegistration userRegistration = new(notifier, unitOfWork, userRegistrationGateway, userRegistrationSender.Object, userValidator);
            Resident existingUser = new("Matheus", "Jesus", new DateTime(1999, 5, 15), Gender.Male, "11986418080", "matheus.jesus@gmail.com", "53354503041");
            existingUser.Activate();
            await unitOfWork.StartWorkAsync();
            await unitOfWork.UserRepository.AddAsync(existingUser);
            await unitOfWork.CommitAsync();
            InputUserRegistration input = new("Matheus", "Jesus", new DateTime(1999, 5, 15), "Male", "11986418080", "matheus.jesus@gmail.com", "53354503041", "Resident", "Senh@123456");
            OutputUserRegistration output = await userRegistration.ExecuteAsync(input);
            Assert.False(output.IsSuccess);
        }
    }
}