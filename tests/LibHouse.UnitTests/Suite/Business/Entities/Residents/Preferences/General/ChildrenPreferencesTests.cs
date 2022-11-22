using LibHouse.Business.Entities.Residents.Preferences.General;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Residents.Preferences.General
{
    [Collection("Business.Entities")]
    public class ChildrenPreferencesTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ChildrenPreferences_ValidChildrenPreferences_ShouldCreateChildrenPreferences(bool acceptChildren)
        {
            ChildrenPreferences childrenPreferences = new(acceptChildren);
            Assert.Equal(acceptChildren, childrenPreferences.RequiresAcceptanceOfChildren());
        }
    }
}