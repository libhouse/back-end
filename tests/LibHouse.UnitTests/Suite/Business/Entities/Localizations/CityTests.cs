using LibHouse.Business.Entities.Localizations;
using System;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Localizations
{
    [Collection("Business.Entities")]
    public class CityTests
    {
        [Theory]
        [InlineData("São Paulo", "SP")]
        [InlineData("Rio de Janeiro", "RJ")]
        public void City_ValidCity_ShouldCreateCity(string name, string abbreviationOfCityFederativeUnit)
        {
            City city = new(name, abbreviationOfCityFederativeUnit);
            Assert.Equal(name, city.GetName());
            Assert.Equal(abbreviationOfCityFederativeUnit, city.GetAbbreviationOfTheFederativeUnit());
        }

        [Theory]
        [InlineData("Cidade 1000", "SP")]
        [InlineData("", "RJ")]
        [InlineData("Nome de cidade com tamanho maior do que o permitido", "RJ")]
        [InlineData("São Paulo", "")]
        public void City_InvalidCity_ShouldNotCreateCity(string name, string abbreviationOfCityFederativeUnit)
        {
            Assert.ThrowsAny<Exception>(() => new City(name, abbreviationOfCityFederativeUnit));
        }
    }
}