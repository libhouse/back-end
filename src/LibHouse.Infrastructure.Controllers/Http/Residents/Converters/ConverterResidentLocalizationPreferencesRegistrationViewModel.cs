using LibHouse.Business.Application.Residents.Inputs;
using LibHouse.Infrastructure.Controllers.ViewModels.Residents;

namespace LibHouse.Infrastructure.Controllers.Http.Residents.Converters
{
    internal static class ConverterResidentLocalizationPreferencesRegistrationViewModel
    {
        internal static InputResidentLocalizationPreferencesRegistration Convert(this ResidentLocalizationPreferencesRegistrationViewModel viewModel)
        {
            return new()
            {
                AddressCity = viewModel.LandmarkAddressCity,
                AddressComplement = viewModel.LandmarkAddressComplement,
                AddressDescription = viewModel.LandmarkAddressDescription,
                AddressFederativeUnit = viewModel.LandmarkAddressFederativeUnit,
                AddressNeighborhood = viewModel.LandmarkAddressNeighborhood,
                AddressNumber = viewModel.LandmarkAddressNumber,
                AddressPostalCodeNumber = viewModel.LandmarkAddressPostalCodeNumber,
                ResidentId = viewModel.ResidentId
            };
        }
    }
}