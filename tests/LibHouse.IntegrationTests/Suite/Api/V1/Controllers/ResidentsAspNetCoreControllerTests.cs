using LibHouse.Business.Entities.Users;
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
            await ResetApiDatabaseAsync();
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
            await ResetApiDatabaseAsync();
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

        [Theory]
        [InlineData("POST", "/api/v1/residents/register-charge-preferences")]
        public async Task RegisterResidentChargePreferencesAsync_NewChargePreferences_ShouldReturn200OK(string method, string route)
        {
            await ResetApiDatabaseAsync();
            User userResident = await CreateActiveResidentAsync();
            HttpRequestMessage httpRequest = new(new HttpMethod(method), route);
            httpRequest.Content = new StringContent(JsonSerializer.Serialize(new ResidentChargePreferencesRegistrationViewModel
            {
                ResidentId = userResident.Id,
                MinimumRentalAmountDesired = 100.0m,
                MaximumRentalAmountDesired = 400.0m,
                MinimumExpenseAmountDesired = 50.0m,
                MaximumExpenseAmountDesired = 150.0m
            }), Encoding.UTF8, "application/json");
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await GetAccessTokenAsync(userResident.GetEmailAddress()));
            HttpResponseMessage httpResponse = await _httpClient.SendAsync(httpRequest);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Theory]
        [InlineData("POST", "/api/v1/residents/register-general-preferences")]
        public async Task RegisterResidentGeneralPreferencesAsync_NewGeneralPreferences_ShouldReturn200OK(string method, string route)
        {
            await ResetApiDatabaseAsync();
            User userResident = await CreateActiveResidentAsync();
            HttpRequestMessage httpRequest = new(new HttpMethod(method), route);
            httpRequest.Content = new StringContent(JsonSerializer.Serialize(new ResidentGeneralPreferencesRegistrationViewModel
            {
                ResidentId = userResident.Id,
                WantSpaceForAnimals = true,
                AcceptChildren = true,
                WantsToParty = false,
                AcceptSmokers = false,
                AcceptsOnlyMenAsRoommates = false,
                AcceptsOnlyWomenAsRoommates = false,
                MinimumNumberOfRoommatesDesired = 1,
                MaximumNumberOfRoommatesDesired = 3
            }), Encoding.UTF8, "application/json");
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await GetAccessTokenAsync(userResident.GetEmailAddress()));
            HttpResponseMessage httpResponse = await _httpClient.SendAsync(httpRequest);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Theory]
        [InlineData("POST", "/api/v1/residents/register-localization-preferences")]
        public async Task RegisterResidentLocalizationPreferencesAsync_NewLocalizationPreferences_ShouldReturn200OK(string method, string route)
        {
            await ResetApiDatabaseAsync();
            User userResident = await CreateActiveResidentAsync();
            HttpRequestMessage httpRequest = new(new HttpMethod(method), route);
            httpRequest.Content = new StringContent(JsonSerializer.Serialize(new ResidentLocalizationPreferencesRegistrationViewModel
            {
                ResidentId = userResident.Id,
                LandmarkAddressDescription = "Rua São Bento",
                LandmarkAddressComplement = "de 321 ao fim - lado ímpar",
                LandmarkAddressNumber = 321,
                LandmarkAddressNeighborhood = "Centro",
                LandmarkAddressCity = "São Paulo",
                LandmarkAddressFederativeUnit = "SP",
                LandmarkAddressPostalCodeNumber = "01011100"
            }), Encoding.UTF8, "application/json");
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await GetAccessTokenAsync(userResident.GetEmailAddress()));
            HttpResponseMessage httpResponse = await _httpClient.SendAsync(httpRequest);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
    }
}