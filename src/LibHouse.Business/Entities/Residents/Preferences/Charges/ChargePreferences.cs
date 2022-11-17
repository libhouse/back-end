namespace LibHouse.Business.Entities.Residents.Preferences.Charges
{
    public class ChargePreferences
    {
        public RentPreferences RentPreferences { get; private set; }
        public ExpensePreferences ExpensePreferences { get; private set; }

        public void AddRentPreferences(decimal minimumRentalAmountDesired, decimal maximumRentalAmountDesired)
        {
            RentPreferences = new(minimumRentalAmountDesired, maximumRentalAmountDesired);
        }

        public bool HaveRentPreferences()
        {
            return RentPreferences is not null;
        }

        public void AddExpensePreferences(decimal minimumExpenseAmountDesired, decimal maximumExpenseAmountDesired)
        {
            ExpensePreferences = new(minimumExpenseAmountDesired, maximumExpenseAmountDesired);
        }

        public bool HaveExpensePreferences()
        {
            return ExpensePreferences is not null;
        }
    }
}