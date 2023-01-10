using LibHouse.Infrastructure.WebClients.LocationIQ.Configurations;
using LibHouse.Infrastructure.WebClients.LocationIQ.Outputs;
using LibHouse.Infrastructure.WebClients.LocationIQ.Parameters;
using LibHouse.Infrastructure.WebClients.LocationIQ.Urls;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace LibHouse.Infrastructure.WebClients.LocationIQ
{
    public class LocationIQWebClient
    {
        private readonly HttpClient _httpClient;
        private readonly LocationIQWebClientConfiguration _configurations;

        public LocationIQWebClient(
            HttpClient httpClient, 
            LocationIQWebClientConfiguration configuration)
        {
            _httpClient = httpClient;
            _configurations = configuration;
        }

        public async Task<IEnumerable<OutputForwardGeocoding>> SearchForwardGeocodingAsync(ForwardGeocodingStructuredRequestParameter parameters)
        {
            if (!parameters.IsValid())
            {
                throw new ArgumentException(message: "Todas as propriedades devem ser fornecidas", paramName: nameof(parameters));
            }
            string queryParameters = LocationIQUrlQueryParameterBuilder.BuildQueryParametersForSearchForwardGeocoding(parameters, _configurations.AccessToken, _configurations.EndpointResponseType);
            string requestUri = $@"{_configurations.BaseUrl}/search?{queryParameters}";
            using HttpRequestMessage httpRequest = new(HttpMethod.Get, requestUri);
            using HttpResponseMessage httpResponse = await _httpClient.SendAsync(httpRequest);
            httpResponse.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<IEnumerable<OutputForwardGeocoding>>(await httpResponse.Content.ReadAsStringAsync());
        }
    }
}