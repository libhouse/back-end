using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Residents.Preferences.Rooms;
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

        public async Task<bool> AddResidentRoomPreferencesAsync(Guid residentId, RoomPreferences roomPreferences)
        {
            int numberOfRowsAffected = await ExecuteStatementAsync($@"
                INSERT INTO [Business].[ResidentPreferences]
                VALUES
                (
                    {residentId},
                    '{roomPreferences.DormitoryPreferences.DormitoryType}',
                    {roomPreferences.DormitoryPreferences.RequireFurnishedDormitory},
                    '{roomPreferences.BathroomPreferences.BathroomType}',
                    {roomPreferences.GaragePreferences.GarageIsRequired},
                    {roomPreferences.KitchenPreferences.StoveIsRequired},
                    {roomPreferences.KitchenPreferences.MicrowaveIsRequired},
                    {roomPreferences.KitchenPreferences.RefrigeratorIsRequired},
                    {roomPreferences.OtherRoomPreferences.ServiceAreaIsRequired},
                    {roomPreferences.OtherRoomPreferences.RecreationAreaIsRequired}
                )
            ");
            return numberOfRowsAffected > 0;
        }
    }
}