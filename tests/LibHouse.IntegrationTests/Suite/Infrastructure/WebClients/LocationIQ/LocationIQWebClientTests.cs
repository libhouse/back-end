using LibHouse.API;
using LibHouse.Infrastructure.WebClients.LocationIQ;
using LibHouse.Infrastructure.WebClients.LocationIQ.Configurations;
using LibHouse.Infrastructure.WebClients.LocationIQ.Outputs;
using LibHouse.Infrastructure.WebClients.LocationIQ.Parameters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace LibHouse.IntegrationTests.Suite.Infrastructure.WebClients.LocationIQ
{
    [Collection("Infrastructure.WebClients")]
    public class LocationIQWebClientTests
    {
        private readonly IConfiguration _testsConfiguration;

        public LocationIQWebClientTests()
        {
            _testsConfiguration = new ConfigurationBuilder().AddUserSecrets<Startup>().Build();
        }

        [Fact]
        public async Task SearchForwardGeocodingAsync_ValidAddress_ShouldReturnAddressForwardGeocodingOutput()
        {
            LocationIQWebClientConfiguration configuration = new()
            {
                BaseUrl = _testsConfiguration.GetValue<string>("LocationIQWebClient.BaseUrl"),
                EndpointResponseType = _testsConfiguration.GetValue<string>("LocationIQWebClient.EndpointResponseType"),
                AccessToken = _testsConfiguration.GetValue<string>("LocationIQWebClient.AccessToken")
            };
            HttpClient httpClient = new();
            LocationIQWebClient locationIQWebClient = new(httpClient, configuration);
            ForwardGeocodingStructuredRequestParameter parameters = new()
            {
                Street = "Avenida Paulista, 2202",
                City = "São Paulo",
                County = "Bela Vista",
                State = "SP",
                Country = "Brasil",
                Limit = 1
            };
            IEnumerable<OutputForwardGeocoding> output = await locationIQWebClient.SearchForwardGeocodingAsync(parameters);
            Assert.Equal("-23.5677057", output.ElementAt(0).Latitude);
            Assert.Equal("-46.6487839", output.ElementAt(0).Longitude);
        }

        [Fact]
        public async Task SearchForwardGeocodingAsync_EmptyRequestParameter_ShouldThrowAnException()
        {
            LocationIQWebClientConfiguration configuration = new()
            {
                BaseUrl = _testsConfiguration.GetValue<string>("LocationIQWebClient.BaseUrl"),
                EndpointResponseType = _testsConfiguration.GetValue<string>("LocationIQWebClient.EndpointResponseType"),
                AccessToken = _testsConfiguration.GetValue<string>("LocationIQWebClient.AccessToken")
            };
            HttpClient httpClient = new();
            LocationIQWebClient locationIQWebClient = new(httpClient, configuration);
            ForwardGeocodingStructuredRequestParameter parameters = new();
            await Assert.ThrowsAnyAsync<Exception>(async () => await locationIQWebClient.SearchForwardGeocodingAsync(parameters));
        }
    }
}