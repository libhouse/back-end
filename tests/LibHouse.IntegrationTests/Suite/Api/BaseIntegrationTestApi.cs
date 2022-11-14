using LibHouse.API;
using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Users;
using LibHouse.Data.Context;
using LibHouse.Infrastructure.Authentication.Context;
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
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LibHouse.IntegrationTests.Suite.Api
{
    public abstract class BaseIntegrationTestApi
    {
        protected readonly HttpClient _httpClient;
        protected readonly LibHouseContext _libHouseContext;
        protected readonly AuthenticationContext _authenticationContext;

        public BaseIntegrationTestApi()
        {
            TestServer server = new(new WebHostBuilder()
                .UseEnvironment(Environments.Staging)
                .UseConfiguration(new ConfigurationBuilder()
                    .AddUserSecrets(typeof(BaseIntegrationTestApi).Assembly)
                    .AddJsonFile("appsettings.Staging.json")
                    .Build())
                .UseStartup<Startup>());
            _libHouseContext = server.Services.GetRequiredService<LibHouseContext>();
            _authenticationContext = server.Services.GetRequiredService<AuthenticationContext>();
            _httpClient = server.CreateClient();
        }

        protected async Task<Resident> CreateActiveResidentAsync()
        {
            Resident resident = new("Resident", "Test", new DateTime(2000, 11, 20), Gender.Male, "11985267981", "resident@gmail.com", "28825323000");
            resident.Activate();
            await _libHouseContext.Residents.AddAsync(resident);
            await _libHouseContext.SaveChangesAsync();
            string residentAuthenticationId = Guid.NewGuid().ToString();
            string residentRoleId = _authenticationContext.Roles.FirstOrDefault(role => role.Name == LibHouseUserRole.Resident).Id;
            await _authenticationContext.Users.AddAsync(new IdentityUser()
            {
                Id = residentAuthenticationId,
                Email = resident.GetEmailAddress(),
                EmailConfirmed = true,
                NormalizedEmail = resident.GetEmailAddress().ToUpper(),
                UserName = resident.GetEmailAddress(),
                NormalizedUserName = resident.GetEmailAddress().ToUpper(),
                PasswordHash = "AQAAAAEAACcQAAAAEGGXbR8FPdApGw6YRB6r95b1wYvOrJkdFCAhpL5t6a3f6E/NtB2YWLifYx5sBpyltw==",
                SecurityStamp = "74LC5CVBBAZCQKTVJFW7VTZTF6UJMQZN",
                ConcurrencyStamp = "97d52be0-5c20-41a4-9ccd-c36f75eb8926",
                LockoutEnabled = true
            });
            await _authenticationContext.UserRoles.AddAsync(new IdentityUserRole<string>()
            {
                RoleId = residentRoleId,
                UserId = residentAuthenticationId
            });
            await _authenticationContext.SaveChangesAsync();
            return resident;
        }

        protected async Task<string> GetAccessTokenAsync(string userEmail)
        {
            HttpRequestMessage httpRequestAccessToken = new(new HttpMethod("POST"), "/api/v1/users/login");
            httpRequestAccessToken.Content = new StringContent(JsonSerializer.Serialize(new UserLoginViewModel
            {
                Email = userEmail,
                Password = "Senh@123456"
            }), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseAccessToken = await _httpClient.SendAsync(httpRequestAccessToken);
            UserLoginResponse userLoginResponse = JsonSerializer.Deserialize<UserLoginResponse>(await httpResponseAccessToken.Content.ReadAsStringAsync(), new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            });
            return userLoginResponse.AccessToken;
        }
    }
}