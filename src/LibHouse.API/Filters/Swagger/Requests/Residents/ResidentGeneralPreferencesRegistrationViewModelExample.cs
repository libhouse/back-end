using LibHouse.Infrastructure.Controllers.ViewModels.Residents;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace LibHouse.API.Filters.Swagger.Requests.Residents
{
    public class ResidentGeneralPreferencesRegistrationViewModelExample : IExamplesProvider<ResidentGeneralPreferencesRegistrationViewModel>
    {
        public ResidentGeneralPreferencesRegistrationViewModel GetExamples()
        {
            return new()
            {
                AcceptChildren = true,
                AcceptSmokers = true,
                AcceptsOnlyMenAsRoommates = false,
                AcceptsOnlyWomenAsRoommates = false,
                MaximumNumberOfRoommatesDesired = 4,
                MinimumNumberOfRoommatesDesired = 1,
                ResidentId = Guid.NewGuid(),
                WantSpaceForAnimals = true,
                WantsToParty = false
            };
        }
    }
}