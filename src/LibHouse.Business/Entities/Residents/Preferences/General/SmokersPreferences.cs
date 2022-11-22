namespace LibHouse.Business.Entities.Residents.Preferences.General
{
    public record SmokersPreferences
    {
        public SmokersPreferences(bool acceptSmokers)
        {
            AcceptSmokers = acceptSmokers;
        }

        public bool AcceptSmokers { get; }

        public bool RequiresAcceptanceOfSmokers()
        {
            return AcceptSmokers;
        }
    }
}