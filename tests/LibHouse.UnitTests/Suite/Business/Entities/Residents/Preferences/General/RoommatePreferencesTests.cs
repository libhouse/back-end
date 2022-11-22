using LibHouse.Business.Entities.Residents.Preferences.General;
using LibHouse.Business.Entities.Users;
using System;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Residents.Preferences.General
{
    [Collection("Business.Entities")]
    public class RoommatePreferencesTests
    {
        [Fact]
        public void RoommatePreferences_ValidRoommatePreferences_ShouldCreateRoommatePreferences()
        {
            int minimumNumberOfRoommatesDesired = 1;
            int maximumNumberOfRoommatesDesired = 5;
            Gender[] acceptedGendersOfRoommates = new[] { Gender.Male, Gender.Female, Gender.Others };
            RoommatePreferences roommatePreferences = new(minimumNumberOfRoommatesDesired, maximumNumberOfRoommatesDesired, acceptedGendersOfRoommates);
            Assert.Equal(minimumNumberOfRoommatesDesired, roommatePreferences.GetMinimumNumberOfRoommatesDesired());
            Assert.Equal(maximumNumberOfRoommatesDesired, roommatePreferences.GetMaximumNumberOfRoommatesDesired());
            Assert.True(roommatePreferences.AcceptsRoommatesOfAllGenders);
        }

        [Fact]
        public void RoommatePreferences_InvalidRangeOfDesiredRoommates_ShouldNotCreateRoommatePreferences()
        {
            int minimumNumberOfRoommatesDesired = 2;
            int maximumNumberOfRoommatesDesired = 1;
            Gender[] acceptedGendersOfRoommates = new[] { Gender.Male, Gender.Female, Gender.Others };
            Assert.ThrowsAny<Exception>(() => new RoommatePreferences(minimumNumberOfRoommatesDesired, maximumNumberOfRoommatesDesired, acceptedGendersOfRoommates));
        }

        [Fact]
        public void RoommatePreferences_AcceptedGendersOfRoommatesNotReported_ShouldNotCreateRoommatePreferences()
        {
            int minimumNumberOfRoommatesDesired = 2;
            int maximumNumberOfRoommatesDesired = 5;
            Gender[] acceptedGendersOfRoommates = Array.Empty<Gender>();
            Assert.ThrowsAny<Exception>(() => new RoommatePreferences(minimumNumberOfRoommatesDesired, maximumNumberOfRoommatesDesired, acceptedGendersOfRoommates));
        }
    }
}