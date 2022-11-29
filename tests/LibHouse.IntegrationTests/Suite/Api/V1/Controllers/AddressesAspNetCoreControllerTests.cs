using LibHouse.Business.Entities.Users;
using LibHouse.Data.Extensions.Context;
using LibHouse.Infrastructure.Authentication.Context.Extensions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace LibHouse.IntegrationTests.Suite.Api.V1.Controllers
{
    [Collection("Api")]
    public class AddressesAspNetCoreControllerTests : BaseIntegrationTestApi
    {
        [Theory]
        [InlineData("GET", "/api/v1/addresses/postal-code/01001000")]
        public async Task GetAddressByPostalCodeAsync_ValidPostalCode_ShouldReturn200OK(string method, string route)
        {
            await _libHouseContext.CleanContextDataAsync();
            await _authenticationContext.CleanContextDataAsync();
            User userResident = await CreateActiveResidentAsync();
            HttpRequestMessage httpRequest = new(new HttpMethod(method), route);
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await GetAccessTokenAsync(userResident.GetEmailAddress()));
            HttpResponseMessage httpResponse = await _httpClient.SendAsync(httpRequest);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
    }
}