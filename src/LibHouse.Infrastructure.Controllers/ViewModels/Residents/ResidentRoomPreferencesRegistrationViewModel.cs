using System;

namespace LibHouse.Infrastructure.Controllers.ViewModels.Residents
{
    public record ResidentRoomPreferencesRegistrationViewModel
    {
        public Guid ResidentId { get; init; }
        public ResidentDormitoryPreferencesRegistrationViewModel DormitoryPreferences { get; init; }
        public ResidentKitchenPreferencesRegistrationViewModel KitchenPreferences { get; init; }
        public ResidentGaragePreferencesRegistrationViewModel GaragePreferences { get; init; }
        public ResidentBathroomPreferencesRegistrationViewModel BathroomPreferences { get; init; }
        public ResidentOtherPreferencesRegistrationViewModel OtherPreferences { get; init; }
    }
}