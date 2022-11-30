using LibHouse.Business.Entities.Localizations;

namespace LibHouse.Business.Entities.Residents.Preferences.Localizations
{
    public class LocalizationPreferences
    {
        public LandmarkPreferences LandmarkPreferences { get; private set; }

        public void AddLandmarkPreferences(Address landmarkAddress)
        {
            LandmarkPreferences = new(landmarkAddress);
        }

        public bool HaveLandmarkPreferences()
        {
            return LandmarkPreferences is not null;
        }
    }
}