using LibHouse.Business.Entities.Residents.Preferences.Rooms;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Residents.Preferences.Rooms
{
    public class DormitoryPreferencesTests
    {
        [Theory]
        [InlineData(DormitoryType.Single, true)]
        [InlineData(DormitoryType.Single, false)]
        [InlineData(DormitoryType.Shared, true)]
        [InlineData(DormitoryType.Shared, false)]
        public void DormitoryPreferences_ValidDormitoryPreferences_ShouldCreateDormitoryPreferences(
            DormitoryType dormitoryType,
            bool requireFurnishedDormitory)
        {
            DormitoryPreferences dormitoryPreferences = new(dormitoryType, requireFurnishedDormitory);
            Assert.Equal(dormitoryType, dormitoryPreferences.GetDormitoryType());
            Assert.Equal(requireFurnishedDormitory, dormitoryPreferences.RequiresFurnishedDormitory());
        }
    }
}