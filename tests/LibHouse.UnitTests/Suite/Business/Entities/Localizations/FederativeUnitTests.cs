using LibHouse.Business.Entities.Localizations;
using System;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Localizations
{
    [Collection("Business.Entities")]
    public class FederativeUnitTests
    {
        [Theory]
        [InlineData("SP")]
        [InlineData("RJ")]
        public void FederativeUnit_ValidFederativeUnit_ShouldCreateFederativeUnit(string abbreviationOfTheFederativeUnit)
        {
            FederativeUnit federativeUnit = new(abbreviationOfTheFederativeUnit);
            Assert.Equal(abbreviationOfTheFederativeUnit, federativeUnit.GetAbbreviation());
        }

        [Theory]
        [InlineData("")]
        [InlineData("SSP")]
        [InlineData("S")]
        [InlineData("22")]
        public void FederativeUnit_InvalidFederativeUnit_ShouldNotCreateFederativeUnit(string abbreviationOfTheFederativeUnit)
        {
            Assert.ThrowsAny<Exception>(() => new FederativeUnit(abbreviationOfTheFederativeUnit));
        }
    }
}