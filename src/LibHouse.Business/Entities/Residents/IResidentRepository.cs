using LibHouse.Business.Entities.Residents.Preferences.Charges;
using LibHouse.Business.Entities.Residents.Preferences.General;
using LibHouse.Business.Entities.Residents.Preferences.Rooms;
using LibHouse.Business.Entities.Residents.Preferences.Services;
using LibHouse.Business.Entities.Shared;
using System;
using System.Threading.Tasks;

namespace LibHouse.Business.Entities.Residents
{
    public interface IResidentRepository : IEntityRepository<Resident>
    {
        Task<bool> AddOrUpdateResidentRoomPreferencesAsync(Guid residentId, RoomPreferences roomPreferences);
        Task<bool> AddOrUpdateResidentServicesPreferencesAsync(Guid residentId, ServicesPreferences servicesPreferences);
        Task<bool> AddOrUpdateResidentChargePreferencesAsync(Guid residentId, ChargePreferences chargePreferences);
        Task<bool> AddOrUpdateResidentGeneralPreferencesAsync(Guid residentId, GeneralPreferences generalPreferences);
    }
}