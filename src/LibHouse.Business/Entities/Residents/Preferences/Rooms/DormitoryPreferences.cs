namespace LibHouse.Business.Entities.Residents.Preferences.Rooms
{
    public record DormitoryPreferences
    {
        public DormitoryType DormitoryType { get; init; }
        public bool RequireFurnishedDormitory { get; init; }

        public DormitoryPreferences(DormitoryType dormitoryType, bool requireFurnishedDormitory)
        {
            DormitoryType = dormitoryType;
            RequireFurnishedDormitory = requireFurnishedDormitory;
        }

        public DormitoryType GetDormitoryType()
        {
            return DormitoryType;
        }

        public bool RequiresFurnishedDormitory()
        {
            return RequireFurnishedDormitory;
        }
    }
}