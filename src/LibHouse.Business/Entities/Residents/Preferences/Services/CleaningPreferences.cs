namespace LibHouse.Business.Entities.Residents.Preferences.Services
{
    public record CleaningPreferences
    {
        public bool HouseCleaningIsRequired { get; init; }

        public CleaningPreferences(bool houseCleaningIsRequired)
        {
            HouseCleaningIsRequired = houseCleaningIsRequired;
        }

        public bool RequiresHouseCleaningService()
        {
            return HouseCleaningIsRequired;
        }
    }
}