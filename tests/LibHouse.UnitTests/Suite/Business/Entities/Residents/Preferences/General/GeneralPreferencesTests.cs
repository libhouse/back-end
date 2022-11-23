using LibHouse.Business.Entities.Residents.Preferences.General;
using LibHouse.Business.Entities.Users;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Residents.Preferences.General
{
    [Collection("Business.Entities")]
    public class GeneralPreferencesTests
    {
        [Fact]
        public void AddAnimalPreferences_ValidAnimalPreferences_ShouldAddAnimalPreferences()
        {
            GeneralPreferences generalPreferences = new();
            generalPreferences.AddAnimalPreferences(wantSpaceForAnimals: true);
            Assert.True(generalPreferences.HaveAnimalPreferences());
        }

        [Fact]
        public void AddChildrenPreferences_ValidChildrenPreferences_ShouldAddChildrenPreferences()
        {
            GeneralPreferences generalPreferences = new();
            generalPreferences.AddChildrenPreferences(acceptChildren: true);
            Assert.True(generalPreferences.HaveChildrenPreferences());
        }

        [Fact]
        public void AddPartyPreferences_ValidPartyPreferences_ShouldAddPartyPreferences()
        {
            GeneralPreferences generalPreferences = new();
            generalPreferences.AddPartyPreferences(wantsToParty: true);
            Assert.True(generalPreferences.HavePartyPreferences());
        }

        [Fact]
        public void AddRoommatePreferences_ValidRoommatePreferences_ShouldAddRoommatePreferences()
        {
            GeneralPreferences generalPreferences = new();
            int minimumNumberOfRoommatesDesired = 1;
            int maximumNumberOfRoommatesDesired = 5;
            Gender[] acceptedGendersOfRoommates = new[] { Gender.Male };
            generalPreferences.AddRoommatePreferences(minimumNumberOfRoommatesDesired, maximumNumberOfRoommatesDesired, acceptedGendersOfRoommates);
            Assert.True(generalPreferences.HaveRoommatePreferences());
        }

        [Fact]
        public void AddSmokersPreferences_ValidSmokersPreferences_ShouldAddSmokersPreferences()
        {
            GeneralPreferences generalPreferences = new();
            generalPreferences.AddSmokersPreferences(acceptSmokers: true);
            Assert.True(generalPreferences.HaveSmokersPreferences());
        }
    }
}