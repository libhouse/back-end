using LibHouse.Business.Entities.Owners;
using LibHouse.Business.Entities.Users;
using System;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Owners
{
    public class OwnerTests
    {
        [Fact]
        public void CreateOwner_ValidOwnerData_ShouldCreateOwner()
        {
            Owner owner = new(
                name: "Jyn",
                lastName: "Erso", 
                birthDate: new DateTime(1986, 9, 20), 
                gender: Gender.Female, 
                phone: "(54) 3424-4016", 
                email: "jynerso@gmail.com", 
                cpf: "96087950010"
            );

            Assert.Equal(Gender.Female, owner.Gender);
            Assert.Equal("Jyn", owner.Name);
            Assert.Equal("(54) 3424-4016", owner.GetPhoneNumber());
            Assert.Equal("jynerso@gmail.com", owner.GetEmailAddress());
            Assert.Equal("96087950010", owner.GetCpfNumber());
        }

        [Fact]
        public void CreateOwner_InvalidName_ShouldThrowException()
        {
            Assert.ThrowsAny<Exception>(() =>
            {
                new Owner(
                    name: string.Empty,
                    lastName: "Erso",
                    birthDate: new DateTime(1986, 9, 20),
                    gender: Gender.Female,
                    phone: "(54) 3424-4016",
                    email: "jynerso@gmail.com",
                    cpf: "96087950010"
                );
            });
        }

        [Fact]
        public void CreateOwner_InvalidLastName_ShouldThrowException()
        {
            Assert.ThrowsAny<Exception>(() =>
            {
                new Owner(
                    name: "Jyn",
                    lastName: "",
                    birthDate: new DateTime(1986, 9, 20),
                    gender: Gender.Female,
                    phone: "(54) 3424-4016",
                    email: "jynerso@gmail.com",
                    cpf: "96087950010"
                );
            });
        }

        [Theory]
        [InlineData("")]
        [InlineData("(411) 99778-2914")]
        public void CreateOwner_InvalidPhone_ShouldThrowException(string phoneNumber)
        {
            Assert.ThrowsAny<Exception>(() =>
            {
                new Owner(
                    name: "Jyn",
                    lastName: "Erso",
                    birthDate: new DateTime(1986, 9, 20),
                    gender: Gender.Female,
                    phone: phoneNumber,
                    email: "jynerso@gmail.com",
                    cpf: "96087950010"
                );
            });
        }

        [Theory]
        [InlineData("")]
        [InlineData("bailorgana.com")]
        [InlineData("bailorgana@.com")]
        [InlineData("bailorgana@gmail")]
        public void CreateOwner_InvalidEmail_ShouldThrowException(string email)
        {
            Assert.ThrowsAny<Exception>(() =>
            {
                new Owner(
                    name: "Jyn",
                    lastName: "Erso",
                    birthDate: new DateTime(1986, 9, 20),
                    gender: Gender.Female,
                    phone: "(11) 9876-1089",
                    email: email,
                    cpf: "96087950010"
                );
            });
        }

        [Theory]
        [InlineData("")]
        [InlineData("960879500101")]
        [InlineData("9608795001")]
        [InlineData("96087950011")]
        public void CreateOwner_InvalidCpf_ShouldThrowException(string cpf)
        {
            Assert.ThrowsAny<Exception>(() =>
            {
                new Owner(
                    name: "Jyn",
                    lastName: "Erso",
                    birthDate: new DateTime(1986, 9, 20),
                    gender: Gender.Female,
                    phone: "(11) 9876-1089",
                    email: "jynerso@gmail.com",
                    cpf: cpf
                );
            });
        }
    }
}