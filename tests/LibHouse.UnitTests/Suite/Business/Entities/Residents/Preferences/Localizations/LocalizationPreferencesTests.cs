using LibHouse.Business.Entities.Localizations;
using LibHouse.Business.Entities.Residents.Preferences.Localizations;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Residents.Preferences.Localizations
{
    [Collection("Business.Entities")]
    public class LocalizationPreferencesTests
    {
        [Fact]
        public void AddLandmarkPreferences_ValidLandmarkPreferences_ShouldAddLandmarkPreferences()
        {
            LocalizationPreferences localizationPreferences = new();
            string landmarkStreet = "Rua São Bento";
            string landmarkComplement = "de 321 ao fim - lado ímpar";
            ushort landmarkNumber = 321;
            string landmarkNeighborhood = "Centro";
            string landmarkCity = "São Paulo";
            string landmarkFederativeUnit = "SP";
            string landmarkPostalCodeNumber = "01011100";
            Address landmarkAddress = new(landmarkStreet, landmarkNumber, landmarkNeighborhood, landmarkCity, landmarkFederativeUnit, landmarkPostalCodeNumber, landmarkComplement);
            localizationPreferences.AddLandmarkPreferences(landmarkAddress);
            Assert.True(localizationPreferences.HaveLandmarkPreferences());
        }
    }
}