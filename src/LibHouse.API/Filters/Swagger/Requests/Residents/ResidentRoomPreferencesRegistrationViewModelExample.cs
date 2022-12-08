using LibHouse.Infrastructure.Controllers.ViewModels.Residents;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace LibHouse.API.Filters.Swagger.Requests.Residents
{
    public class ResidentRoomPreferencesRegistrationViewModelExample : IExamplesProvider<ResidentRoomPreferencesRegistrationViewModel>
    {
        public ResidentRoomPreferencesRegistrationViewModel GetExamples()
        {
            return new()
            {
                ResidentId = Guid.NewGuid(),
                BathroomPreferences = new()
                {
                    WantPrivateBathroom = true
                },
                DormitoryPreferences = new()
                {
                    WantFurnishedDormitory = true,
                    WantSingleDormitory = true
                },
                GaragePreferences = new()
                {
                    WantGarage = false
                },
                KitchenPreferences = new()
                {
                    WantMicrowave = true,
                    WantRefrigerator = true,
                    WantStove = true,
                },
                OtherPreferences = new()
                {
                    WantRecreationArea = false,
                    WantServiceArea = true
                }
            };
        }
    }
}