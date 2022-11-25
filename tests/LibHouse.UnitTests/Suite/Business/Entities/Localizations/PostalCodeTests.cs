using LibHouse.Business.Entities.Localizations;
using System;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Localizations
{
    [Collection("Business.Entities")]
    public class PostalCodeTests
    {
        [Theory]
        [InlineData("01001-000")]
        [InlineData("02307210")]
        public void PostalCode_ValidPostalCode_ShouldCreatePostalCode(string postalCodeNumber)
        {
            PostalCode postalCode = new(postalCodeNumber);
            Assert.Equal(postalCodeNumber.Replace("-", ""), postalCode.GetNumber());
        }

        [Theory]
        [InlineData("")]
        [InlineData("0230721")]
        [InlineData("023072101")]
        [InlineData("01001-0000")]
        [InlineData("010010-00")]
        public void PostalCode_InvalidPostalCode_ShouldNotCreatePostalCode(string postalCodeNumber)
        {
            Assert.ThrowsAny<Exception>(() => new PostalCode(postalCodeNumber));
        }
    }
}