using LibHouse.Business.Entities.Localizations;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Localizations
{
    [Collection("Business.Entities")]
    public class AddressComplementTests
    {
        [Theory]
        [InlineData("Praça da Sé")]
        [InlineData("Lote 04")]
        public void AddressComplement_ValidAddressComplement_ShouldCreateAddressComplement(string description)
        {
            AddressComplement addressComplement = new(description);
            Assert.Equal(description, addressComplement.GetDescription());
        }
    }
}