namespace LibHouse.Infrastructure.Controllers.ViewModels.Residents
{
    public record ResidentKitchenPreferencesRegistrationViewModel
    {
        public bool WantStove { get; init; }
        public bool WantMicrowave { get; init; }
        public bool WantRefrigerator { get; init; }
    }
}