using System;

namespace LibHouse.Business.Application.Residents.Inputs
{
    public record InputResidentLocalizationPreferencesRegistration
    {
        public Guid ResidentId { get; init; }
        public string AddressDescription { get; init; }
        public string AddressComplement { get; init; }
        public ushort AddressNumber { get; init; }
        public string AddressNeighborhood { get; init; }
        public string AddressCity { get; init; }
        public string AddressFederativeUnit { get; init; }
        public string AddressPostalCodeNumber { get; init; }
    }
}