using LibHouse.Business.Entities.Residents.Preferences.General;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Residents.Preferences.General
{
    [Collection("Business.Entities")]
    public record PartyPreferencesTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void PartyPreferences_ValidPartyPreferences_ShouldCreatePartyPreferences(bool wantsToParty)
        {
            PartyPreferences partyPreferences = new(wantsToParty);
            Assert.Equal(wantsToParty, partyPreferences.RequiresAcceptanceOfParties());
        }
    }
}