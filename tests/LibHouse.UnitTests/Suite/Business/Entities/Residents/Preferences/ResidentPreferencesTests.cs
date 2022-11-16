using LibHouse.Business.Entities.Residents.Preferences;
using LibHouse.Business.Entities.Residents.Preferences.Rooms;
using LibHouse.Business.Entities.Residents.Preferences.Services;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Residents.Preferences
{
    [Collection("Business.Entities")]
    public class ResidentPreferencesTests
    {
        [Fact]
        public void AddRoomPreferences_ValidRoomPreferences_ShouldAddRoomPreferences()
        {
            ResidentPreferences residentPreferences = new();
            RoomPreferences roomPreferences = new();
            roomPreferences.AddBathroomPreferences(BathroomType.Shared);
            roomPreferences.AddDormitoryPreferences(DormitoryType.Shared, requireFurnishedDormitory: true);
            roomPreferences.AddGaragePreferences(garageIsRequired: true);
            roomPreferences.AddKitchenPreferences(stoveIsRequired: true, microwaveIsRequired: true, refrigeratorIsRequired: true);
            roomPreferences.AddOtherRoomPreferences(serviceAreaIsRequired: true, recreationAreaIsRequired: true);
            residentPreferences.AddRoomPreferences(roomPreferences);
            Assert.True(residentPreferences.HaveRoomPreferences());
        }

        [Fact]
        public void AddServicesPreferences_ValidServicesPreferences_ShouldAddServicesPreferences()
        {
            ResidentPreferences residentPreferences = new();
            ServicesPreferences servicesPreferences = new();
            servicesPreferences.AddCleaningPreferences(houseCleaningIsRequired: true);
            servicesPreferences.AddInternetPreferences(internetServiceIsRequired: false);
            servicesPreferences.AddTelevisionPreferences(cableTelevisionIsRequired: false);
            residentPreferences.AddServicesPreferences(servicesPreferences);
            Assert.True(residentPreferences.HaveServicesPreferences());
        }
    }
}