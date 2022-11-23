using LibHouse.Business.Entities.Residents.Preferences;
using LibHouse.Business.Entities.Residents.Preferences.Charges;
using LibHouse.Business.Entities.Residents.Preferences.General;
using LibHouse.Business.Entities.Residents.Preferences.Rooms;
using LibHouse.Business.Entities.Residents.Preferences.Services;
using LibHouse.Business.Entities.Users;
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

        [Fact]
        public void AddChargePreferences_ValidChargePreferences_ShouldAddChargePreferences()
        {
            ResidentPreferences residentPreferences = new();
            ChargePreferences chargePreferences = new();
            chargePreferences.AddRentPreferences(minimumRentalAmountDesired: 150.0m, maximumRentalAmountDesired: 350.0m);
            chargePreferences.AddExpensePreferences(minimumExpenseAmountDesired: 150.0m, maximumExpenseAmountDesired: 350.0m);
            residentPreferences.AddChargePreferences(chargePreferences);
            Assert.True(residentPreferences.HaveChargePreferences());
        }

        [Fact]
        public void AddGeneralPreferences_ValidGeneralPreferences_ShouldAddGeneralPreferences()
        {
            ResidentPreferences residentPreferences = new();
            GeneralPreferences generalPreferences = new();
            generalPreferences.AddAnimalPreferences(wantSpaceForAnimals: true);
            generalPreferences.AddChildrenPreferences(acceptChildren: true);
            generalPreferences.AddPartyPreferences(wantsToParty: true);
            generalPreferences.AddRoommatePreferences(1, 4, new[] { Gender.Male });
            generalPreferences.AddSmokersPreferences(acceptSmokers: true);
            residentPreferences.AddGeneralPreferences(generalPreferences);
            Assert.True(residentPreferences.HaveGeneralPreferences());
        }
    }
}