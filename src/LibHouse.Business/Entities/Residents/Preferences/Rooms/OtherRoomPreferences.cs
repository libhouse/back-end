namespace LibHouse.Business.Entities.Residents.Preferences.Rooms
{
    public record OtherRoomPreferences
    {
        public bool ServiceAreaIsRequired { get; }
        public bool RecreationAreaIsRequired { get; }

        public OtherRoomPreferences(bool serviceAreaIsRequired, bool recreationAreaIsRequired)
        {
            ServiceAreaIsRequired = serviceAreaIsRequired;
            RecreationAreaIsRequired = recreationAreaIsRequired;
        }

        public bool RequiresServiceArea()
        {
            return ServiceAreaIsRequired;
        }

        public bool RequiresRecreationArea()
        {
            return RecreationAreaIsRequired;
        }
    }
}