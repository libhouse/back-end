using LibHouse.Business.Entities.Residents.Preferences.Rooms;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Residents.Preferences.Rooms
{
    [Collection("Business.Entities")]
    public class KitchenPreferencesTests
    {
        [Theory]
        [InlineData(true, true, true)]
        [InlineData(true, false, true)]
        [InlineData(true, true, false)]
        [InlineData(false, true, true)]
        public void KitchenPreferences_ValidKitchenPreferences_ShouldCreateKitchenPreferences(
            bool requiresStove,
            bool requiresMicrowave,
            bool requiresRefrigerator)
        {
            KitchenPreferences kitchenPreferences = new(requiresStove, requiresMicrowave, requiresRefrigerator);
            Assert.Equal(requiresStove, kitchenPreferences.RequiresStove());
            Assert.Equal(requiresMicrowave, kitchenPreferences.RequiresMicrowave());
            Assert.Equal(requiresRefrigerator, kitchenPreferences.RequiresRefrigerator());
        }
    }
}