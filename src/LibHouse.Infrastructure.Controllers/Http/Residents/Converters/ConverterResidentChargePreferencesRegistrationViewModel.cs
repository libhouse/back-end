using LibHouse.Business.Application.Residents.Inputs;
using LibHouse.Infrastructure.Controllers.ViewModels.Residents;

namespace LibHouse.Infrastructure.Controllers.Http.Residents.Converters
{
    internal static class ConverterResidentChargePreferencesRegistrationViewModel
    {
        internal static InputResidentChargePreferencesRegistration Convert(this ResidentChargePreferencesRegistrationViewModel viewModel)
        {
            return new()
            {
                MaximumExpenseAmountDesired = viewModel.MaximumExpenseAmountDesired,
                MaximumRentalAmountDesired = viewModel.MaximumRentalAmountDesired,
                MinimumExpenseAmountDesired = viewModel.MinimumExpenseAmountDesired,
                MinimumRentalAmountDesired = viewModel.MinimumRentalAmountDesired,
                ResidentId = viewModel.ResidentId
            };
        }
    }
}