using LibHouse.Business.Application.Residents;
using LibHouse.Business.Application.Residents.Inputs;
using LibHouse.Business.Application.Residents.Outputs;
using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Residents.Preferences.Rooms;
using LibHouse.Business.Entities.Residents.Preferences.Rooms.Builders;
using LibHouse.Business.Entities.Users;
using LibHouse.Business.Notifiers;
using LibHouse.Data.Context;
using LibHouse.Data.Extensions.Context;
using LibHouse.Data.Repositories.Residents;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LibHouse.IntegrationTests.Suite.Business.Application.Residents
{
    public class ResidentRoomPreferencesRegistrationTests
    {
        [Fact]
        public async Task ExecuteAsync_NewRoomPreferences_ShouldRegisterRoomPreferences()
        {
            Notifier notifier = new();
            string connectionString = "Data source=(localdb)\\mssqllocaldb;Initial Catalog=LibHouse;Integrated Security=true;pooling=true;MultipleActiveResultSets=true";
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseSqlServer(connectionString).Options);
            await libHouseContext.CleanContextDataAsync();
            IResidentRepository residentRepository = new ResidentRepository(libHouseContext);
            IRoomPreferencesBuilder roomPreferencesBuilder = new RoomPreferencesBuilder();
            ResidentRoomPreferencesRegistration residentRoomPreferencesRegistration = new(notifier, residentRepository, roomPreferencesBuilder);
            Resident resident = new("Thomas", "Morgado", new DateTime(1990, 10, 25), Gender.Male, "(11) 98526-7981", "thomas.morgado@gmail.com", "06432684056");
            await libHouseContext.Residents.AddAsync(resident);
            await libHouseContext.SaveChangesAsync();
            InputResidentRoomPreferencesRegistration input = new()
            {
                ResidentId = resident.Id,
                WantSingleDormitory = true,
                WantFurnishedDormitory = true,
                WantStove = false,
                WantMicrowave = true,
                WantRefrigerator = true,
                WantGarage = false,
                WantPrivateBathroom = true,
                WantServiceArea = true,
                WantRecreationArea = false
            };
            OutputResidentRoomPreferencesRegistration output = await residentRoomPreferencesRegistration.ExecuteAsync(input);
            Assert.True(output.IsSuccess);
        }

        [Fact]
        public async Task ExecuteAsync_ResidentWithRoomPreferencesAlreadyRegistered_ShouldNotRegisterRoomPreferences()
        {
            Notifier notifier = new();
            string connectionString = "Data source=(localdb)\\mssqllocaldb;Initial Catalog=LibHouse;Integrated Security=true;pooling=true;MultipleActiveResultSets=true";
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseSqlServer(connectionString).Options);
            await libHouseContext.CleanContextDataAsync();
            IResidentRepository residentRepository = new ResidentRepository(libHouseContext);
            IRoomPreferencesBuilder roomPreferencesBuilder = new RoomPreferencesBuilder();
            ResidentRoomPreferencesRegistration residentRoomPreferencesRegistration = new(notifier, residentRepository, roomPreferencesBuilder);
            Resident resident = new("James", "Luna", new DateTime(1985, 11, 21), Gender.Male, "(11) 98526-7981", "james.luna@gmail.com", "64720833047");
            RoomPreferences roomPreferences = new();
            roomPreferences.AddBathroomPreferences(BathroomType.Shared);
            roomPreferences.AddDormitoryPreferences(DormitoryType.Shared, requireFurnishedDormitory: true);
            roomPreferences.AddGaragePreferences(garageIsRequired: false);
            roomPreferences.AddKitchenPreferences(stoveIsRequired: true, microwaveIsRequired: false, refrigeratorIsRequired: true);
            roomPreferences.AddOtherRoomPreferences(serviceAreaIsRequired: false, recreationAreaIsRequired: false);
            resident.WithPreferences();
            resident.AddRoomPreferences(roomPreferences);
            await libHouseContext.Residents.AddAsync(resident);
            await libHouseContext.SaveChangesAsync();
            InputResidentRoomPreferencesRegistration input = new()
            {
                ResidentId = resident.Id,
                WantSingleDormitory = true,
                WantFurnishedDormitory = true,
                WantStove = false,
                WantMicrowave = true,
                WantRefrigerator = true,
                WantGarage = false,
                WantPrivateBathroom = true,
                WantServiceArea = true,
                WantRecreationArea = false
            };
            OutputResidentRoomPreferencesRegistration output = await residentRoomPreferencesRegistration.ExecuteAsync(input);
            Assert.False(output.IsSuccess);
        }
    }
}