namespace LibHouse.Business.Entities.Residents.Preferences.Rooms
{
    public class RoomPreferences
    {
        public DormitoryPreferences DormitoryPreferences { get; private set; }
        public BathroomPreferences BathroomPreferences { get; private set; }
        public GaragePreferences GaragePreferences { get; private set; }
        public KitchenPreferences KitchenPreferences { get; private set; }
        public OtherRoomPreferences OtherRoomPreferences { get; private set; }

        public void AddDormitoryPreferences(DormitoryType dormitoryType, bool requireFurnishedDormitory)
        {
            DormitoryPreferences = new(dormitoryType, requireFurnishedDormitory);
        }

        public bool HaveDormitoryPreferences()
        {
            return DormitoryPreferences is not null;
        }

        public void AddBathroomPreferences(BathroomType bathroomType)
        {
            BathroomPreferences = new(bathroomType);
        }

        public bool HaveBathroomPreferences()
        {
            return BathroomPreferences is not null;
        }

        public void AddGaragePreferences(bool garageIsRequired)
        {
            GaragePreferences = new(garageIsRequired);
        }

        public bool HaveGaragePreferences()
        {
            return GaragePreferences is not null;
        }

        public void AddKitchenPreferences(
            bool stoveIsRequired, 
            bool microwaveIsRequired,
            bool refrigeratorIsRequired)
        {
            KitchenPreferences = new(stoveIsRequired, microwaveIsRequired, refrigeratorIsRequired);
        }

        public bool HaveKitchenPreferences()
        {
            return KitchenPreferences is not null;
        }

        public void AddOtherRoomPreferences(bool serviceAreaIsRequired, bool recreationAreaIsRequired)
        {
            OtherRoomPreferences = new(serviceAreaIsRequired, recreationAreaIsRequired);
        }

        public bool HaveOtherRoomPreferences()
        {
            return OtherRoomPreferences is not null;
        }
    }
}