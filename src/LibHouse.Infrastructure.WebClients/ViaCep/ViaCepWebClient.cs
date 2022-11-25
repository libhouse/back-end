using LibHouse.Infrastructure.WebClients.ViaCep.Configurations;
using LibHouse.Infrastructure.WebClients.ViaCep.Outputs;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace LibHouse.Infrastructure.WebClients.ViaCep
{
    public class ViaCepWebClient
    {
        private readonly HttpClient _httpClient;
        private readonly ViaCepWebClientConfiguration _configurations;

        public ViaCepWebClient(
            HttpClient httpClient,
            ViaCepWebClientConfiguration viaCepWebClientConfiguration)
        {
            _httpClient = httpClient;
            _configurations = viaCepWebClientConfiguration;
        }

        public async Task<OutputGetAddressByPostalCode> GetAddressByPostalCodeAsync(string postalCode)
        {
            if (string.IsNullOrEmpty(postalCode))
            {
                throw new ArgumentNullException(paramName: nameof(postalCode), "O código postal é obrigatório");
            }
            using HttpRequestMessage httpRequest = new(HttpMethod.Get, $"{_configurations.BaseUrl}/{postalCode}/{_configurations.EndpointResponseType}");
            using HttpResponseMessage httpResponse = await _httpClient.SendAsync(httpRequest);
            httpResponse.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<OutputGetAddressByPostalCode>(await httpResponse.Content.ReadAsStringAsync());
        }
    }
}