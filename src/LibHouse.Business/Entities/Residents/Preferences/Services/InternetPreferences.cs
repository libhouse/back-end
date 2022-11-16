namespace LibHouse.Business.Entities.Residents.Preferences.Services
{
    public record InternetPreferences
    {
        public bool InternetServiceIsRequired { get; init; }

        public InternetPreferences(bool internetServiceIsRequired)
        {
            InternetServiceIsRequired = internetServiceIsRequired;
        }

        public bool RequiresInternetService()
        {
            return InternetServiceIsRequired;
        }
    }
}