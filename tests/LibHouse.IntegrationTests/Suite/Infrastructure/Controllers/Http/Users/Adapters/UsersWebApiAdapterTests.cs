using LibHouse.Business.Application.Users;
using LibHouse.Business.Application.Users.Gateways;
using LibHouse.Business.Application.Users.Gateways.Outputs;
using LibHouse.Business.Application.Users.Interfaces;
using LibHouse.Business.Application.Users.Senders;
using LibHouse.Business.Entities.Owners;
using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Shared;
using LibHouse.Business.Entities.Users;
using LibHouse.Business.Monads;
using LibHouse.Business.Notifiers;
using LibHouse.Business.Validations.Users;
using LibHouse.Data.Context;
using LibHouse.Data.Extensions.Context;
using LibHouse.Data.Repositories.Users;
using LibHouse.Data.Transactions;
using LibHouse.Infrastructure.Authentication.Context;
using LibHouse.Infrastructure.Authentication.Login;
using LibHouse.Infrastructure.Authentication.Password;
using LibHouse.Infrastructure.Authentication.Register;
using LibHouse.Infrastructure.Authentication.Roles;
using LibHouse.Infrastructure.Authentication.Token.Generators.AccessTokens;
using LibHouse.Infrastructure.Authentication.Token.Generators.RefreshTokens;
using LibHouse.Infrastructure.Authentication.Token.Models;
using LibHouse.Infrastructure.Authentication.Token.Services.RefreshTokens;
using LibHouse.Infrastructure.Authentication.Token.Settings;
using LibHouse.Infrastructure.Authentication.Token.Validations.RefreshTokens;
using LibHouse.Infrastructure.Controllers.Http.Users;
using LibHouse.Infrastructure.Controllers.Http.Users.Adapters;
using LibHouse.Infrastructure.Controllers.Responses.Users;
using LibHouse.Infrastructure.Controllers.ViewModels.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibHouse.IntegrationTests.Suite.Infrastructure.Controllers.Http.Users.Adapters
{
    [Collection("Infrastructure.Controllers")]
    public class UsersWebApiAdapterTests
    {
        private readonly IConfiguration _testsConfiguration;

        public UsersWebApiAdapterTests()
        {
            _testsConfiguration = new ConfigurationBuilder().AddJsonFile("appsettings.Tests.json").Build();
        }

        [Fact]
        public async Task UserRegistration_ValidUserRegistration_ShouldBeSuccess()
        {
            UsersWebApiAdapter userWebApiAdapter = new();
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
            Mock<IUserLoginRenewal> userLoginRenewal = new();
            _ = new UsersController(
                userWebApiAdapter,
                userRegistration,
                new Mock<IConfirmUserRegistration>().Object,
                new Mock<IUserLogin>().Object,
                new Mock<IUserLogout>().Object,
                new Mock<IUserLoginRenewal>().Object,
                new Mock<IUserPasswordReset>().Object,
                new Mock<IConfirmUserPasswordReset>().Object
            );
            UserRegistrationViewModel viewModel = new()
            {
                BirthDate = new DateTime(1977, 7, 12),
                ConfirmPassword = "Senh@123456",
                Cpf = "58163445017",
                Email = "julianapereira@hotmail.com",
                Gender = Gender.Female,
                LastName = "Pereira",
                Name = "Juliana",
                Password = "Senh@123456",
                Phone = "18973175431",
                UserType = UserType.Owner
            };
            Result userRegistrationResult = await userWebApiAdapter.UserRegistration(viewModel);
            Assert.True(userRegistrationResult.IsSuccess);
        }

        [Fact]
        public async Task ConfirmUserRegistration_UserWithoutConfirmation_ShouldBeSuccess()
        {
            string userEmail = "alexandre.nunes@gmail.com";
            UsersWebApiAdapter userWebApiAdapter = new();
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
            _ = new UsersController(
                userWebApiAdapter,
                new Mock<IUserRegistration>().Object,
                confirmUserRegistration,
                new Mock<IUserLogin>().Object,
                new Mock<IUserLogout>().Object,
                new Mock<IUserLoginRenewal>().Object,
                new Mock<IUserPasswordReset>().Object,
                new Mock<IConfirmUserPasswordReset>().Object
            );
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
            User user = new Resident("Alexandre", "Nunes", new DateTime(1989, 2, 15), Gender.Male, "11975678421", userEmail, "80077797019");
            user.Inactivate();
            await unitOfWork.StartWorkAsync();
            await unitOfWork.UserRepository.AddAsync(user);
            await unitOfWork.CommitAsync();
            ConfirmUserRegistrationViewModel viewModel = new()
            {
                ConfirmationToken = Guid.NewGuid().ToString(),
                UserEmail = user.GetEmailAddress(),
                UserId = user.Id
            };
            Result confirmUserRegistrationResult = await userWebApiAdapter.ConfirmUserRegistration(viewModel);
            Assert.True(confirmUserRegistrationResult.IsSuccess);
        }

        [Fact]
        public async Task UserLogin_RegisteredUser_ShouldBeSuccess()
        {
            string userEmail = "albertoayamashita@outlook.com";
            string userPassword = "Senh@123456";
            UsersWebApiAdapter userWebApiAdapter = new();
            Notifier notifier = new();
            string connectionString = _testsConfiguration.GetSection("ConnectionStrings:LibHouseBusiness").Value;
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseSqlServer(connectionString).Options);
            await libHouseContext.CleanContextDataAsync();
            IUserRepository userRepository = new UserRepository(libHouseContext);
            IUnitOfWork unitOfWork = new UnitOfWork(libHouseContext);
            AuthenticationContext authenticationContext = new(new DbContextOptionsBuilder<AuthenticationContext>().UseInMemoryDatabase("InMemoryAuthentication").Options);
            IUserStore<IdentityUser> userStore = new UserStore<IdentityUser>(authenticationContext);
            Mock<UserManager<IdentityUser>> userManager = new(userStore, null, new PasswordHasher<IdentityUser>(), null, null, null, null, null, null);
            Mock<SignInManager<IdentityUser>> signInManager = new(userManager.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<IdentityUser>>(), null, null, null, null);
            signInManager.Setup(s => s.PasswordSignInAsync(userEmail, userPassword, true, true)).ReturnsAsync(SignInResult.Success);
            Mock<IAccessTokenGenerator> accessTokenGenerator = new();
            accessTokenGenerator.Setup(a => a.GenerateAccessTokenAsync(userEmail)).ReturnsAsync(new AccessToken(
                id: Guid.NewGuid().ToString(),
                value: Guid.NewGuid().ToString(),
                expiresIn: DateTime.Today.AddMinutes(10),
                claims: Enumerable.Empty<AccessTokenClaim>(),
                refreshToken: new(
                    token: Guid.NewGuid().ToString(),
                    jwtId: Guid.NewGuid().ToString(),
                    userId: Guid.NewGuid().ToString(),
                    createdAt: DateTime.UtcNow,
                    expiresIn: DateTime.UtcNow.AddMonths(3)
                )
              )
            );
            IUserLoginGateway userLoginGateway = new IdentityUserLoginGateway(signInManager.Object, accessTokenGenerator.Object);
            UserLogin userLogin = new(notifier, userLoginGateway, userRepository);
            _ = new UsersController(
                userWebApiAdapter,
                new Mock<IUserRegistration>().Object,
                new Mock<IConfirmUserRegistration>().Object,
                userLogin,
                new Mock<IUserLogout>().Object,
                new Mock<IUserLoginRenewal>().Object,
                new Mock<IUserPasswordReset>().Object,
                new Mock<IConfirmUserPasswordReset>().Object
            );
            Owner registeredUser = new("Alberto", "Ayamashita", new DateTime(1970, 3, 25), Gender.Male, "(22) 99655-1134", userEmail, "23219514006");
            registeredUser.Activate();
            await unitOfWork.StartWorkAsync();
            await unitOfWork.UserRepository.AddAsync(registeredUser);
            await unitOfWork.CommitAsync();
            Result userLoginResult = await userWebApiAdapter.UserLogin(new UserLoginViewModel()
            {
                Email = userEmail,
                Password = userPassword
            });
            Assert.True(userLoginResult.IsSuccess);
        }

        [Fact]
        public async Task UserLogout_LoggedUser_ShouldBeSuccess()
        {
            string userEmail = "rodrigo-jesus@gmail.com";
            string userToken = Guid.NewGuid().ToString();
            Notifier notifier = new();
            UsersWebApiAdapter userWebApiAdapter = new();
            Mock<IUserLogoutGateway> userLogoutGateway = new();
            userLogoutGateway.Setup(u => u.LogoutAsync(userEmail, userToken)).ReturnsAsync(new OutputUserLogoutGateway(IsSuccess: true));
            UserLogout userLogout = new(notifier, userLogoutGateway.Object);
            _ = new UsersController(
                userWebApiAdapter,
                new Mock<IUserRegistration>().Object,
                new Mock<IConfirmUserRegistration>().Object,
                new Mock<IUserLogin>().Object,
                userLogout,
                new Mock<IUserLoginRenewal>().Object,
                new Mock<IUserPasswordReset>().Object,
                new Mock<IConfirmUserPasswordReset>().Object
            );
            Result userLogoutResult = await userWebApiAdapter.UserLogout(new() { Email = userEmail, RefreshToken = userToken });
            Assert.True(userLogoutResult.IsSuccess);
        }

        [Fact]
        public async Task UserLoginRenewal_ExpiredLogin_ShouldBeSuccess()
        {
            string userEmail = "clara-alencar@gmail.com";
            string accessTokenSecret = "AccessTokenSecretForIntegrationTest";
            string accessTokenIssuer = "LibHouseAPI";
            string accessTokenAudience = "https://localhost";
            int accessTokenExpiresInSeconds = 1;
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
            string connectionString = _testsConfiguration.GetSection("ConnectionStrings:LibHouseBusiness").Value;
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseSqlServer(connectionString).Options);
            await libHouseContext.CleanContextDataAsync();
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
            UsersWebApiAdapter userWebApiAdapter = new();
            _ = new UsersController(
                userWebApiAdapter,
                new Mock<IUserRegistration>().Object,
                new Mock<IConfirmUserRegistration>().Object,
                new Mock<IUserLogin>().Object,
                new Mock<IUserLogout>().Object,
                userLoginRenewal,
                new Mock<IUserPasswordReset>().Object,
                new Mock<IConfirmUserPasswordReset>().Object
            );
            authenticationContext.Users.Add(identityUser);
            await authenticationContext.SaveChangesAsync();
            User user = new Resident("Clara", "Alencar", new DateTime(1980, 5, 27), Gender.Female, "17995312518", userEmail, "21248528093");
            user.Activate();
            await libHouseContext.AddAsync(user);
            await libHouseContext.SaveChangesAsync();
            AccessToken userExpiredAccessToken = await accessTokenGenerator.GenerateAccessTokenAsync(userEmail);
            await Task.Delay(((int)TimeSpan.FromSeconds(accessTokenExpiresInSeconds).TotalMilliseconds));
            Result<UserLoginRenewalResponse> userLoginRenewalResult = await userWebApiAdapter.UserLoginRenewal(new() 
            { 
                Email = userEmail, 
                AccessToken = userExpiredAccessToken.Value,
                RefreshToken = userExpiredAccessToken.RefreshToken.Token 
            });
            Assert.True(userLoginRenewalResult.IsSuccess);
        }

        [Fact]
        public async Task UserPasswordReset_RegisteredUser_ShouldBeSuccess()
        {
            string userCpf = "48206098070";
            string userEmail = "marta-uchoa@gmail.com";
            Notifier notifier = new();
            string connectionString = _testsConfiguration.GetSection("ConnectionStrings:LibHouseBusiness").Value;
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseSqlServer(connectionString).Options);
            await libHouseContext.CleanContextDataAsync();
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
            UsersWebApiAdapter userWebApiAdapter = new();
            _ = new UsersController(
                userWebApiAdapter,
                new Mock<IUserRegistration>().Object,
                new Mock<IConfirmUserRegistration>().Object,
                new Mock<IUserLogin>().Object,
                new Mock<IUserLogout>().Object,
                new Mock<IUserLoginRenewal>().Object,
                userPasswordReset,
                new Mock<IConfirmUserPasswordReset>().Object
            );
            Resident registeredUser = new("Marta", "Uchoa", new DateTime(1981, 5, 9), Gender.Female, "(19) 98344-9985", userEmail, userCpf);
            registeredUser.Activate();
            await libHouseContext.Users.AddAsync(registeredUser);
            await libHouseContext.SaveChangesAsync();
            Result userPasswordResetResult = await userWebApiAdapter.UserPasswordReset(new UserPasswordResetViewModel() { Cpf = userCpf });
            Assert.True(userPasswordResetResult.IsSuccess);
        }

        [Fact]
        public async Task ConfirmUserPasswordReset_UserThatRequestedPasswordReset_ShouldBeSuccess()
        {
            string userEmail = "davidlima@hotmail.com";
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
            UsersWebApiAdapter userWebApiAdapter = new();
            _ = new UsersController(
                userWebApiAdapter,
                new Mock<IUserRegistration>().Object,
                new Mock<IConfirmUserRegistration>().Object,
                new Mock<IUserLogin>().Object,
                new Mock<IUserLogout>().Object,
                new Mock<IUserLoginRenewal>().Object,
                new Mock<IUserPasswordReset>().Object,
                confirmUserPasswordReset
            );
            Result confirmUserPasswordResetResult = await userWebApiAdapter.ConfirmUserPasswordReset(new ConfirmUserPasswordResetViewModel()
            {
                ConfirmPassword = userNewPassword,
                Password = userNewPassword,
                PasswordResetToken = userPasswordResetToken,
                UserEmail = userEmail
            });
            Assert.True(confirmUserPasswordResetResult.IsSuccess);
        }
    }
}