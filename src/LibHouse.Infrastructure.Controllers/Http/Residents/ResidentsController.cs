using LibHouse.Business.Application.Residents.Interfaces;
using LibHouse.Infrastructure.Controllers.Http.Residents.Interfaces;

namespace LibHouse.Infrastructure.Controllers.Http.Residents
{
    public class ResidentsController
    {
        public ResidentsController(
            IHttpResident residentsWebApiAdapter, 
            IResidentRoomPreferencesRegistration residentRoomPreferencesRegistration)
        {
            residentsWebApiAdapter.OnResidentRoomPreferencesRegistration(
                async (input) =>
                {
                    return await residentRoomPreferencesRegistration.ExecuteAsync(input);
                }
            );
        }

        public ResidentsController(
            IHttpResident residentsWebApiAdapter,
            IResidentServicesPreferencesRegistration residentServicesPreferencesRegistration)
        {
            residentsWebApiAdapter.OnResidentServicesPreferencesRegistration(
                async (input) =>
                {
                    return await residentServicesPreferencesRegistration.ExecuteAsync(input);
                }
            );
        }
    }
}