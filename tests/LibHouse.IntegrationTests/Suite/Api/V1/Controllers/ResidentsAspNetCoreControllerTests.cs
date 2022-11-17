using LibHouse.Business.Entities.Users;
using LibHouse.Data.Extensions.Context;
using LibHouse.Infrastructure.Authentication.Context.Extensions;
using LibHouse.Infrastructure.Controllers.ViewModels.Residents;
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
    public class ResidentsAspNetCoreControllerTests : BaseIntegrationTestApi
    {
        [Theory]
        [InlineData("POST", "/api/v1/residents/register-room-preferences")]
        public async Task RegisterResidentRoomPreferencesAsync_NewRoomPreferences_ShouldReturn200OK(string method, string route)
        {
            await _libHouseContext.CleanContextDataAsync();
            await _authenticationContext.CleanContextDataAsync();
            User userResident = await CreateActiveResidentAsync();
            HttpRequestMessage httpRequest = new(new HttpMethod(method), route);
            httpRequest.Content = new StringContent(JsonSerializer.Serialize(new ResidentRoomPreferencesRegistrationViewModel
            {
                BathroomPreferences = new()
                {
                    WantPrivateBathroom = true
                },
                DormitoryPreferences = new()
                {
                    WantFurnishedDormitory = true,
                    WantSingleDormitory = false 
                },
                GaragePreferences = new()
                {
                    WantGarage = true 
                },
                KitchenPreferences = new()
                {
                    WantMicrowave = true,
                    WantRefrigerator = true,
                    WantStove = true
                },
                OtherPreferences = new()
                {
                    WantRecreationArea = false,
                    WantServiceArea = false
                },
                ResidentId = userResident.Id
            }), Encoding.UTF8, "application/json");
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await GetAccessTokenAsync(userResident.GetEmailAddress()));
            HttpResponseMessage httpResponse = await _httpClient.SendAsync(httpRequest);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Theory]
        [InlineData("POST", "/api/v1/residents/register-services-preferences")]
        public async Task RegisterResidentServicesPreferencesAsync_NewServicesPreferences_ShouldReturn200OK(string method, string route)
        {
            await _libHouseContext.CleanContextDataAsync();
            await _authenticationContext.CleanContextDataAsync();
            User userResident = await CreateActiveResidentAsync();
            HttpRequestMessage httpRequest = new(new HttpMethod(method), route);
            httpRequest.Content = new StringContent(JsonSerializer.Serialize(new ResidentServicesPreferencesRegistrationViewModel
            {
                ResidentId = userResident.Id,
                WantInternetService = true,
                WantCableTelevisionService = true,
                WantHouseCleaningService = true
            }), Encoding.UTF8, "application/json");
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await GetAccessTokenAsync(userResident.GetEmailAddress()));
            HttpResponseMessage httpResponse = await _httpClient.SendAsync(httpRequest);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
    }
}