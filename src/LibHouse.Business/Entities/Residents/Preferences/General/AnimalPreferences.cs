namespace LibHouse.Business.Entities.Residents.Preferences.General
{
    public record AnimalPreferences
    {
        public AnimalPreferences(bool wantSpaceForAnimals)
        {
            WantSpaceForAnimals = wantSpaceForAnimals;
        }

        public bool WantSpaceForAnimals { get; }

        public bool RequiresAcceptanceOfAnimals()
        {
            return WantSpaceForAnimals;
        }
    }
}