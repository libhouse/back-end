using LibHouse.Business.Entities.Residents.Preferences.Services;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Residents.Preferences.Services
{
    [Collection("Business.Entities")]
    public class TelevisionPreferencesTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void TelevisionPreferences_ValidTelevisionPreferences_ShouldCreateTelevisionPreferences(bool cableTelevisionIsRequired)
        {
            TelevisionPreferences televisionPreferences = new(cableTelevisionIsRequired);
            Assert.Equal(cableTelevisionIsRequired, televisionPreferences.RequiresCableTelevisionService());
        }
    }
}