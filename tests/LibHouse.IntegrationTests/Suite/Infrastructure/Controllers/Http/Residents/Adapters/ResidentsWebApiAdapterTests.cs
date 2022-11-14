using LibHouse.Business.Application.Residents;
using LibHouse.Business.Entities.Residents;
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
    }
}