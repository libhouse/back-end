using LibHouse.Business.Entities.Residents.Preferences.Charges;
using System;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Residents.Preferences.Charges
{
    [Collection("Business.Entities")]
    public class RentPreferencesTests
    {
        [Fact]
        public void RentPreferences_ValidRentPreferences_ShouldCreateRentPreferences()
        {
            decimal minimumRentalAmountDesired = 150.0m;
            decimal maximumRentalAmountDesired = 400.0m;
            RentPreferences rentPreferences = new(minimumRentalAmountDesired, maximumRentalAmountDesired);
            Assert.Equal(minimumRentalAmountDesired, rentPreferences.GetMinimumRentalAmount());
            Assert.Equal(maximumRentalAmountDesired, rentPreferences.GetMaximumRentalAmount());
        }

        [Fact]
        public void RentPreferences_InvalidRentPreferences_ShouldNotCreateRentPreferences()
        {
            decimal minimumRentalAmountDesired = 150.0m;
            decimal maximumRentalAmountDesired = 100.0m;
            Assert.ThrowsAny<ArgumentOutOfRangeException>(() => new RentPreferences(minimumRentalAmountDesired, maximumRentalAmountDesired));
        }
    }
}