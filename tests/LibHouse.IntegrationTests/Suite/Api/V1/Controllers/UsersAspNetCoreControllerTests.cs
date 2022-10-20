using LibHouse.API;
using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Users;
using LibHouse.Data.Context;
using LibHouse.Data.Extensions.Context;
using LibHouse.Infrastructure.Authentication.Context;
using LibHouse.Infrastructure.Authentication.Context.Extensions;
using LibHouse.Infrastructure.Authentication.Roles;
using LibHouse.Infrastructure.Controllers.Responses.Users;
using LibHouse.Infrastructure.Controllers.ViewModels.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace LibHouse.IntegrationTests.Suite.Api.V1.Controllers
{
    public class UsersAspNetCoreControllerTests
    {
        private readonly HttpClient _httpClient;
        private readonly LibHouseContext _libHouseContext;
        private readonly AuthenticationContext _authenticationContext;

        public UsersAspNetCoreControllerTests()
        {
            TestServer server = new(new WebHostBuilder()
                .UseEnvironment(Environments.Staging)
                .UseConfiguration(new ConfigurationBuilder()
                    .AddUserSecrets(typeof(UsersAspNetCoreControllerTests).Assembly)
                    .AddJsonFile("appsettings.Staging.json")
                    .Build())
                .UseStartup<Startup>());

            _libHouseContext = server.Services.GetRequiredService<LibHouseContext>();

            _authenticationContext = server.Services.GetRequiredService<AuthenticationContext>();

            _httpClient = server.CreateClient();
        }

        [Theory]
        [InlineData("POST", "/api/v1/users/new-account")]
        public async Task RegisterUserAsync_NewUser_ShouldReturn200OK(string method, string route)
        {
            await _libHouseContext.CleanContextDataAsync();
            await _authenticationContext.CleanContextDataAsync();
            HttpRequestMessage httpRequest = new(new HttpMethod(method), route);
            httpRequest.Content = new StringContent(JsonSerializer.Serialize(new UserRegistrationViewModel
            {
                BirthDate = new DateTime(1990, 5, 20),
                ConfirmPassword = "Senh@123456",
                Cpf = "58363714070",
                Email = "lucas.dirani@gmail.com",
                Gender = Gender.Male,
                LastName = "Dirani",
                Name = "Lucas",
                Password = "Senh@123456",
                Phone = "11918314320",
                UserType = UserType.Resident
            }), Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.SendAsync(httpRequest);

            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Theory]
        [InlineData("PATCH", "/api/v1/users/confirm-registration")]
        public async Task ConfirmUserRegistrationAsync_UserWithoutConfirmation_ShouldReturn204NoContent(string method, string route)
        {
            await _libHouseContext.CleanContextDataAsync();
            await _authenticationContext.CleanContextDataAsync();

            string userEmail = "leonardo.jardim@gmail.com";
            HttpRequestMessage httpRequestUserRegistration = new(new HttpMethod("POST"), "/api/v1/users/new-account");
            httpRequestUserRegistration.Content = new StringContent(JsonSerializer.Serialize(new UserRegistrationViewModel
            {
                BirthDate = new DateTime(1995, 7, 25),
                ConfirmPassword = "Senh@123456",
                Cpf = "47802855004",
                Email = userEmail,
                Gender = Gender.Male,
                LastName = "Jardim",
                Name = "Leonardo",
                Password = "Senh@123456",
                Phone = "21998415581",
                UserType = UserType.Owner
            }), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseUserRegistration = await _httpClient.SendAsync(httpRequestUserRegistration);
            UserRegistrationResponse userRegistrationResponse = JsonSerializer.Deserialize<UserRegistrationResponse>(await httpResponseUserRegistration.Content.ReadAsStringAsync(), new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            });
            User registeredUser = await _libHouseContext.Users.FirstOrDefaultAsync(user => user.Email.Value == userEmail);
            HttpRequestMessage httpRequestConfirmUserRegistration = new(new HttpMethod(method), route);
            httpRequestConfirmUserRegistration.Content = new StringContent(JsonSerializer.Serialize(new ConfirmUserRegistrationViewModel
            {
                ConfirmationToken = userRegistrationResponse.RegistrationToken,
                UserEmail = userEmail,
                UserId = registeredUser.Id
            }), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseConfirmUserRegistration = await _httpClient.SendAsync(httpRequestConfirmUserRegistration);

            Assert.Equal(HttpStatusCode.NoContent, httpResponseConfirmUserRegistration.StatusCode);
        }

        [Theory]
        [InlineData("POST", "/api/v1/users/login")]
        public async Task LoginUserAsync_RegisteredUser_ShouldReturn200OK(string method, string route)
        {
            await _libHouseContext.CleanContextDataAsync();
            await _authenticationContext.CleanContextDataAsync();
            string userEmail = "flavia-novais@gmail.com";
            User user = new Resident("Flavia", "Novais", new DateTime(1995, 7, 11), Gender.Female, "14925448519", userEmail, "87724072043");
            user.Activate();
            _libHouseContext.Users.Add(user);
            await _libHouseContext.SaveChangesAsync();
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
            _authenticationContext.Users.Add(identityUser);
            await _authenticationContext.SaveChangesAsync();
            HttpRequestMessage httpRequest = new(new HttpMethod(method), route);
            httpRequest.Content = new StringContent(JsonSerializer.Serialize(new UserLoginViewModel
            {
                Email = userEmail,
                Password = "Senh@123456"
            }), Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await _httpClient.SendAsync(httpRequest);

            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Theory]
        [InlineData("PATCH", "/api/v1/users/logout")]
        public async Task LogoutUserAsync_LoggedUser_ShouldReturn204NoContent(string method, string route)
        {
            await _libHouseContext.CleanContextDataAsync();
            await _authenticationContext.CleanContextDataAsync();
            string userEmail = "felipe-anderson@yahoo.com.br";
            User user = new Resident("Felipe", "Anderson", new DateTime(1978, 4, 15), Gender.Male, "17995241132", userEmail, "46252455083");
            user.Activate();
            _libHouseContext.Users.Add(user);
            await _libHouseContext.SaveChangesAsync();
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
            _authenticationContext.Users.Add(identityUser);
            string userRoleId = _authenticationContext.Roles.FirstOrDefault(role => role.Name == LibHouseUserRole.User).Id;
            _authenticationContext.UserRoles.Add(new IdentityUserRole<string>()
            {
                RoleId = userRoleId,
                UserId = identityUser.Id
            });
            await _authenticationContext.SaveChangesAsync();

            HttpRequestMessage httpRequestLogin = new(new HttpMethod("POST"), "/api/v1/users/login");
            httpRequestLogin.Content = new StringContent(JsonSerializer.Serialize(new UserLoginViewModel
            {
                Email = userEmail,
                Password = "Senh@123456"
            }), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseLogin = await _httpClient.SendAsync(httpRequestLogin);
            UserLoginResponse userLoginResponse = JsonSerializer.Deserialize<UserLoginResponse>(await httpResponseLogin.Content.ReadAsStringAsync(), new JsonSerializerOptions 
            { 
                WriteIndented = true,
                PropertyNameCaseInsensitive = true 
            });
            HttpRequestMessage httpRequestLogout = new(new HttpMethod(method), route);
            httpRequestLogout.Content = new StringContent(JsonSerializer.Serialize(new UserLogoutViewModel
            {
                Email = userEmail,
                RefreshToken = userLoginResponse.RefreshToken
            }), Encoding.UTF8, "application/json");
            httpRequestLogout.Headers.Authorization = new AuthenticationHeaderValue("Bearer", userLoginResponse.AccessToken);
            HttpResponseMessage httpResponseLogout = await _httpClient.SendAsync(httpRequestLogout);

            Assert.Equal(HttpStatusCode.NoContent, httpResponseLogout.StatusCode);
        }

        [Theory]
        [InlineData("POST", "/api/v1/users/refresh-token")]
        public async Task RefreshTokenAsync_UserAccessTokenNotExpired_ShouldReturn400BadRequest(string method, string route)
        {
            await _libHouseContext.CleanContextDataAsync();
            await _authenticationContext.CleanContextDataAsync();
            string userEmail = "rogerio.duarte@gmail.com";
            User user = new Resident("Rogério", "Duarte", new DateTime(1981, 6, 21), Gender.Male, "19991244671", userEmail, "69261910009");
            user.Activate();
            _libHouseContext.Users.Add(user);
            await _libHouseContext.SaveChangesAsync();
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
            _authenticationContext.Users.Add(identityUser);
            string userRoleId = _authenticationContext.Roles.FirstOrDefault(role => role.Name == LibHouseUserRole.User).Id;
            _authenticationContext.UserRoles.Add(new IdentityUserRole<string>()
            {
                RoleId = userRoleId,
                UserId = identityUser.Id
            });
            await _authenticationContext.SaveChangesAsync();

            HttpRequestMessage httpRequestLogin = new(new HttpMethod("POST"), "/api/v1/users/login");
            httpRequestLogin.Content = new StringContent(JsonSerializer.Serialize(new UserLoginViewModel
            {
                Email = userEmail,
                Password = "Senh@123456"
            }), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseLogin = await _httpClient.SendAsync(httpRequestLogin);
            UserLoginResponse userLoginResponse = JsonSerializer.Deserialize<UserLoginResponse>(await httpResponseLogin.Content.ReadAsStringAsync(), new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            });
            HttpRequestMessage httpRequestRefreshToken = new(new HttpMethod(method), route);
            httpRequestRefreshToken.Content = new StringContent(JsonSerializer.Serialize(new UserLoginRenewalViewModel
            {
                Email = userEmail,
                RefreshToken = userLoginResponse.RefreshToken,
                AccessToken = userLoginResponse.AccessToken
            }), Encoding.UTF8, "application/json");
            httpRequestRefreshToken.Headers.Authorization = new AuthenticationHeaderValue("Bearer", userLoginResponse.AccessToken);
            HttpResponseMessage httpResponseRefreshToken = await _httpClient.SendAsync(httpRequestRefreshToken);

            Assert.Equal(HttpStatusCode.BadRequest, httpResponseRefreshToken.StatusCode);
        }

        [Theory]
        [InlineData("POST", "/api/v1/users/request-password-reset")]
        public async Task RequestPasswordResetAsync_RegisteredUser_ShouldReturn200OK(string method, string route)
        {
            await _libHouseContext.CleanContextDataAsync();
            await _authenticationContext.CleanContextDataAsync();
            string userCpf = "66104368000";
            string userEmail = "jair-pereira@gmail.com";
            User user = new Resident("Jair", "Pereira", new DateTime(1985, 9, 11), Gender.Male, "11941554970", userEmail, userCpf);
            user.Activate();
            _libHouseContext.Users.Add(user);
            await _libHouseContext.SaveChangesAsync();
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
            _authenticationContext.Users.Add(identityUser);
            await _authenticationContext.SaveChangesAsync();

            HttpRequestMessage httpRequestPasswordReset = new(new HttpMethod(method), route);
            httpRequestPasswordReset.Content = new StringContent(JsonSerializer.Serialize(new UserPasswordResetViewModel
            {
                Cpf = userCpf
            }), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponsePasswordReset = await _httpClient.SendAsync(httpRequestPasswordReset);

            Assert.Equal(HttpStatusCode.OK, httpResponsePasswordReset.StatusCode);
        }

        [Theory]
        [InlineData("PATCH", "/api/v1/users/confirm-password-reset")]
        public async Task ConfirmPasswordResetAsync_UserThatRequestedPasswordReset_ShouldReturn204NoContent(string method, string route)
        {
            await _libHouseContext.CleanContextDataAsync();
            await _authenticationContext.CleanContextDataAsync();
            string userCpf = "99092729035";
            string userEmail = "dercyvasconcelos@gmail.com";
            string userNewPassword = "Senh@1234567";
            User user = new Resident("Dercy", "Vasconcelos", new DateTime(1960, 5, 7), Gender.Female, "17992257813", userEmail, userCpf);
            user.Activate();
            _libHouseContext.Users.Add(user);
            await _libHouseContext.SaveChangesAsync();
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
            _authenticationContext.Users.Add(identityUser);
            await _authenticationContext.SaveChangesAsync();

            HttpRequestMessage httpRequestPasswordReset = new(new HttpMethod("POST"), "/api/v1/users/request-password-reset");
            httpRequestPasswordReset.Content = new StringContent(JsonSerializer.Serialize(new UserPasswordResetViewModel
            {
                Cpf = userCpf
            }), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseRequestPasswordReset = await _httpClient.SendAsync(httpRequestPasswordReset);
            var userPasswordResetResponse = JsonSerializer.Deserialize<UserPasswordResetResponse>(await httpResponseRequestPasswordReset.Content.ReadAsStringAsync(), new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            });
            HttpRequestMessage httpRequestConfirmUserPasswordReset = new(new HttpMethod(method), route);
            httpRequestConfirmUserPasswordReset.Content = new StringContent(JsonSerializer.Serialize(new ConfirmUserPasswordResetViewModel
            {
                ConfirmPassword = userNewPassword,
                Password = userNewPassword,
                UserEmail = userEmail,
                PasswordResetToken = userPasswordResetResponse.PasswordResetToken
            }), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseConfirmUserPasswordReset = await _httpClient.SendAsync(httpRequestConfirmUserPasswordReset);

            Assert.Equal(HttpStatusCode.NoContent, httpResponseConfirmUserPasswordReset.StatusCode);
        }
    }
}