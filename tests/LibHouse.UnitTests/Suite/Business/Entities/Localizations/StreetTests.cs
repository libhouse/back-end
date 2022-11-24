using LibHouse.Business.Entities.Localizations;
using System;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Localizations
{
    [Collection("Business.Entities")]
    public class StreetTests
    {
        [Theory]
        [InlineData("Rua Hidrolândia", "Tucuruvi", "São Paulo", "SP")]
        [InlineData("Rua Ipanema", "Ipanema", "Rio de Janeiro", "RJ")]
        public void Street_ValidStreet_ShouldCreateStreet(string name, string streetNeighborhood, string streetCity, string abbreviationOfCityFederativeUnit)
        {
            Street street = new(name, streetNeighborhood, streetCity, abbreviationOfCityFederativeUnit);
            Assert.Equal(name, street.GetName());
            Assert.Equal(streetNeighborhood, street.GetNeighborhoodName());
            Assert.Equal(streetCity, street.GetCityName());
            Assert.Equal(abbreviationOfCityFederativeUnit, street.GetAbbreviationOfTheFederativeUnit());
        }

        [Theory]
        [InlineData("", "Tucuruvi", "São Paulo", "SP")]
        [InlineData("Nome para o logradouro com um comprimento maior do que o permitido", "Ipanema", "Rio de Janeiro", "RJ")]
        [InlineData("Logradouro 1000", "Tucuruvi", "São Paulo", "SP")]
        [InlineData("Rua Hidrolândia", "", "São Paulo", "SP")]
        [InlineData("Rua Hidrolândia", "Tucuruvi", "", "SP")]
        [InlineData("Rua Hidrolândia", "Tucuruvi", "São Paulo", "")]
        public void Street_InvalidStreet_ShouldNotCreateStreet(string name, string streetNeighborhood, string streetCity, string abbreviationOfCityFederativeUnit)
        {
            Assert.ThrowsAny<Exception>(() => new Street(name, streetNeighborhood, streetCity, abbreviationOfCityFederativeUnit));
        }
    }
}