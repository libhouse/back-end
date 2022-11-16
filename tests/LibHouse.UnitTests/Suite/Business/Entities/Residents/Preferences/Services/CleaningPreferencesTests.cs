using LibHouse.Business.Entities.Residents.Preferences.Services;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Residents.Preferences.Services
{
    [Collection("Business.Entities")]
    public class CleaningPreferencesTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CleaningPreferences_ValidCleaningPreferences_ShouldCreateCleaningPreferences(bool houseCleaningIsRequired)
        {
            CleaningPreferences cleaningPreferences = new(houseCleaningIsRequired);
            Assert.Equal(houseCleaningIsRequired, cleaningPreferences.RequiresHouseCleaningService());
        }
    }
}