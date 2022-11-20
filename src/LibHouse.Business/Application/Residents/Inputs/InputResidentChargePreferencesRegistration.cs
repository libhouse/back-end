using System;

namespace LibHouse.Business.Application.Residents.Inputs
{
    public record InputResidentChargePreferencesRegistration
    {
        public Guid ResidentId { get; init; }
        public decimal MinimumRentalAmountDesired { get; init; }
        public decimal MaximumRentalAmountDesired { get; init; }
        public decimal MinimumExpenseAmountDesired { get; init; }
        public decimal MaximumExpenseAmountDesired { get; init; }
    }
}