using System;

namespace LibHouse.Infrastructure.Controllers.ViewModels.Residents
{
    public record ResidentChargePreferencesRegistrationViewModel
    {
        public Guid ResidentId { get; init; }
        public decimal MinimumRentalAmountDesired { get; init; }
        public decimal MaximumRentalAmountDesired { get; init; }
        public decimal MinimumExpenseAmountDesired { get; init; }
        public decimal MaximumExpenseAmountDesired { get; init; }
    }
}