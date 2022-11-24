using LibHouse.Business.Entities.Localizations;
using System;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Localizations
{
    [Collection("Business.Entities")]
    public class NeighborhoodTests
    {
        [Theory]
        [InlineData("Tucuruvi", "São Paulo", "SP")]
        [InlineData("Ipanema", "Rio de Janeiro", "RJ")]
        public void Neighborhood_ValidNeighborhood_ShouldCreateNeighborhood(string name, string neighborhoodCity, string abbreviationOfCityFederativeUnit)
        {
            Neighborhood neighborhood = new(name, neighborhoodCity, abbreviationOfCityFederativeUnit);
            Assert.Equal(name, neighborhood.GetName());
            Assert.Equal(neighborhoodCity, neighborhood.GetCityName());
            Assert.Equal(abbreviationOfCityFederativeUnit, neighborhood.GetAbbreviationOfTheFederativeUnit());
        }

        [Theory]
        [InlineData("", "São Paulo", "SP")]
        [InlineData("Nome para o bairro com um comprimento maior do que o permitido", "Rio de Janeiro", "RJ")]
        [InlineData("Bairro 1000", "São Paulo", "SP")]
        [InlineData("Tucuruvi", "", "SP")]
        [InlineData("Tucuruvi", "São Paulo", "")]
        public void Neighborhood_InvalidNeighborhood_ShouldNotCreateNeighborhood(string name, string neighborhoodCity, string abbreviationOfCityFederativeUnit)
        {
            Assert.ThrowsAny<Exception>(() => new Neighborhood(name, neighborhoodCity, abbreviationOfCityFederativeUnit));
        }
    }
}