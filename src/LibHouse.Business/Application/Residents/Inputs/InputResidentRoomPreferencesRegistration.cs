using System;

namespace LibHouse.Business.Application.Residents.Inputs
{
    public record InputResidentRoomPreferencesRegistration
    {
        public Guid ResidentId { get; init; }
        public bool WantSingleDormitory { get; init; }
        public bool WantFurnishedDormitory { get; init; }
        public bool WantStove { get; init; }
        public bool WantMicrowave { get; init; }
        public bool WantRefrigerator { get; init; }
        public bool WantGarage { get; init; }
        public bool WantPrivateBathroom { get; init; }
        public bool WantServiceArea { get; init; }
        public bool WantRecreationArea { get; init; }
    }
}