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
        public void Address_ValidAddress_ShouldCreateAddress(
            string name, 
            ushort number, 
            string addressNeighborhood, 
            string addressCity, 
            string abbreviationOfCityFederativeUnit, 
            string postalCode, 
            string complement = null)
        {
            Address address = new(name, number, addressNeighborhood, addressCity, abbreviationOfCityFederativeUnit, postalCode, complement);
            Assert.Equal(name, address.GetName());
            Assert.Equal(addressNeighborhood, address.GetNeighborhoodName());
            Assert.Equal(addressCity, address.GetCityName());
            Assert.Equal(abbreviationOfCityFederativeUnit, address.GetAbbreviationOfTheFederativeUnit());
            Assert.Equal(number, address.GetNumber());
            Assert.Equal(postalCode.Replace("-", ""), address.GetPostalCodeNumber());
            Assert.Equal(complement, address.GetComplement());
        }

        [Theory]
        [InlineData("", 100, "Tucuruvi", "São Paulo", "SP", "02307210")]
        [InlineData("Nome para o logradouro com um comprimento maior do que o permitido", 100, "Tucuruvi", "São Paulo", "SP", "02307210")]
        [InlineData("Logradouro 1000", 100, "Tucuruvi", "São Paulo", "SP", "02307210")]
        [InlineData("Rua Hidrolândia", 100, "", "São Paulo", "SP", "02307210")]
        [InlineData("Rua Hidrolândia", 100, "Tucuruvi", "", "SP", "02307210")]
        [InlineData("Rua Hidrolândia", 100, "Tucuruvi", "São Paulo", "", "02307210")]
        [InlineData("Rua Hidrolândia", 101, "Tucuruvi", "São Paulo", "SP", "")]
        public void Address_InvalidAddress_ShouldNotCreateAddress(
            string name,
            ushort number,
            string addressNeighborhood,
            string addressCity,
            string abbreviationOfCityFederativeUnit,
            string postalCode,
            string complement = null)
        {
            Assert.ThrowsAny<Exception>(() => new Address(name, number, addressNeighborhood, addressCity, abbreviationOfCityFederativeUnit, postalCode, complement));
        }
    }
}