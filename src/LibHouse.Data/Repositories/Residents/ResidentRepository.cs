using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Residents.Preferences.Charges;
using LibHouse.Business.Entities.Residents.Preferences.General;
using LibHouse.Business.Entities.Residents.Preferences.Rooms;
using LibHouse.Business.Entities.Residents.Preferences.Services;
using LibHouse.Data.Context;
using LibHouse.Data.Repositories.Shared;
using System;
using System.Threading.Tasks;

namespace LibHouse.Data.Repositories.Residents
{
    public class ResidentRepository : EntityTypeRepository<Resident>, IResidentRepository
    {
        public ResidentRepository(LibHouseContext context)
            : base(context)
        {
        }

        public async Task<bool> AddOrUpdateResidentChargePreferencesAsync(Guid residentId, ChargePreferences chargePreferences)
        {
            int numberOfRowsAffected = await ExecuteStatementAsync($@"
                EXEC [Business].[sp_residentpreferences_addOrUpdateResidentChargePreferences]
                    {residentId},
                    {chargePreferences.ExpensePreferences.GetMinimumExpenseAmount()},
                    {chargePreferences.ExpensePreferences.GetMaximumExpenseAmount()},
                    {chargePreferences.RentPreferences.GetMinimumRentalAmount()},
                    {chargePreferences.RentPreferences.GetMaximumRentalAmount()}");
            return numberOfRowsAffected > 0;
        }

        public async Task<bool> AddOrUpdateResidentGeneralPreferencesAsync(Guid residentId, GeneralPreferences generalPreferences)
        {
            int numberOfRowsAffected = await ExecuteStatementAsync($@"
                EXEC [Business].[sp_residentpreferences_addOrUpdateResidentGeneralPreferences]
                    {residentId},
                    {generalPreferences.AnimalPreferences.WantSpaceForAnimals},
                    {generalPreferences.ChildrenPreferences.AcceptChildren},
                    {generalPreferences.PartyPreferences.WantsToParty},
                    {generalPreferences.RoommatePreferences.AcceptsOnlyFemaleRoommates},
                    {generalPreferences.RoommatePreferences.AcceptsOnlyMaleRoommates},
                    {generalPreferences.RoommatePreferences.GetMaximumNumberOfRoommatesDesired()},
                    {generalPreferences.RoommatePreferences.GetMinimumNumberOfRoommatesDesired()},
                    {generalPreferences.SmokersPreferences.AcceptSmokers}");
            return numberOfRowsAffected > 0;
        }

        public async Task<bool> AddOrUpdateResidentRoomPreferencesAsync(Guid residentId, RoomPreferences roomPreferences)
        {
            int numberOfRowsAffected = await ExecuteStatementAsync($@"
                EXEC [Business].[sp_residentpreferences_addOrUpdateResidentRoomPreferences]
                    {residentId},
                    '{roomPreferences.DormitoryPreferences.GetDormitoryType()}',
                    {roomPreferences.DormitoryPreferences.RequiresFurnishedDormitory()},
                    '{roomPreferences.BathroomPreferences.GetBathroomType()}',
                    {roomPreferences.GaragePreferences.RequiresGarage()},
                    {roomPreferences.KitchenPreferences.RequiresStove()},
                    {roomPreferences.KitchenPreferences.RequiresMicrowave()},
                    {roomPreferences.KitchenPreferences.RequiresRefrigerator()},
                    {roomPreferences.OtherRoomPreferences.RequiresServiceArea()},
                    {roomPreferences.OtherRoomPreferences.RequiresRecreationArea()}");
            return numberOfRowsAffected > 0;
        }

        public async Task<bool> AddOrUpdateResidentServicesPreferencesAsync(Guid residentId, ServicesPreferences servicesPreferences)
        {
            int numberOfRowsAffected = await ExecuteStatementAsync($@"
                IF NOT EXISTS
                (
                	SELECT TOP 1 ResidentId 
                	FROM [Business].[ResidentPreferences]
                	WHERE ResidentId = {residentId}
                )
                BEGIN
                    INSERT INTO [Business].[ResidentPreferences]
                    ([ResidentId]
                    ,[ServicesPreferences_Cleaning_HouseCleaningIsRequired]
                    ,[ServicesPreferences_Internet_InternetServiceIsRequired]
                    ,[ServicesPreferences_Television_CableTelevisionIsRequired])
                    VALUES
                    (
                        {residentId},
                        {servicesPreferences.CleaningPreferences.HouseCleaningIsRequired},
                        {servicesPreferences.InternetPreferences.InternetServiceIsRequired},
                        {servicesPreferences.TelevisionPreferences.CableTelevisionIsRequired}
                    )
                END
                ELSE
                BEGIN
                    UPDATE [Business].[ResidentPreferences]
                    SET [ServicesPreferences_Cleaning_HouseCleaningIsRequired] = {servicesPreferences.CleaningPreferences.HouseCleaningIsRequired}
                        ,[ServicesPreferences_Internet_InternetServiceIsRequired] = {servicesPreferences.InternetPreferences.InternetServiceIsRequired}
                        ,[ServicesPreferences_Television_CableTelevisionIsRequired] = {servicesPreferences.TelevisionPreferences.CableTelevisionIsRequired}
                    WHERE [ResidentId] = {residentId}
                END
            ");
            return numberOfRowsAffected > 0;
        }
    }
}