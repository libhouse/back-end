namespace LibHouse.Business.Entities.Residents.Preferences.Rooms
{
    public record GaragePreferences
    {
        public bool GarageIsRequired { get; init; }

        public GaragePreferences(bool garageIsRequired)
        {
            GarageIsRequired = garageIsRequired;
        }

        public bool RequiresGarage()
        {
            return GarageIsRequired;
        }
    }
}