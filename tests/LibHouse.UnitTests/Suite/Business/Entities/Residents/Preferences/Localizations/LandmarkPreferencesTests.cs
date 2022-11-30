using LibHouse.Business.Entities.Localizations;
using LibHouse.Business.Entities.Residents.Preferences.Localizations;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Residents.Preferences.Localizations
{
    [Collection("Business.Entities")]
    public class LandmarkPreferencesTests
    {
        [Fact]
        public void LandmarkPreferences_ValidLandmarkPreferences_ShouldCreateLandmarkPreferences()
        {
            string landmarkStreet = "Rua São Bento";
            string landmarkComplement = "de 321 ao fim - lado ímpar";
            ushort landmarkNumber = 321;
            string landmarkNeighborhood = "Centro";
            string landmarkCity = "São Paulo";
            string landmarkFederativeUnit = "SP";
            string landmarkPostalCodeNumber = "01011100";
            Address landmarkAddress = new(landmarkStreet, landmarkNumber, landmarkNeighborhood, landmarkCity, landmarkFederativeUnit, landmarkPostalCodeNumber, landmarkComplement);
            LandmarkPreferences landmarkPreferences = new(landmarkAddress);
            Assert.Equal(landmarkStreet, landmarkPreferences.GetLandmarkAddress());
            Assert.Equal(landmarkNumber, landmarkPreferences.GetLandmarkNumber());
            Assert.Equal(landmarkNeighborhood, landmarkPreferences.GetLandmarkNeighborhood());
            Assert.Equal(landmarkCity, landmarkPreferences.GetLandmarkCity());
            Assert.Equal(landmarkFederativeUnit, landmarkPreferences.GetLandmarkFederativeUnit());
            Assert.Equal(landmarkPostalCodeNumber, landmarkPreferences.GetLandmarkPostalCodeNumber());
            Assert.Equal(landmarkComplement, landmarkPreferences.GetLandmarkComplement());
            Assert.Equal(landmarkAddress.Id, landmarkPreferences.LandmarkAddressId);
        }
    }
}