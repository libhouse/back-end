using LibHouse.Business.Application.Residents;
using LibHouse.Business.Application.Residents.Inputs;
using LibHouse.Business.Application.Residents.Outputs;
using LibHouse.Business.Entities.Localizations;
using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Residents.Preferences.Localizations;
using LibHouse.Business.Entities.Shared;
using LibHouse.Business.Entities.Users;
using LibHouse.Business.Notifiers;
using LibHouse.Data.Context;
using LibHouse.Data.Extensions.Context;
using LibHouse.Data.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LibHouse.IntegrationTests.Suite.Business.Application.Residents
{
    [Collection("Business.Application")]
    public class ResidentLocalizationPreferencesRegistrationTests
    {
        private readonly IConfiguration _testsConfiguration;

        public ResidentLocalizationPreferencesRegistrationTests()
        {
            _testsConfiguration = new ConfigurationBuilder().AddJsonFile("appsettings.Tests.json").Build();
        }

        [Fact]
        public async Task ExecuteAsync_NewLocalizationPreferences_ShouldRegisterLocalizationPreferences()
        {
            Notifier notifier = new();
            string connectionString = _testsConfiguration.GetSection("ConnectionStrings:LibHouseBusiness").Value;
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseSqlServer(connectionString).Options);
            await libHouseContext.CleanContextDataAsync();
            Resident resident = new("Fabio", "Piperno", new DateTime(1969, 7, 15), Gender.Male, "11995267991", "fabiopiperno@gmail.com", "79847728003");
            await libHouseContext.Residents.AddAsync(resident);
            await libHouseContext.SaveChangesAsync();
            IUnitOfWork unitOfWork = new UnitOfWork(libHouseContext);
            ResidentLocalizationPreferencesRegistration residentLocalizationPreferencesRegistration = new(notifier, unitOfWork);
            InputResidentLocalizationPreferencesRegistration input = new()
            {
                ResidentId = resident.Id,
                AddressDescription = "Rua São Bento",
                AddressComplement = "de 321 ao fim - lado ímpar",
                AddressNumber = 321,
                AddressNeighborhood = "Centro",
                AddressCity = "São Paulo",
                AddressFederativeUnit = "SP",
                AddressPostalCodeNumber = "01011100"
            };
            OutputResidentLocalizationPreferencesRegistration output = await residentLocalizationPreferencesRegistration.ExecuteAsync(input);
            Assert.True(output.IsSuccess);
        }

        [Fact]
        public async Task ExecuteAsync_ResidentWithLocalizationPreferencesAlreadyRegistered_ShouldNotRegisterLocalizationPreferences()
        {
            Notifier notifier = new();
            string connectionString = _testsConfiguration.GetSection("ConnectionStrings:LibHouseBusiness").Value;
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseSqlServer(connectionString).Options);
            await libHouseContext.CleanContextDataAsync();
            Resident resident = new("Fabio", "Piperno", new DateTime(1969, 7, 15), Gender.Male, "11995267991", "fabiopiperno@gmail.com", "79847728003");
            resident.WithPreferences();
            LocalizationPreferences localizationPreferences = new();
            string landmarkStreet = "Rua São Bento";
            string landmarkComplement = "de 321 ao fim - lado ímpar";
            ushort landmarkNumber = 321;
            string landmarkNeighborhood = "Centro";
            string landmarkCity = "São Paulo";
            string landmarkFederativeUnit = "SP";
            string landmarkPostalCodeNumber = "01011100";
            Address landmarkAddress = new(landmarkStreet, landmarkNumber, landmarkNeighborhood, landmarkCity, landmarkFederativeUnit, landmarkPostalCodeNumber, landmarkComplement);
            localizationPreferences.AddLandmarkPreferences(landmarkAddress);
            resident.AddLocalizationPreferences(localizationPreferences);
            await libHouseContext.Residents.AddAsync(resident);
            await libHouseContext.SaveChangesAsync();
            IUnitOfWork unitOfWork = new UnitOfWork(libHouseContext);
            ResidentLocalizationPreferencesRegistration residentLocalizationPreferencesRegistration = new(notifier, unitOfWork);
            InputResidentLocalizationPreferencesRegistration input = new()
            {
                ResidentId = resident.Id,
                AddressDescription = "Rua São Bento",
                AddressComplement = "de 321 ao fim - lado ímpar",
                AddressNumber = 321,
                AddressNeighborhood = "Centro",
                AddressCity = "São Paulo",
                AddressFederativeUnit = "SP",
                AddressPostalCodeNumber = "01011100"
            };
            OutputResidentLocalizationPreferencesRegistration output = await residentLocalizationPreferencesRegistration.ExecuteAsync(input);
            Assert.False(output.IsSuccess);
        }
    }
}