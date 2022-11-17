using LibHouse.Business.Application.Residents.Inputs;
using LibHouse.Infrastructure.Controllers.ViewModels.Residents;

namespace LibHouse.Infrastructure.Controllers.Http.Residents.Converters
{
    internal static class ConverterResidentServicesPreferencesRegistrationViewModel
    {
        internal static InputResidentServicesPreferencesRegistration Convert(this ResidentServicesPreferencesRegistrationViewModel viewModel)
        {
            return new()
            {
                ResidentId = viewModel.ResidentId,
                WantCableTelevisionService = viewModel.WantCableTelevisionService,
                WantHouseCleaningService = viewModel.WantHouseCleaningService,
                WantInternetService = viewModel.WantInternetService
            };
        }
    }
}