using FluentValidation.Results;
using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Users;
using LibHouse.Business.Validations.Users;
using LibHouse.Data.Context;
using LibHouse.Data.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace LibHouse.UnitTests.Suite.Business.Validations.Users
{
    public class UserRegistrationValidatorTests
    {
        [Fact]
        public void Validate_NewUserRegistration_ShouldReturnTrue()
        {
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseInMemoryDatabase("InMemoryLibHouse").Options);
            IUserRepository userRepository = new UserRepository(libHouseContext);
            UserRegistrationValidator validator = new(userRepository);
            Resident user = new("Fábio", "Santos", new DateTime(1980, 2, 25), Gender.Male, "(11) 78555-8132", "fabio.santos@gmail.com", "072.806.770-61");
            ValidationResult validationResult = validator.Validate(user);
            Assert.True(validationResult.IsValid);
        }

        [Fact]
        public void Validate_RepeatedUserRegistration_ShouldReturnFalse()
        {
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseInMemoryDatabase("InMemoryLibHouse").Options);
            IUserRepository userRepository = new UserRepository(libHouseContext);
            UserRegistrationValidator validator = new(userRepository);
            Resident user = new("Renato", "Augusto", new DateTime(1984, 3, 14), Gender.Male, "(15) 99521-7180", "renato.augusto@gmail.com", "534.856.010-39");
            user.Activate();
            libHouseContext.Users.Add(user);
            libHouseContext.SaveChanges();
            ValidationResult validationResult = validator.Validate(user);
            Assert.False(validationResult.IsValid);
        }
    }
}