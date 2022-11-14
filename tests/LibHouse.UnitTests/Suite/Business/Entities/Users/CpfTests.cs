using LibHouse.Business.Entities.Users;
using System;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Users
{
    [Collection("Business.Entities")]
    public class CpfTests
    {
        [Theory]
        [InlineData("432.400.810-83")]
        [InlineData("58086163016")]
        [InlineData("19579729042")]
        public void CreateFromDocument_ValidDocumentNumber_ShouldCreateCpf(string documentNumber)
        {
            Cpf cpf = Cpf.CreateFromDocument(documentNumber);
            Assert.Equal(documentNumber, cpf.Value);
        }

        [Theory]
        [InlineData("")]
        [InlineData("11111111111")]
        [InlineData("68086163016")]
        [InlineData("5808616301")]
        [InlineData("580861630161")]
        public void CreateFromDocument_InvalidDocumentNumber_ShouldThrowException(string documentNumber)
        {
            Assert.ThrowsAny<Exception>(() => Cpf.CreateFromDocument(documentNumber));
        }
    }
}