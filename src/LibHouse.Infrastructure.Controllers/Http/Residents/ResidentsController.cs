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
    }
}