using LibHouse.Business.Entities.Residents.Preferences.Rooms;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Residents.Preferences.Rooms
{
    [Collection("Business.Entities")]
    public class GaragePreferencesTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GaragePreferences_ValidGaragePreferences_ShouldCreateGaragePreferences(bool garageIsRequired)
        {
            GaragePreferences garagePreferences = new(garageIsRequired);
            Assert.Equal(garageIsRequired, garagePreferences.RequiresGarage());
        }
    }
}