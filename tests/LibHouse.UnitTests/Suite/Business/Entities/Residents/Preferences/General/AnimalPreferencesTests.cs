using LibHouse.Business.Entities.Residents.Preferences.General;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Residents.Preferences.General
{
    [Collection("Business.Entities")]
    public class AnimalPreferencesTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void AnimalPreferences_ValidAnimalPreferences_ShouldCreateAnimalPreferences(bool wantSpaceForAnimals)
        {
            AnimalPreferences animalPreferences = new(wantSpaceForAnimals);
            Assert.Equal(wantSpaceForAnimals, animalPreferences.RequiresAcceptanceOfAnimals());
        }
    }
}