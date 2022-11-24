﻿using LibHouse.Business.Application.Residents;
using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Residents.Preferences.General.Builders;
using LibHouse.Business.Entities.Residents.Preferences.Rooms.Builders;
using LibHouse.Business.Entities.Users;
using LibHouse.Business.Monads;
using LibHouse.Business.Notifiers;
using LibHouse.Data.Context;
using LibHouse.Data.Extensions.Context;
using LibHouse.Data.Repositories.Residents;
using LibHouse.Infrastructure.Controllers.Http.Residents;
using LibHouse.Infrastructure.Controllers.Http.Residents.Adapters;
using LibHouse.Infrastructure.Controllers.ViewModels.Residents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LibHouse.IntegrationTests.Suite.Infrastructure.Controllers.Http.Residents.Adapters
{
    [Collection("Infrastructure.Controllers")]
    public class ResidentsWebApiAdapterTests
    {
        private readonly IConfiguration _testsConfiguration;

        public ResidentsWebApiAdapterTests()
        {
            _testsConfiguration = new ConfigurationBuilder().AddJsonFile("appsettings.Tests.json").Build();
        }

        [Fact]
        public async Task ResidentRoomPreferencesRegistration_NewRoomPreferences_ShouldBeSuccess()
        {
            Notifier notifier = new();
            string connectionString = _testsConfiguration.GetSection("ConnectionStrings:LibHouseBusiness").Value;
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseSqlServer(connectionString).Options);
            await libHouseContext.CleanContextDataAsync();
            IResidentRepository residentRepository = new ResidentRepository(libHouseContext);
            Resident resident = new("Anderson", "Pena", new DateTime(1978, 2, 15), Gender.Male, "11985261522", "anderson.pena@gmail.com", "32890623092");
            await libHouseContext.Residents.AddAsync(resident);
            await libHouseContext.SaveChangesAsync();
            IRoomPreferencesBuilder roomPreferencesBuilder = new RoomPreferencesBuilder();
            ResidentRoomPreferencesRegistration residentRoomPreferencesRegistration = new(notifier, residentRepository, roomPreferencesBuilder);
            ResidentsWebApiAdapter residentsWebApiAdapter = new();
            _ = new ResidentsController(residentsWebApiAdapter, residentRoomPreferencesRegistration);
            ResidentRoomPreferencesRegistrationViewModel viewModel = new()
            {
                ResidentId = resident.Id,
                DormitoryPreferences = new()
                {
                    WantSingleDormitory = true,
                    WantFurnishedDormitory = true
                },
                KitchenPreferences = new()
                {
                    WantStove = true,
                    WantMicrowave = false,
                    WantRefrigerator = false
                },
                GaragePreferences = new()
                {
                    WantGarage = true
                },
                BathroomPreferences = new()
                {
                    WantPrivateBathroom = true
                },
                OtherPreferences = new()
                {
                    WantServiceArea = true,
                    WantRecreationArea = true
                }
            };
            Result residentRoomPreferencesRegistrationResult = await residentsWebApiAdapter.ResidentRoomPreferencesRegistration(viewModel);
            Assert.True(residentRoomPreferencesRegistrationResult.IsSuccess);
        }

        [Fact]
        public async Task ResidentServicesPreferencesRegistration_NewServicesPreferences_ShouldBeSuccess()
        {
            Notifier notifier = new();
            string connectionString = _testsConfiguration.GetSection("ConnectionStrings:LibHouseBusiness").Value;
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseSqlServer(connectionString).Options);
            await libHouseContext.CleanContextDataAsync();
            IResidentRepository residentRepository = new ResidentRepository(libHouseContext);
            Resident resident = new("Gustavo", "Rodriguez", new DateTime(1967, 1, 20), Gender.Male, "11985261522", "gustavorodriguez@gmail.com", "72047778085");
            await libHouseContext.Residents.AddAsync(resident);
            await libHouseContext.SaveChangesAsync();
            ResidentServicesPreferencesRegistration residentServicesPreferencesRegistration = new(notifier, residentRepository);
            ResidentsWebApiAdapter residentsWebApiAdapter = new();
            _ = new ResidentsController(residentsWebApiAdapter, residentServicesPreferencesRegistration);
            ResidentServicesPreferencesRegistrationViewModel viewModel = new()
            {
                ResidentId = resident.Id,
                WantHouseCleaningService = true,
                WantInternetService = true,
                WantCableTelevisionService = true
            };
            Result residentServicesPreferencesRegistrationResult = await residentsWebApiAdapter.ResidentServicesPreferencesRegistration(viewModel);
            Assert.True(residentServicesPreferencesRegistrationResult.IsSuccess);
        }

        [Fact]
        public async Task ResidentChargePreferencesRegistration_NewChargePreferences_ShouldBeSuccess()
        {
            Notifier notifier = new();
            string connectionString = _testsConfiguration.GetSection("ConnectionStrings:LibHouseBusiness").Value;
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseSqlServer(connectionString).Options);
            await libHouseContext.CleanContextDataAsync();
            IResidentRepository residentRepository = new ResidentRepository(libHouseContext);
            Resident resident = new("Juan", "Batista", new DateTime(1971, 10, 10), Gender.Male, "11985261522", "juanbatista@gmail.com", "76912370063");
            await libHouseContext.Residents.AddAsync(resident);
            await libHouseContext.SaveChangesAsync();
            ResidentChargePreferencesRegistration residentChargePreferencesRegistration = new(notifier, residentRepository);
            ResidentsWebApiAdapter residentsWebApiAdapter = new();
            _ = new ResidentsController(residentsWebApiAdapter, residentChargePreferencesRegistration);
            ResidentChargePreferencesRegistrationViewModel viewModel = new()
            {
                ResidentId = resident.Id,
                MinimumRentalAmountDesired = 100.0m,
                MaximumRentalAmountDesired = 400.0m,
                MinimumExpenseAmountDesired = 50.0m,
                MaximumExpenseAmountDesired = 150.0m
            };
            Result residentChargePreferencesRegistrationResult = await residentsWebApiAdapter.ResidentChargePreferencesRegistration(viewModel);
            Assert.True(residentChargePreferencesRegistrationResult.IsSuccess);
        }

        [Fact]
        public async Task ResidentGeneralPreferencesRegistration_NewGeneralPreferences_ShouldBeSuccess()
        {
            Notifier notifier = new();
            string connectionString = _testsConfiguration.GetSection("ConnectionStrings:LibHouseBusiness").Value;
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseSqlServer(connectionString).Options);
            await libHouseContext.CleanContextDataAsync();
            IGeneralPreferencesBuilder generalPreferencesBuilder = new GeneralPreferencesBuilder();
            IResidentRepository residentRepository = new ResidentRepository(libHouseContext);
            Resident resident = new("Cléber", "Dias", new DateTime(1975, 11, 11), Gender.Male, "11985261522", "cleberdias@gmail.com", "21571050000");
            await libHouseContext.Residents.AddAsync(resident);
            await libHouseContext.SaveChangesAsync();
            ResidentGeneralPreferencesRegistration residentGeneralPreferencesRegistration = new(notifier, residentRepository, generalPreferencesBuilder);
            ResidentsWebApiAdapter residentsWebApiAdapter = new();
            _ = new ResidentsController(residentsWebApiAdapter, residentGeneralPreferencesRegistration);
            ResidentGeneralPreferencesRegistrationViewModel viewModel = new()
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
            Result residentGeneralPreferencesRegistrationResult = await residentsWebApiAdapter.ResidentGeneralPreferencesRegistration(viewModel);
            Assert.True(residentGeneralPreferencesRegistrationResult.IsSuccess);
        }
    }
}