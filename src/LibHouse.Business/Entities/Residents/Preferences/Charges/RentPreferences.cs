using LibHouse.Business.Entities.Shared;

namespace LibHouse.Business.Entities.Residents.Preferences.Charges
{
    public record RentPreferences
    {
        public MonetaryRange RentRange { get; init; }

        public RentPreferences(decimal minimumRentalAmountDesired, decimal maximumRentalAmountDesired)
        {
            RentRange = new(minimumRentalAmountDesired, maximumRentalAmountDesired);
        }

        public decimal GetMinimumRentalAmount()
        {
            return RentRange.MinimumValue;
        }

        public decimal GetMaximumRentalAmount()
        {
            return RentRange.MaximumValue;
        }
    }
}