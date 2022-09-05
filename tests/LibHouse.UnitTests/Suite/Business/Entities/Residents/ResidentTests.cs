using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Users;
using System;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Entities.Residents
{
    public class ResidentTests
    {
        [Fact]
        public void CreateResident_ValidResidentData_ShouldCreateResident()
        {
            Resident resident = new(
                name: "Luke",
                lastName: "Skywalker",
                birthDate: new DateTime(1960, 5, 10),
                gender: Gender.Male,
                phone: "(11) 44245-9016",
                email: "luke.skywalker@gmail.com",
                cpf: "876.511.550-33"
            );

            Assert.Equal(Gender.Male, resident.Gender);
            Assert.Equal("Luke", resident.Name);
            Assert.Equal("(11) 44245-9016", resident.GetPhoneNumber());
            Assert.Equal("luke.skywalker@gmail.com", resident.GetEmailAddress());
            Assert.Equal("876.511.550-33", resident.GetCpfNumber());
        }

        [Fact]
        public void CreateResident_InvalidName_ShouldThrowException()
        {
            Assert.ThrowsAny<Exception>(() =>
            {
                new Resident(
                    name: null,
                    lastName: "Skywalker",
                    birthDate: new DateTime(1960, 5, 10),
                    gender: Gender.Male,
                    phone: "(11) 44245-9016",
                    email: "luke.skywalker@gmail.com",
                    cpf: "876.511.550-33"
                );
            });
        }

        [Fact]
        public void CreateResident_InvalidLastName_ShouldThrowException()
        {
            Assert.ThrowsAny<Exception>(() =>
            {
                new Resident(
                    name: "Luke",
                    lastName: string.Empty,
                    birthDate: new DateTime(1960, 5, 10),
                    gender: Gender.Male,
                    phone: "(11) 44245-9016",
                    email: "luke.skywalker@gmail.com",
                    cpf: "876.511.550-33"
                );
            });
        }

        [Theory]
        [InlineData("")]
        [InlineData("(44245-9016")]
        public void CreateResident_InvalidPhone_ShouldThrowException(string phoneNumber)
        {
            Assert.ThrowsAny<Exception>(() =>
            {
                new Resident(
                    name: "Luke",
                    lastName: "Skywalker",
                    birthDate: new DateTime(1960, 5, 10),
                    gender: Gender.Male,
                    phone: phoneNumber,
                    email: "luke.skywalker@gmail.com",
                    cpf: "876.511.550-33"
                );
            });
        }

        [Theory]
        [InlineData("")]
        [InlineData("luke.skywalker.com")]
        [InlineData("luke.skywalker@.com")]
        [InlineData("luke.skywalker@gmail")]
        public void CreateResident_InvalidEmail_ShouldThrowException(string email)
        {
            Assert.ThrowsAny<Exception>(() =>
            {
                new Resident(
                    name: "Luke",
                    lastName: "Skywalker",
                    birthDate: new DateTime(1960, 5, 10),
                    gender: Gender.Male,
                    phone: "(11) 44245-9016",
                    email: email,
                    cpf: "876.511.550-33"
                );
            });
        }

        [Theory]
        [InlineData("")]
        [InlineData("876.511.550-333")]
        [InlineData("876.511.550-3")]
        [InlineData("876.511.550-34")]
        public void CreateResident_InvalidCpf_ShouldThrowException(string cpf)
        {
            Assert.ThrowsAny<Exception>(() =>
            {
                new Resident(
                    name: "Luke",
                    lastName: "Skywalker",
                    birthDate: new DateTime(1960, 5, 10),
                    gender: Gender.Male,
                    phone: "(11) 44245-9016",
                    email: "luke.skywalker@gmail.com",
                    cpf: cpf
                );
            });
        }
    }
}