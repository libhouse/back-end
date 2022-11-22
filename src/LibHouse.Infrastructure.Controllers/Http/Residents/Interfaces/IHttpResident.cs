using LibHouse.Business.Application.Residents.Inputs;
using LibHouse.Business.Application.Residents.Outputs;
using System;
using System.Threading.Tasks;

namespace LibHouse.Infrastructure.Controllers.Http.Residents.Interfaces
{
    public interface IHttpResident
    {
        void OnResidentRoomPreferencesRegistration(Func<InputResidentRoomPreferencesRegistration, Task<OutputResidentRoomPreferencesRegistration>> on);
        void OnResidentServicesPreferencesRegistration(Func<InputResidentServicesPreferencesRegistration, Task<OutputResidentServicesPreferencesRegistration>> on);
        void OnResidentChargePreferencesRegistration(Func<InputResidentChargePreferencesRegistration, Task<OutputResidentChargePreferencesRegistration>> on);
    }
}