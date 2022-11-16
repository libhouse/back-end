using System;

namespace LibHouse.Business.Application.Residents.Inputs
{
    public record InputResidentServicesPreferencesRegistration
    {
        public Guid ResidentId { get; init; }
        public bool WantHouseCleaningService { get; init; }
        public bool WantInternetService { get; init; }
        public bool WantCableTelevisionService { get; init; }
    }
}