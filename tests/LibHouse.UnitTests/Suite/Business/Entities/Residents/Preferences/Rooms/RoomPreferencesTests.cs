using LibHouse.Business.Entities.Residents.Preferences.Rooms;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Residents.Preferences.Rooms
{
    [Collection("Business.Entities")]
    public class RoomPreferencesTests
    {
        [Fact]
        public void AddDormitoryPreferences_ValidDormitoryPreferences_ShouldAddDormitoryPreferences()
        {
            RoomPreferences roomPreferences = new();
            roomPreferences.AddDormitoryPreferences(DormitoryType.Shared, requireFurnishedDormitory: true);
            Assert.True(roomPreferences.HaveDormitoryPreferences());
        }

        [Fact]
        public void AddBathroomPreferences_ValidBathroomPreferences_ShouldAddBathroomPreferences()
        {
            RoomPreferences roomPreferences = new();
            roomPreferences.AddBathroomPreferences(BathroomType.Shared);
            Assert.True(roomPreferences.HaveBathroomPreferences());
        }

        [Fact]
        public void AddGaragePreferences_ValidGaragePreferences_ShouldAddGaragePreferences()
        {
            RoomPreferences roomPreferences = new();
            roomPreferences.AddGaragePreferences(garageIsRequired: true);
            Assert.True(roomPreferences.HaveGaragePreferences());
        }

        [Fact]
        public void AddKitchenPreferences_ValidKitchenPreferences_ShouldAddKitchenPreferences()
        {
            RoomPreferences roomPreferences = new();
            roomPreferences.AddKitchenPreferences(stoveIsRequired: true, microwaveIsRequired: true, refrigeratorIsRequired: false);
            Assert.True(roomPreferences.HaveKitchenPreferences());
        }

        [Fact]
        public void AddOtherRoomPreferences_ValidOtherRoomPreferences_ShouldAddOtherRoomPreferences()
        {
            RoomPreferences roomPreferences = new();
            roomPreferences.AddOtherRoomPreferences(serviceAreaIsRequired: true, recreationAreaIsRequired: true);
            Assert.True(roomPreferences.HaveOtherRoomPreferences());
        }
    }
}