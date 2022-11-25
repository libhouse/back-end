using LibHouse.Infrastructure.WebClients.ViaCep;
using LibHouse.Infrastructure.WebClients.ViaCep.Configurations;
using LibHouse.Infrastructure.WebClients.ViaCep.Outputs;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace LibHouse.IntegrationTests.Suite.Infrastructure.WebClients.ViaCep
{
    [Collection("Infrastructure.WebClients")]
    public class ViaCepWebClientTests
    {
        [Fact]
        public async Task GetAddressByPostalCodeAsync_ValidPostalCode_ShouldReturnAddress()
        {
            ViaCepWebClientConfiguration viaCepWebClientConfiguration = new()
            {
                BaseUrl = "https://viacep.com.br/ws",
                EndpointResponseType = "json"
            };
            HttpClient httpClient = new();
            ViaCepWebClient viaCepWebClient = new(httpClient, viaCepWebClientConfiguration);
            OutputGetAddressByPostalCode output = await viaCepWebClient.GetAddressByPostalCodeAsync("01001000");
            Assert.Equal("São Paulo", output.Localization);
            Assert.Equal("Praça da Sé", output.Street);
            Assert.Equal("Sé", output.Neighborhood);
        }
    }
}