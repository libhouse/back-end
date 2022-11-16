namespace LibHouse.Business.Entities.Residents.Preferences.Services
{
    public class ServicesPreferences
    {
        public CleaningPreferences CleaningPreferences { get; private set; }
        public InternetPreferences InternetPreferences { get; private set; }
        public TelevisionPreferences TelevisionPreferences { get; private set; }

        public void AddCleaningPreferences(bool houseCleaningIsRequired)
        {
            CleaningPreferences = new(houseCleaningIsRequired);
        }

        public bool HaveCleaningPreferences()
        {
            return CleaningPreferences is not null;
        }

        public void AddInternetPreferences(bool internetServiceIsRequired)
        {
            InternetPreferences = new(internetServiceIsRequired);
        }

        public bool HaveInternetPreferences()
        {
            return InternetPreferences is not null;
        }

        public void AddTelevisionPreferences(bool cableTelevisionIsRequired)
        {
            TelevisionPreferences = new(cableTelevisionIsRequired);
        }

        public bool HaveTelevisionPreferences()
        {
            return TelevisionPreferences is not null;
        }
    }
}