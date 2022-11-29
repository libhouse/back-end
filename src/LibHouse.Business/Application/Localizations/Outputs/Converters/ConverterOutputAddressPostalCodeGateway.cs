using LibHouse.Business.Application.Localizations.Gateways.Outputs;

namespace LibHouse.Business.Application.Localizations.Outputs.Converters
{
    internal static class ConverterOutputAddressPostalCodeGateway
    {
        internal static OutputPostalCodeSearch Convert(this OutputAddressPostalCodeGateway output)
        {
            return new()
            {
                Complement = output.Complement,
                FederativeUnit = output.FederativeUnit,
                IsSuccess = output.IsSuccess,
                Localization = output.Localization,
                Neighborhood = output.Neighborhood,
                PostalCode = output.PostalCode,
                Street = output.Street
            };
        }
    }
}