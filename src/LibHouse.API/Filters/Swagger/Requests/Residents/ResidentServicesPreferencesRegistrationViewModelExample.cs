using LibHouse.Infrastructure.Controllers.ViewModels.Residents;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace LibHouse.API.Filters.Swagger.Requests.Residents
{
    public class ResidentServicesPreferencesRegistrationViewModelExample : IExamplesProvider<ResidentServicesPreferencesRegistrationViewModel>
    {
        public ResidentServicesPreferencesRegistrationViewModel GetExamples()
        {
            return new()
            {
                ResidentId = Guid.NewGuid(),
                WantCableTelevisionService = true,
                WantHouseCleaningService = false,
                WantInternetService = true
            };
        }
    }
}