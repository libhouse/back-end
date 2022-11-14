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
    [Collection("Api")]
    public class UsersAspNetCoreControllerTests : BaseIntegrationTestApi
    {
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
    }
}