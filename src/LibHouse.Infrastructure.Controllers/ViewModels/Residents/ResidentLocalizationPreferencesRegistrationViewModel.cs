using System;

namespace LibHouse.Infrastructure.Controllers.ViewModels.Residents
{
    public record ResidentLocalizationPreferencesRegistrationViewModel
    {
        public Guid ResidentId { get; init; }
        public string LandmarkAddressDescription { get; init; }
        public string LandmarkAddressComplement { get; init; }
        public ushort LandmarkAddressNumber { get; init; }
        public string LandmarkAddressNeighborhood { get; init; }
        public string LandmarkAddressCity { get; init; }
        public string LandmarkAddressFederativeUnit { get; init; }
        public string LandmarkAddressPostalCodeNumber { get; init; }
    }
}