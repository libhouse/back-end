using LibHouse.Business.Application.Users;
using LibHouse.Business.Application.Users.Gateways;
using LibHouse.Business.Application.Users.Outputs;
using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Shared;
using LibHouse.Business.Entities.Users;
using LibHouse.Business.Extensions;
using LibHouse.Business.Notifiers;
using LibHouse.Data.Context;
using LibHouse.Data.Repositories.Users;
using LibHouse.Data.Transactions;
using LibHouse.Infrastructure.Authentication.Context;
using LibHouse.Infrastructure.Authentication.Login;
using LibHouse.Infrastructure.Authentication.Token.Generators.AccessTokens;
using LibHouse.Infrastructure.Authentication.Token.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LibHouse.IntegrationTests.Suite.Business.Application.Users
{
    [Collection("Business.Application")]
    public class UserLoginTests
    {
        [Fact]
        public async Task ExecuteAsync_RegisteredUser_ShouldLoginUser()
        {
            string userEmail = "roberto-motta@gmail.com";
            string userPassword = "Senh@123456";
            Notifier notifier = new();
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
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseInMemoryDatabase("InMemoryLibHouse").Options);
            IUserRepository userRepository = new UserRepository(libHouseContext);
            IUnitOfWork unitOfWork = new UnitOfWork(libHouseContext);
            UserLogin userLogin = new(notifier, userLoginGateway, userRepository);
            Resident registeredUser = new("Roberto", "Motta", new DateTime(1960, 8, 20), Gender.Male, "(11) 91633-9187", userEmail, "98598456039");
            registeredUser.Activate();
            await unitOfWork.UserRepository.AddAsync(registeredUser);
            await unitOfWork.CommitAsync();
            OutputUserLogin outputUserLogin = await userLogin.ExecuteAsync(new(userEmail, userPassword));
            Assert.True(outputUserLogin.IsSuccess);
            Assert.Equal(userEmail, outputUserLogin.UserEmail);
            Assert.Equal(registeredUser.UserType.GetDescription(), outputUserLogin.UserType);
            Assert.Equal(string.Concat(registeredUser.Name, " ", registeredUser.LastName), outputUserLogin.UserFullName);
        }

        [Fact]
        public async Task ExecuteAsync_UnregisteredUser_ShouldFail()
        {
            string userEmail = "amanda-rodrigues@yahoo.com.br";
            string userPassword = "Senh@123456";
            Notifier notifier = new();
            AuthenticationContext authenticationContext = new(new DbContextOptionsBuilder<AuthenticationContext>().UseInMemoryDatabase("InMemoryAuthentication").Options);
            IUserStore<IdentityUser> userStore = new UserStore<IdentityUser>(authenticationContext);
            Mock<UserManager<IdentityUser>> userManager = new(userStore, null, new PasswordHasher<IdentityUser>(), null, null, null, null, null, null);
            Mock<SignInManager<IdentityUser>> signInManager = new(userManager.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<IdentityUser>>(), null, null, null, null);
            signInManager.Setup(s => s.PasswordSignInAsync(userEmail, userPassword, true, true)).ReturnsAsync(SignInResult.Failed);
            Mock<IAccessTokenGenerator> accessTokenGenerator = new();
            IUserLoginGateway userLoginGateway = new IdentityUserLoginGateway(signInManager.Object, accessTokenGenerator.Object);
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseInMemoryDatabase("InMemoryLibHouse").Options);
            IUserRepository userRepository = new UserRepository(libHouseContext);
            UserLogin userLogin = new(notifier, userLoginGateway, userRepository);
            OutputUserLogin outputUserLogin = await userLogin.ExecuteAsync(new(userEmail, userPassword));
            Assert.False(outputUserLogin.IsSuccess);
        }
    }
}