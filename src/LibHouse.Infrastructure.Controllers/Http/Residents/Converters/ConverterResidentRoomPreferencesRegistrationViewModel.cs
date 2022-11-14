using LibHouse.Business.Application.Residents.Inputs;
using LibHouse.Infrastructure.Controllers.ViewModels.Residents;

namespace LibHouse.Infrastructure.Controllers.Http.Residents.Converters
{
    internal static class ConverterResidentRoomPreferencesRegistrationViewModel
    {
        internal static InputResidentRoomPreferencesRegistration Convert(this ResidentRoomPreferencesRegistrationViewModel viewModel)
        {
            return new()
            {
                ResidentId = viewModel.ResidentId,
                WantFurnishedDormitory = viewModel.DormitoryPreferences.WantFurnishedDormitory,
                WantGarage = viewModel.GaragePreferences.WantGarage,
                WantMicrowave = viewModel.KitchenPreferences.WantMicrowave,
                WantPrivateBathroom = viewModel.BathroomPreferences.WantPrivateBathroom,
                WantRecreationArea = viewModel.OtherPreferences.WantRecreationArea,
                WantRefrigerator = viewModel.KitchenPreferences.WantRefrigerator,
                WantServiceArea = viewModel.OtherPreferences.WantServiceArea,
                WantSingleDormitory = viewModel.DormitoryPreferences.WantSingleDormitory,
                WantStove = viewModel.KitchenPreferences.WantStove
            };
        }
    }
}