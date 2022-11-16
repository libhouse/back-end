namespace LibHouse.Business.Entities.Residents.Preferences.Services
{
    public record TelevisionPreferences
    {
        public bool CableTelevisionIsRequired { get; init; }

        public TelevisionPreferences(bool cableTelevisionIsRequired)
        {
            CableTelevisionIsRequired = cableTelevisionIsRequired;
        }

        public bool RequiresCableTelevisionService()
        {
            return CableTelevisionIsRequired;
        }
    }
}