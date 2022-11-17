using LibHouse.Business.Entities.Residents.Preferences.Charges;
using System;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Residents.Preferences.Charges
{
    [Collection("Business.Entities")]
    public class ExpensePreferencesTests
    {
        [Fact]
        public void ExpensePreferences_ValidExpensePreferences_ShouldCreateExpensePreferences()
        {
            decimal minimumExpenseAmountDesired = 100.0m;
            decimal maximumExpenseAmountDesired = 200.0m;
            ExpensePreferences expensePreferences = new(minimumExpenseAmountDesired, maximumExpenseAmountDesired);
            Assert.Equal(minimumExpenseAmountDesired, expensePreferences.GetMinimumExpenseAmount());
            Assert.Equal(maximumExpenseAmountDesired, expensePreferences.GetMaximumExpenseAmount());
        }

        [Fact]
        public void ExpensePreferences_InvalidExpensePreferences_ShouldNotCreateExpensePreferences()
        {
            decimal minimumExpenseAmountDesired = 100.0m;
            decimal maximumExpenseAmountDesired = 50.0m;
            Assert.ThrowsAny<ArgumentOutOfRangeException>(() => new ExpensePreferences(minimumExpenseAmountDesired, maximumExpenseAmountDesired));
        }
    }
}