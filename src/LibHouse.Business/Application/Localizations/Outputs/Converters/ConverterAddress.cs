using LibHouse.Business.Entities.Localizations;

namespace LibHouse.Business.Application.Localizations.Outputs.Converters
{
    internal static class ConverterAddress
    {
        internal static OutputPostalCodeSearch Convert(this Address address)
        {
            return new()
            {
                Street = address.GetName(),
                PostalCode = address.GetPostalCodeNumber(),
                Complement = address.GetComplement(),
                FederativeUnit = address.GetAbbreviationOfTheFederativeUnit(),
                IsSuccess = true,
                Localization = address.GetCityName(),
                Neighborhood = address.GetNeighborhoodName()
            };
        }
    }
}