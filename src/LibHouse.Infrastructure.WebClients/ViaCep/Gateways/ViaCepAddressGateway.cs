using LibHouse.Business.Application.Localizations.Gateways;
using LibHouse.Business.Application.Localizations.Gateways.Outputs;
using LibHouse.Infrastructure.WebClients.ViaCep.Gateways.Converters;
using LibHouse.Infrastructure.WebClients.ViaCep.Outputs;
using System.Threading.Tasks;

namespace LibHouse.Infrastructure.WebClients.ViaCep.Gateways
{
    public class ViaCepAddressGateway : IAddressPostalCodeGateway
    {
        private readonly ViaCepWebClient _viaCepWebClient;

        public ViaCepAddressGateway(ViaCepWebClient viaCepWebClient)
        {
            _viaCepWebClient = viaCepWebClient;
        }

        public async Task<OutputAddressPostalCodeGateway> GetAddressByPostalCodeAsync(string postalCode)
        {
            OutputGetAddressByPostalCode output = await _viaCepWebClient.GetAddressByPostalCodeAsync(postalCode);
            return output.Convert();
        }
    }
}