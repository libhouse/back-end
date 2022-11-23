using LibHouse.Business.Application.Residents;
using LibHouse.Business.Application.Residents.Inputs;
using LibHouse.Business.Application.Residents.Outputs;
using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Residents.Preferences.General;
using LibHouse.Business.Entities.Residents.Preferences.General.Builders;
using LibHouse.Business.Entities.Users;
using LibHouse.Business.Notifiers;
using LibHouse.Data.Context;
using LibHouse.Data.Extensions.Context;
using LibHouse.Data.Repositories.Residents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LibHouse.IntegrationTests.Suite.Business.Application.Residents
{
    [Collection("Business.Application")]
    public class ResidentGeneralPreferencesRegistrationTests
    {
        private readonly IConfiguration _testsConfiguration;

        public ResidentGeneralPreferencesRegistrationTests()
        {
            _testsConfiguration = new ConfigurationBuilder().AddJsonFile("appsettings.Tests.json").Build();
        }

        [Fact]
        public async Task ExecuteAsync_NewGeneralPreferences_ShouldRegisterGeneralPreferences()
        {
            Notifier notifier = new();
            string connectionString = _testsConfiguration.GetSection("ConnectionStrings:LibHouseBusiness").Value;
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseSqlServer(connectionString).Options);
            await libHouseContext.CleanContextDataAsync();
            IResidentRepository residentRepository = new ResidentRepository(libHouseContext);
            IGeneralPreferencesBuilder generalPreferencesBuilder = new GeneralPreferencesBuilder();
            ResidentGeneralPreferencesRegistration residentGeneralPreferencesRegistration = new(notifier, residentRepository, generalPreferencesBuilder);
            Resident resident = new("Dante", "Barros", new DateTime(1992, 1, 20), Gender.Male, "11995267991", "dantebarros@gmail.com", "57506418053");
            await libHouseContext.Residents.AddAsync(resident);
            await libHouseContext.SaveChangesAsync();
            InputResidentGeneralPreferencesRegistration input = new()
            {
                ResidentId = resident.Id,
                WantSpaceForAnimals = true,
                AcceptChildren = true,
                WantsToParty = false,
                AcceptSmokers = false,
                AcceptsOnlyMenAsRoommates = false,
                AcceptsOnlyWomenAsRoommates = false,
                MinimumNumberOfRoommatesDesired = 1,
                MaximumNumberOfRoommatesDesired = 3
            };
            OutputResidentGeneralPreferencesRegistration output = await residentGeneralPreferencesRegistration.ExecuteAsync(input);
            Assert.True(output.IsSuccess);
        }

        [Fact]
        public async Task ExecuteAsync_ResidentWithGeneralPreferencesAlreadyRegistered_ShouldNotRegisterGeneralPreferences()
        {
            Notifier notifier = new();
            string connectionString = _testsConfiguration.GetSection("ConnectionStrings:LibHouseBusiness").Value;
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseSqlServer(connectionString).Options);
            await libHouseContext.CleanContextDataAsync();
            IResidentRepository residentRepository = new ResidentRepository(libHouseContext);
            IGeneralPreferencesBuilder generalPreferencesBuilder = new GeneralPreferencesBuilder();
            ResidentGeneralPreferencesRegistration residentGeneralPreferencesRegistration = new(notifier, residentRepository, generalPreferencesBuilder);
            Resident resident = new("Dante", "Barros", new DateTime(1992, 1, 20), Gender.Male, "11995267991", "dantebarros@gmail.com", "57506418053");
            resident.WithPreferences();
            GeneralPreferences generalPreferences = new();
            generalPreferences.AddAnimalPreferences(wantSpaceForAnimals: true);
            generalPreferences.AddChildrenPreferences(acceptChildren: true);
            generalPreferences.AddPartyPreferences(wantsToParty: true);
            generalPreferences.AddRoommatePreferences(1, 4, new[] { Gender.Male });
            generalPreferences.AddSmokersPreferences(acceptSmokers: true);
            resident.AddGeneralPreferences(generalPreferences);
            await libHouseContext.Residents.AddAsync(resident);
            await libHouseContext.SaveChangesAsync();
            InputResidentGeneralPreferencesRegistration input = new()
            {
                ResidentId = resident.Id,
                WantSpaceForAnimals = true,
                AcceptChildren = true,
                WantsToParty = false,
                AcceptSmokers = false,
                AcceptsOnlyMenAsRoommates = false,
                AcceptsOnlyWomenAsRoommates = false,
                MinimumNumberOfRoommatesDesired = 1,
                MaximumNumberOfRoommatesDesired = 3
            };
            OutputResidentGeneralPreferencesRegistration output = await residentGeneralPreferencesRegistration.ExecuteAsync(input);
            Assert.False(output.IsSuccess);
        }
    }
}