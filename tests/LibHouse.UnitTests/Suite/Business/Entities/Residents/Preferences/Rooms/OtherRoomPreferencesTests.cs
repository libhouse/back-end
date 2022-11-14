using LibHouse.Business.Entities.Residents.Preferences.Rooms;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Residents.Preferences.Rooms
{
    [Collection("Business.Entities")]
    public class OtherRoomPreferencesTests
    {
        [Theory]
        [InlineData(true, true)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(false, false)]
        public void OtherRoomPreferences_ValidOtherRoomPreferences_ShouldCreateOtherRoomPreferences(
            bool serviceAreaIsRequired,
            bool recreationAreaIsRequired)
        {
            OtherRoomPreferences otherRoomPreferences = new(serviceAreaIsRequired, recreationAreaIsRequired);
            Assert.Equal(serviceAreaIsRequired, otherRoomPreferences.RequiresServiceArea());
            Assert.Equal(recreationAreaIsRequired, otherRoomPreferences.RequiresRecreationArea());
        }
    }
}