using LibHouse.Business.Entities.Users;
using System;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Users
{
    [Collection("Business.Entities")]
    public class PhoneTests
    {
        [Theory]
        [InlineData("(11) 98564-3280")]
        [InlineData("11985643280")]
        [InlineData("(11) 9856-3280")]
        public void CreateFromNumber_ValidNumber_ShouldCreatePhone(string number)
        {
            Phone phone = Phone.CreateFromNumber(number);
            Assert.Equal(number, phone.Value);
        }

        [Theory]
        [InlineData("")]
        [InlineData("(11) 98564-32801")]
        [InlineData("(11) 985640-3280")]
        [InlineData("98564-3280")]
        public void CreateFromNumber_InvalidNumber_ShouldThrowException(string number)
        {
            Assert.ThrowsAny<Exception>(() => Phone.CreateFromNumber(number));
        }
    }
}