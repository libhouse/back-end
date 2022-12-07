using LibHouse.Business.Application.Residents.Interfaces;
using LibHouse.Infrastructure.Controllers.Http.Residents.Interfaces;

namespace LibHouse.Infrastructure.Controllers.Http.Residents
{
    public class ResidentsController
    {
        public ResidentsController(
            IHttpResident residentsWebApiAdapter,
            IResidentRoomPreferencesRegistration residentRoomPreferencesRegistration,
            IResidentServicesPreferencesRegistration residentServicesPreferencesRegistration,
            IResidentChargePreferencesRegistration residentChargePreferencesRegistration,
            IResidentGeneralPreferencesRegistration residentGeneralPreferencesRegistration)
        {
            residentsWebApiAdapter.OnResidentRoomPreferencesRegistration(
                async (input) =>
                {
                    return await residentRoomPreferencesRegistration.ExecuteAsync(input);
                }
            );
            residentsWebApiAdapter.OnResidentServicesPreferencesRegistration(
                async (input) =>
                {
                    return await residentServicesPreferencesRegistration.ExecuteAsync(input);
                }
            );
            residentsWebApiAdapter.OnResidentChargePreferencesRegistration(
                async (input) =>
                {
                    return await residentChargePreferencesRegistration.ExecuteAsync(input);
                }
            );
            residentsWebApiAdapter.OnResidentGeneralPreferencesRegistration(
                async (input) =>
                {
                    return await residentGeneralPreferencesRegistration.ExecuteAsync(input);
                }
            );
        }

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

        public ResidentsController(
            IHttpResident residentsWebApiAdapter, 
            IResidentChargePreferencesRegistration residentChargePreferencesRegistration)
        {
            residentsWebApiAdapter.OnResidentChargePreferencesRegistration(
                async (input) =>
                {
                    return await residentChargePreferencesRegistration.ExecuteAsync(input);
                }
            );
        }

        public ResidentsController(
            IHttpResident residentsWebApiAdapter,
            IResidentGeneralPreferencesRegistration residentGeneralPreferencesRegistration)
        {
            residentsWebApiAdapter.OnResidentGeneralPreferencesRegistration(
                async (input) =>
                {
                    return await residentGeneralPreferencesRegistration.ExecuteAsync(input);
                }
            );
        }

        public ResidentsController(
            IHttpResident residentsWebApiAdapter, 
            IResidentLocalizationPreferencesRegistration residentLocalizationPreferencesRegistration)
        {
            residentsWebApiAdapter.OnResidentLocalizationPreferencesRegistration(
                async (input) =>
                {
                    return await residentLocalizationPreferencesRegistration.ExecuteAsync(input);
                }
            );
        }
    }
}