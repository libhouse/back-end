namespace LibHouse.Business.Entities.Residents.Preferences.Rooms
{
    public record BathroomPreferences
    {
        public BathroomType BathroomType { get; init; }

        public BathroomPreferences(BathroomType bathroomType)
        {
            BathroomType = bathroomType;
        }

        public BathroomType GetBathroomType()
        {
            return BathroomType;
        }
    }
}