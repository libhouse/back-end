using LibHouse.Business.Entities.Localizations;
using System;

namespace LibHouse.Business.Entities.Residents.Preferences.Localizations
{
    public record LandmarkPreferences
    {
        public LandmarkPreferences(Address address)
        {
            if (address is null)
            {
                throw new ArgumentNullException(paramName: nameof(address), message: "O endereço de referência é obrigatório");
            }
            LandmarkAddress = address;
            LandmarkAddressId = address.Id;
        }

        private LandmarkPreferences() { }

        public Guid LandmarkAddressId { get; init; }
        private Address LandmarkAddress { get; init; }

        public string GetLandmarkAddress()
        {
            return LandmarkAddress.GetName();
        }

        public ushort GetLandmarkNumber()
        {
            return LandmarkAddress.GetNumber();
        }

        public string GetLandmarkNeighborhood()
        {
            return LandmarkAddress.GetNeighborhoodName();
        }

        public string GetLandmarkCity()
        {
            return LandmarkAddress.GetCityName();
        }

        public string GetLandmarkFederativeUnit()
        {
            return LandmarkAddress.GetAbbreviationOfTheFederativeUnit();
        }

        public string GetLandmarkPostalCodeNumber()
        {
            return LandmarkAddress.GetPostalCodeNumber();
        }

        public string GetLandmarkComplement()
        {
            return LandmarkAddress.GetComplement();
        }
    }
}