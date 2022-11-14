namespace LibHouse.Infrastructure.Controllers.ViewModels.Residents
{
    public record ResidentOtherPreferencesRegistrationViewModel
    {
        public bool WantServiceArea { get; init; }
        public bool WantRecreationArea { get; init; }
    }
}