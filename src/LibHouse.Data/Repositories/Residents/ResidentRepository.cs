using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Residents.Preferences.Charges;
using LibHouse.Business.Entities.Residents.Preferences.General;
using LibHouse.Business.Entities.Residents.Preferences.Localizations;
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

        public async Task<bool> AddOrUpdateResidentLocalizationPreferencesAsync(Guid residentId, LocalizationPreferences localizationPreferences)
        {
            int numberOfRowsAffected = await ExecuteStatementAsync($@"
                EXEC [Business].[sp_residentpreferences_addOrUpdateResidentLocalizationPreferences]
                    {residentId},
                    {localizationPreferences.LandmarkPreferences.LandmarkAddressId}");
            return numberOfRowsAffected > 0;
        }

        public async Task<bool> AddOrUpdateResidentRoomPreferencesAsync(Guid residentId, RoomPreferences roomPreferences)
        {
            int numberOfRowsAffected = await ExecuteStatementAsync($@"
                EXEC [Business].[sp_residentpreferences_addOrUpdateResidentRoomPreferences]
                    {residentId},
                    {roomPreferences.DormitoryPreferences.GetDormitoryType()},
                    {roomPreferences.DormitoryPreferences.RequiresFurnishedDormitory()},
                    {roomPreferences.BathroomPreferences.GetBathroomType()},
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
                EXEC [Business].[sp_residentpreferences_addOrUpdateResidentServicesPreferences]
                    {residentId},
                    {servicesPreferences.CleaningPreferences.RequiresHouseCleaningService()},
                    {servicesPreferences.InternetPreferences.RequiresInternetService()},
                    {servicesPreferences.TelevisionPreferences.RequiresCableTelevisionService()}");
            return numberOfRowsAffected > 0;
        }
    }
}