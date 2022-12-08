using LibHouse.Infrastructure.Controllers.ViewModels.Residents;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace LibHouse.API.Filters.Swagger.Requests.Residents
{
    public class ResidentLocalizationPreferencesRegistrationViewModelExample : IExamplesProvider<ResidentLocalizationPreferencesRegistrationViewModel>
    {
        public ResidentLocalizationPreferencesRegistrationViewModel GetExamples()
        {
            return new()
            {
                LandmarkAddressCity = "São Paulo",
                LandmarkAddressComplement = "de 321 ao fim - lado ímpar",
                LandmarkAddressDescription = "Rua São Bento",
                LandmarkAddressFederativeUnit = "SP",
                LandmarkAddressNeighborhood = "Centro",
                LandmarkAddressNumber = 321,
                LandmarkAddressPostalCodeNumber = "01011100",
                ResidentId = Guid.NewGuid()
            };
        }
    }
}