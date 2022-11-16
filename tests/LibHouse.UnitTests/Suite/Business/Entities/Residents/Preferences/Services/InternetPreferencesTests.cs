using LibHouse.Business.Entities.Residents.Preferences.Services;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Residents.Preferences.Services
{
    [Collection("Business.Entities")]
    public class InternetPreferencesTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void InternetPreferences_ValidInternetPreferences_ShouldCreateInternetPreferences(bool internetServiceIsRequired)
        {
            InternetPreferences internetPreferences = new(internetServiceIsRequired);
            Assert.Equal(internetServiceIsRequired, internetPreferences.RequiresInternetService());
        }
    }
}