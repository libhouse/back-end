using LibHouse.Business.Entities.Residents.Preferences.Charges;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Residents.Preferences.Charges
{
    [Collection("Business.Entities")]
    public class ChargePreferencesTests
    {
        [Fact]
        public void AddRentPreferences_ValidRentPreferences_ShouldAddRentPreferences()
        {
            ChargePreferences chargePreferences = new();
            chargePreferences.AddRentPreferences(minimumRentalAmountDesired: 150.0m, maximumRentalAmountDesired: 350.0m);
            Assert.True(chargePreferences.HaveRentPreferences());
        }

        [Fact]
        public void AddExpensePreferences_ValidExpensePreferences_ShouldAddExpensePreferences()
        {
            ChargePreferences chargePreferences = new();
            chargePreferences.AddExpensePreferences(minimumExpenseAmountDesired: 150.0m, maximumExpenseAmountDesired: 350.0m);
            Assert.True(chargePreferences.HaveExpensePreferences());
        }
    }
}