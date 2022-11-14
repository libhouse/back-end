using LibHouse.Business.Entities.Residents.Preferences.Rooms;
using LibHouse.Business.Entities.Shared;
using System;
using System.Threading.Tasks;

namespace LibHouse.Business.Entities.Residents
{
    public interface IResidentRepository : IEntityRepository<Resident>
    {
        Task<bool> AddResidentRoomPreferencesAsync(Guid residentId, RoomPreferences roomPreferences);
    }
}