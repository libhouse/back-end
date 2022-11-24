using LibHouse.Business.Application.Residents.Inputs;
using LibHouse.Infrastructure.Controllers.ViewModels.Residents;

namespace LibHouse.Infrastructure.Controllers.Http.Residents.Converters
{
    internal static class ConverterResidentGeneralPreferencesRegistrationViewModel
    {
        internal static InputResidentGeneralPreferencesRegistration Convert(this ResidentGeneralPreferencesRegistrationViewModel viewModel)
        {
            return new()
            {
                AcceptChildren = viewModel.AcceptChildren,
                AcceptSmokers = viewModel.AcceptSmokers,
                AcceptsOnlyMenAsRoommates = viewModel.AcceptsOnlyMenAsRoommates,
                AcceptsOnlyWomenAsRoommates = viewModel.AcceptsOnlyWomenAsRoommates,
                MaximumNumberOfRoommatesDesired = viewModel.MaximumNumberOfRoommatesDesired,
                MinimumNumberOfRoommatesDesired = viewModel.MinimumNumberOfRoommatesDesired,
                ResidentId = viewModel.ResidentId,
                WantSpaceForAnimals = viewModel.WantSpaceForAnimals,
                WantsToParty = viewModel.WantsToParty
            };
        }
    }
}