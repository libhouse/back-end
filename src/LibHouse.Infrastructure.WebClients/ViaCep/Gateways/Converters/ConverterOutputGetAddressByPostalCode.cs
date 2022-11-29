using LibHouse.Business.Application.Localizations.Gateways.Outputs;
using LibHouse.Infrastructure.WebClients.ViaCep.Outputs;

namespace LibHouse.Infrastructure.WebClients.ViaCep.Gateways.Converters
{
    internal static class ConverterOutputGetAddressByPostalCode
    {
        internal static OutputAddressPostalCodeGateway Convert(this OutputGetAddressByPostalCode output)
        {
            return new()
            {
                Complement = output.Complement,
                FederativeUnit = output.FederativeUnit,
                IsSuccess = output.AddressWasObtained(),
                Localization = output.Localization,
                Neighborhood = output.Neighborhood,
                PostalCode = output.PostalCode,
                Street = output.Street
            };
        }
    }
}