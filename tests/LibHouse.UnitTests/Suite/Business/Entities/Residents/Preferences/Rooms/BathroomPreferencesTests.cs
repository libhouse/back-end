using LibHouse.Business.Entities.Residents.Preferences.Rooms;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Residents.Preferences.Rooms
{
    public class BathRoomPreferencesTests
    {
        [Theory]
        [InlineData(BathroomType.Single)]
        [InlineData(BathroomType.Shared)]
        public void BathroomPreferences_ValidBathroomPreferences_ShouldCreateBathroomPreferences(BathroomType bathroomType)
        {
            BathroomPreferences bathroomPreferences = new(bathroomType);
            Assert.Equal(bathroomType, bathroomPreferences.GetBathroomType());
        }
    }
}