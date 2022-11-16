using LibHouse.Business.Entities.Residents.Preferences.Services;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Residents.Preferences.Services
{
    [Collection("Business.Entities")]
    public class ServicesPreferencesTests
    {
        [Fact]
        public void AddCleaningPreferences_ValidCleaningPreferences_ShouldAddCleaningPreferences()
        {
            ServicesPreferences servicesPreferences = new();
            servicesPreferences.AddCleaningPreferences(houseCleaningIsRequired: true);
            Assert.True(servicesPreferences.HaveCleaningPreferences());
        }

        [Fact]
        public void AddInternetPreferences_ValidInternetPreferences_ShouldAddInternetPreferences()
        {
            ServicesPreferences servicesPreferences = new();
            servicesPreferences.AddInternetPreferences(internetServiceIsRequired: true);
            Assert.True(servicesPreferences.HaveInternetPreferences());
        }

        [Fact]
        public void AddTelevisionPreferences_ValidTelevisionPreferences_ShouldAddTelevisionPreferences()
        {
            ServicesPreferences servicesPreferences = new();
            servicesPreferences.AddTelevisionPreferences(cableTelevisionIsRequired: true);
            Assert.True(servicesPreferences.HaveTelevisionPreferences());
        }
    }
}