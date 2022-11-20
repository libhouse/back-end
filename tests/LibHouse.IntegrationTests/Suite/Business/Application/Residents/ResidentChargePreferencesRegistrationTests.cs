using LibHouse.Business.Application.Residents;
using LibHouse.Business.Application.Residents.Inputs;
using LibHouse.Business.Application.Residents.Outputs;
using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Residents.Preferences.Charges;
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
    public class ResidentChargePreferencesRegistrationTests
    {
        private readonly IConfiguration _testsConfiguration;

        public ResidentChargePreferencesRegistrationTests()
        {
            _testsConfiguration = new ConfigurationBuilder().AddJsonFile("appsettings.Tests.json").Build();
        }

        [Fact]
        public async Task ExecuteAsync_NewChargePreferences_ShouldRegisterChargePreferences()
        {
            Notifier notifier = new();
            string connectionString = _testsConfiguration.GetSection("ConnectionStrings:LibHouseBusiness").Value;
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseSqlServer(connectionString).Options);
            await libHouseContext.CleanContextDataAsync();
            IResidentRepository residentRepository = new ResidentRepository(libHouseContext);
            ResidentChargePreferencesRegistration residentChargePreferencesRegistration = new(notifier, residentRepository);
            Resident resident = new("Adenor", "Junior", new DateTime(1987, 9, 25), Gender.Male, "11995267991", "adenorjunior@gmail.com", "23987691000");
            await libHouseContext.Residents.AddAsync(resident);
            await libHouseContext.SaveChangesAsync();
            InputResidentChargePreferencesRegistration input = new()
            {
                ResidentId = resident.Id,
                MinimumRentalAmountDesired = 100.0m,
                MaximumRentalAmountDesired = 400.0m,
                MinimumExpenseAmountDesired = 50.0m,
                MaximumExpenseAmountDesired = 150.0m
            };
            OutputResidentChargePreferencesRegistration output = await residentChargePreferencesRegistration.ExecuteAsync(input);
            Assert.True(output.IsSuccess);
        }

        [Fact]
        public async Task ExecuteAsync_ResidentWithChargePreferencesAlreadyRegistered_ShouldNotRegisterChargePreferences()
        {
            Notifier notifier = new();
            string connectionString = _testsConfiguration.GetSection("ConnectionStrings:LibHouseBusiness").Value;
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseSqlServer(connectionString).Options);
            await libHouseContext.CleanContextDataAsync();
            IResidentRepository residentRepository = new ResidentRepository(libHouseContext);
            ResidentChargePreferencesRegistration residentChargePreferencesRegistration = new(notifier, residentRepository);
            Resident resident = new("Livia", "Antunes", new DateTime(1990, 5, 20), Gender.Female, "11995267991", "livia.antunes@gmail.com", "44005152066");
            resident.WithPreferences();
            ChargePreferences chargePreferences = new();
            chargePreferences.AddExpensePreferences(100.0m, 500.0m);
            chargePreferences.AddRentPreferences(45.0m, 135.0m);
            resident.AddChargePreferences(chargePreferences);
            await libHouseContext.Residents.AddAsync(resident);
            await libHouseContext.SaveChangesAsync();
            InputResidentChargePreferencesRegistration input = new()
            {
                ResidentId = resident.Id,
                MinimumRentalAmountDesired = 100.0m,
                MaximumRentalAmountDesired = 400.0m,
                MinimumExpenseAmountDesired = 50.0m,
                MaximumExpenseAmountDesired = 150.0m
            };
            OutputResidentChargePreferencesRegistration output = await residentChargePreferencesRegistration.ExecuteAsync(input);
            Assert.False(output.IsSuccess);
        }
    }
}