using LibHouse.Business.Entities.Shared;

namespace LibHouse.Business.Entities.Residents.Preferences.Charges
{
    public record ExpensePreferences
    {
        public MonetaryRange ExpenseRange { get; init; }

        public ExpensePreferences(
            decimal minimumExpenseAmountDesired,
            decimal maximumExpenseAmountDesired)
        {
            ExpenseRange = new(minimumExpenseAmountDesired, maximumExpenseAmountDesired);
        }

        private ExpensePreferences() { }

        public decimal GetMinimumExpenseAmount()
        {
            return ExpenseRange.MinimumValue;
        }

        public decimal GetMaximumExpenseAmount()
        {
            return ExpenseRange.MaximumValue;
        }
    }
}