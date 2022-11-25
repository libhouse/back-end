using LibHouse.Business.Entities.Localizations;
using System;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Localizations
{
    [Collection("Business.Entities")]
    public class AddressTests
    {
        [Theory]
        [InlineData("Rua Hidrolândia", 101, "Tucuruvi", "São Paulo", "SP", "02307210")]
        [InlineData("Praça da Sé", 200, "Sé", "São Paulo", "SP", "01001-000", "lado ímpar")]
        public void Street_ValidStreet_ShouldCreateStreet(
            string name, 
            ushort number, 
            string streetNeighborhood, 
            string streetCity, 
            string abbreviationOfCityFederativeUnit, 
            string postalCode, 
            string complement = null)
        {
            Address street = new(name, number, streetNeighborhood, streetCity, abbreviationOfCityFederativeUnit, postalCode, complement);
            Assert.Equal(name, street.GetName());
            Assert.Equal(streetNeighborhood, street.GetNeighborhoodName());
            Assert.Equal(streetCity, street.GetCityName());
            Assert.Equal(abbreviationOfCityFederativeUnit, street.GetAbbreviationOfTheFederativeUnit());
            Assert.Equal(number, street.GetNumber());
            Assert.Equal(postalCode.Replace("-", ""), street.GetPostalCodeNumber());
            Assert.Equal(complement, street.GetComplement());
        }

        [Theory]
        [InlineData("", 100, "Tucuruvi", "São Paulo", "SP", "02307210")]
        [InlineData("Nome para o logradouro com um comprimento maior do que o permitido", 100, "Tucuruvi", "São Paulo", "SP", "02307210")]
        [InlineData("Logradouro 1000", 100, "Tucuruvi", "São Paulo", "SP", "02307210")]
        [InlineData("Rua Hidrolândia", 100, "", "São Paulo", "SP", "02307210")]
        [InlineData("Rua Hidrolândia", 100, "Tucuruvi", "", "SP", "02307210")]
        [InlineData("Rua Hidrolândia", 100, "Tucuruvi", "São Paulo", "", "02307210")]
        [InlineData("Rua Hidrolândia", 101, "Tucuruvi", "São Paulo", "SP", "")]
        public void Street_InvalidStreet_ShouldNotCreateStreet(
            string name,
            ushort number,
            string streetNeighborhood,
            string streetCity,
            string abbreviationOfCityFederativeUnit,
            string postalCode,
            string complement = null)
        {
            Assert.ThrowsAny<Exception>(() => new Address(name, number, streetNeighborhood, streetCity, abbreviationOfCityFederativeUnit, postalCode, complement));
        }
    }
}