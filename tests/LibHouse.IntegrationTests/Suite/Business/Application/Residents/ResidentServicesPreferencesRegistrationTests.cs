using LibHouse.Business.Application.Residents;
using LibHouse.Business.Application.Residents.Inputs;
using LibHouse.Business.Application.Residents.Outputs;
using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Residents.Preferences.Services;
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
    public class ResidentServicesPreferencesRegistrationTests
    {
        private readonly IConfiguration _testsConfiguration;

        public ResidentServicesPreferencesRegistrationTests()
        {
            _testsConfiguration = new ConfigurationBuilder().AddJsonFile("appsettings.Tests.json").Build();
        }

        [Fact]
        public async Task ExecuteAsync_NewServicesPreferences_ShouldRegisterServicesPreferences()
        {
            Notifier notifier = new();
            string connectionString = _testsConfiguration.GetSection("ConnectionStrings:LibHouseBusiness").Value;
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseSqlServer(connectionString).Options);
            await libHouseContext.CleanContextDataAsync();
            IResidentRepository residentRepository = new ResidentRepository(libHouseContext);
            ResidentServicesPreferencesRegistration residentServicesPreferencesRegistration = new(notifier, residentRepository);
            Resident resident = new("Thiago", "Lira", new DateTime(1980, 8, 22), Gender.Male, "11995267991", "thiago.lira@gmail.com", "67730254000");
            await libHouseContext.Residents.AddAsync(resident);
            await libHouseContext.SaveChangesAsync();
            InputResidentServicesPreferencesRegistration input = new()
            {
                ResidentId = resident.Id,
                WantHouseCleaningService = true,
                WantInternetService = true,
                WantCableTelevisionService = false
            };
            OutputResidentServicesPreferencesRegistration output = await residentServicesPreferencesRegistration.ExecuteAsync(input);
            Assert.True(output.IsSuccess);
        }

        [Fact]
        public async Task ExecuteAsync_ResidentWithServicesPreferencesAlreadyRegistered_ShouldNotRegisterServicesPreferences()
        {
            Notifier notifier = new();
            string connectionString = _testsConfiguration.GetSection("ConnectionStrings:LibHouseBusiness").Value;
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseSqlServer(connectionString).Options);
            await libHouseContext.CleanContextDataAsync();
            IResidentRepository residentRepository = new ResidentRepository(libHouseContext);
            ResidentServicesPreferencesRegistration residentServicesPreferencesRegistration = new(notifier, residentRepository);
            Resident resident = new("Peter", "Mendes", new DateTime(1986, 5, 4), Gender.Male, "11995267991", "petermendes@gmail.com", "14107161048");
            ServicesPreferences servicesPreferences = new();
            servicesPreferences.AddCleaningPreferences(houseCleaningIsRequired: true);
            servicesPreferences.AddInternetPreferences(internetServiceIsRequired: false);
            servicesPreferences.AddTelevisionPreferences(cableTelevisionIsRequired: false);
            resident.WithPreferences();
            resident.AddServicesPreferences(servicesPreferences);
            await libHouseContext.Residents.AddAsync(resident);
            await libHouseContext.SaveChangesAsync();
            InputResidentServicesPreferencesRegistration input = new()
            {
                ResidentId = resident.Id,
                WantHouseCleaningService = true,
                WantInternetService = true,
                WantCableTelevisionService = false
            };
            OutputResidentServicesPreferencesRegistration output = await residentServicesPreferencesRegistration.ExecuteAsync(input);
            Assert.False(output.IsSuccess);
        }
    }
}