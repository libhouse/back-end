using System;

namespace LibHouse.Infrastructure.Controllers.ViewModels.Residents
{
    public record ResidentServicesPreferencesRegistrationViewModel
    {
        public Guid ResidentId { get; init; }
        public bool WantHouseCleaningService { get; init; }
        public bool WantInternetService { get; init; }
        public bool WantCableTelevisionService { get; init; }
    }
}