using LibHouse.Business.Entities.Users;
using System;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Users
{
    [Collection("Business.Entities")]
    public class EmailTests
    {
        [Theory]
        [InlineData("lukeskywalker@gmail.com")]
        [InlineData("luke.skywalker@hotmail.com.br")]
        [InlineData("luke-skywalker@yahoo.com")]
        public void CreateFromAddress_ValidAddress_ShouldCreateEmail(string address)
        {
            Email email = Email.CreateFromAddress(address);
            Assert.Equal(address, email.Value);
        }

        [Theory]
        [InlineData("")]
        [InlineData("luke.skywalker")]
        [InlineData("luke.skywalker.com")]
        [InlineData("lukeskywalker@.com")]
        [InlineData("lukeskywalker@gmail")]
        public void CreateFromAddress_InvalidAddress_ShouldThrowException(string address)
        {
            Assert.ThrowsAny<Exception>(() => Email.CreateFromAddress(address));
        }
    }
}