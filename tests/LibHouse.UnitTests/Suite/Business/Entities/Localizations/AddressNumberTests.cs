using LibHouse.Business.Entities.Localizations;
using System;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Localizations
{
    [Collection("Business.Entities")]
    public class AddressNumberTests
    {
        [Theory]
        [InlineData(1000)]
        [InlineData(777)]
        public void AddressNumber_ValidAddressNumber_ShouldCreateAddressNumber(ushort number)
        {
            AddressNumber addressNumber = new(number);
            Assert.Equal(number, addressNumber.GetNumber());
        }

        [Theory]
        [InlineData(0)]
        public void AddressNumber_InvalidAddressNumber_ShouldNotCreateAddressNumber(ushort number)
        {
            Assert.ThrowsAny<Exception>(() => new AddressNumber(number));
        }
    }
}