namespace LibHouse.Infrastructure.Controllers.ViewModels.Residents
{
    public record ResidentDormitoryPreferencesRegistrationViewModel
    {
        public bool WantSingleDormitory { get; init; }
        public bool WantFurnishedDormitory { get; init; }
    }
}