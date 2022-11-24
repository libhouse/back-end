using System;

namespace LibHouse.Infrastructure.Controllers.ViewModels.Residents
{
    public record ResidentGeneralPreferencesRegistrationViewModel
    {
        public Guid ResidentId { get; init; }
        public bool WantSpaceForAnimals { get; init; }
        public bool AcceptChildren { get; init; }
        public bool WantsToParty { get; init; }
        public bool AcceptSmokers { get; init; }
        public bool AcceptsOnlyMenAsRoommates { get; init; }
        public bool AcceptsOnlyWomenAsRoommates { get; init; }
        public int MinimumNumberOfRoommatesDesired { get; init; }
        public int MaximumNumberOfRoommatesDesired { get; init; }
    }
}