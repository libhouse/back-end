using LibHouse.Business.Entities.Residents.Preferences.General;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Residents.Preferences.General
{
    [Collection("Business.Entities")]
    public class SmokersPreferencesTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SmokersPreferences_ValidSmokersPreferences_ShouldCreateSmokersPreferences(bool acceptSmokers)
        {
            SmokersPreferences smokersPreferences = new(acceptSmokers);
            Assert.Equal(acceptSmokers, smokersPreferences.RequiresAcceptanceOfSmokers());
        }
    }
}