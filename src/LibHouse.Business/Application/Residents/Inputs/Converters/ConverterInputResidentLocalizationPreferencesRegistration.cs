using LibHouse.Business.Entities.Localizations;

namespace LibHouse.Business.Application.Residents.Inputs.Converters
{
    internal static class ConverterInputResidentLocalizationPreferencesRegistration
    {
        internal static Address Convert(this InputResidentLocalizationPreferencesRegistration input)
        {
            return new(
                input.AddressDescription, 
                input.AddressNumber,
                input.AddressNeighborhood,
                input.AddressCity, 
                input.AddressFederativeUnit,
                input.AddressPostalCodeNumber, 
                input.AddressComplement);
        }
    }
}