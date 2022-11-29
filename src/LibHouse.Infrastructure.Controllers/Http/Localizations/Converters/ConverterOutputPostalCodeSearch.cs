using LibHouse.Business.Application.Localizations.Outputs;
using LibHouse.Infrastructure.Controllers.Responses.Localizations;

namespace LibHouse.Infrastructure.Controllers.Http.Localizations.Converters
{
    internal static class ConverterOutputPostalCodeSearch
    {
        internal static PostalCodeSearchResponse Convert(this OutputPostalCodeSearch output)
        {
            return new()
            {
                Complement = output.Complement,
                FederativeUnit = output.FederativeUnit,
                Localization = output.Localization,
                Neighborhood = output.Neighborhood,
                Street = output.Street
            };
        }
    }
}