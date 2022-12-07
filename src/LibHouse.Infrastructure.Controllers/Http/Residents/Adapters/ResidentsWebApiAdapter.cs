using LibHouse.Business.Application.Residents.Inputs;
using LibHouse.Business.Application.Residents.Outputs;
using LibHouse.Business.Monads;
using LibHouse.Infrastructure.Controllers.Http.Residents.Converters;
using LibHouse.Infrastructure.Controllers.Http.Residents.Interfaces;
using LibHouse.Infrastructure.Controllers.ViewModels.Residents;
using System;
using System.Threading.Tasks;

namespace LibHouse.Infrastructure.Controllers.Http.Residents.Adapters
{
    public class ResidentsWebApiAdapter : IHttpResident
    {
        private Func<InputResidentRoomPreferencesRegistration, Task<OutputResidentRoomPreferencesRegistration>> OnResidentRoomPreferencesRegistrationFunction { get; set; }

        public void OnResidentRoomPreferencesRegistration(Func<InputResidentRoomPreferencesRegistration, Task<OutputResidentRoomPreferencesRegistration>> on) => OnResidentRoomPreferencesRegistrationFunction = on;

        public async Task<Result> ResidentRoomPreferencesRegistration(ResidentRoomPreferencesRegistrationViewModel preferencesRegistrationViewModel)
        {
            InputResidentRoomPreferencesRegistration input = preferencesRegistrationViewModel.Convert();
            OutputResidentRoomPreferencesRegistration output = await OnResidentRoomPreferencesRegistrationFunction(input);
            return output.IsSuccess ? Result.Success() : Result.Fail(output.RoomPreferencesRegistrationMessage);
        }

        private Func<InputResidentServicesPreferencesRegistration, Task<OutputResidentServicesPreferencesRegistration>> OnResidentServicesPreferencesRegistrationFunction { get; set; }

        public void OnResidentServicesPreferencesRegistration(Func<InputResidentServicesPreferencesRegistration, Task<OutputResidentServicesPreferencesRegistration>> on) => OnResidentServicesPreferencesRegistrationFunction = on;

        public async Task<Result> ResidentServicesPreferencesRegistration(ResidentServicesPreferencesRegistrationViewModel preferencesRegistrationViewModel)
        {
            InputResidentServicesPreferencesRegistration input = preferencesRegistrationViewModel.Convert();
            OutputResidentServicesPreferencesRegistration output = await OnResidentServicesPreferencesRegistrationFunction(input);
            return output.IsSuccess ? Result.Success() : Result.Fail(output.ServicesPreferencesRegistrationMessage);
        }

        private Func<InputResidentChargePreferencesRegistration, Task<OutputResidentChargePreferencesRegistration>> OnResidentChargePreferencesRegistrationFunction { get; set; }

        public void OnResidentChargePreferencesRegistration(Func<InputResidentChargePreferencesRegistration, Task<OutputResidentChargePreferencesRegistration>> on) => OnResidentChargePreferencesRegistrationFunction = on;

        public async Task<Result> ResidentChargePreferencesRegistration(ResidentChargePreferencesRegistrationViewModel preferencesRegistrationViewModel)
        {
            InputResidentChargePreferencesRegistration input = preferencesRegistrationViewModel.Convert();
            OutputResidentChargePreferencesRegistration output = await OnResidentChargePreferencesRegistrationFunction(input);
            return output.IsSuccess ? Result.Success() : Result.Fail(output.ChargePreferencesRegistrationMessage);
        }

        private Func<InputResidentGeneralPreferencesRegistration, Task<OutputResidentGeneralPreferencesRegistration>> OnResidentGeneralPreferencesRegistrationFunction { get; set; }

        public void OnResidentGeneralPreferencesRegistration(Func<InputResidentGeneralPreferencesRegistration, Task<OutputResidentGeneralPreferencesRegistration>> on) => OnResidentGeneralPreferencesRegistrationFunction = on;

        public async Task<Result> ResidentGeneralPreferencesRegistration(ResidentGeneralPreferencesRegistrationViewModel preferencesRegistrationViewModel)
        {
            InputResidentGeneralPreferencesRegistration input = preferencesRegistrationViewModel.Convert();
            OutputResidentGeneralPreferencesRegistration output = await OnResidentGeneralPreferencesRegistrationFunction(input);
            return output.IsSuccess ? Result.Success() : Result.Fail(output.GeneralPreferencesRegistrationMessage);
        }

        private Func<InputResidentLocalizationPreferencesRegistration, Task<OutputResidentLocalizationPreferencesRegistration>> OnResidentLocalizationPreferencesRegistrationFunction { get; set; }

        public void OnResidentLocalizationPreferencesRegistration(Func<InputResidentLocalizationPreferencesRegistration, Task<OutputResidentLocalizationPreferencesRegistration>> on) => OnResidentLocalizationPreferencesRegistrationFunction = on;

        public async Task<Result> ResidentLocalizationPreferencesRegistration(ResidentLocalizationPreferencesRegistrationViewModel preferencesRegistrationViewModel)
        {
            InputResidentLocalizationPreferencesRegistration input = preferencesRegistrationViewModel.Convert();
            OutputResidentLocalizationPreferencesRegistration output = await OnResidentLocalizationPreferencesRegistrationFunction(input);
            return output.IsSuccess ? Result.Success() : Result.Fail(output.LocalizationPreferencesRegistrationMessage);
        }
    }
}