namespace LibHouse.Business.Entities.Residents.Preferences.Rooms
{
    public record KitchenPreferences
    {
        public bool StoveIsRequired { get; }
        public bool MicrowaveIsRequired { get; }
        public bool RefrigeratorIsRequired { get; }

        public KitchenPreferences(
            bool stoveIsRequired, 
            bool microwaveIsRequired, 
            bool refrigeratorIsRequired)
        {
            StoveIsRequired = stoveIsRequired;
            MicrowaveIsRequired = microwaveIsRequired;
            RefrigeratorIsRequired = refrigeratorIsRequired;
        }

        public bool RequiresStove()
        {
            return StoveIsRequired;
        }

        public bool RequiresMicrowave()
        {
            return MicrowaveIsRequired;
        }

        public bool RequiresRefrigerator()
        {
            return RefrigeratorIsRequired;
        }
    }
}