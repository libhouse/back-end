using LibHouse.Infrastructure.Controllers.ViewModels.Residents;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace LibHouse.API.Filters.Swagger.Requests.Residents
{
    public class ResidentChargePreferencesRegistrationViewModelExample : IExamplesProvider<ResidentChargePreferencesRegistrationViewModel>
    {
        public ResidentChargePreferencesRegistrationViewModel GetExamples()
        {
            return new()
            {
                ResidentId = Guid.NewGuid(),
                MaximumExpenseAmountDesired = 500.0m,
                MinimumExpenseAmountDesired = 150.0m,
                MaximumRentalAmountDesired = 200.0m,
                MinimumRentalAmountDesired = 50.0m
            };
        }
    }
}