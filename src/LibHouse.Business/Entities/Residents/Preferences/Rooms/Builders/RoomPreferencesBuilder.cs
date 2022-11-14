namespace LibHouse.Business.Entities.Residents.Preferences.Rooms.Builders
{
    public class RoomPreferencesBuilder : IRoomPreferencesBuilder
    {
        private readonly RoomPreferences _roomPreferences;

        public RoomPreferencesBuilder()
        {
            _roomPreferences = new();
        }

        public RoomPreferences GetRoomPreferences()
        {
            return _roomPreferences;
        }

        public void WithBathroomPreferences(bool wantPrivateBathroom)
        {
            BathroomType bathroomType = wantPrivateBathroom ? BathroomType.Single : BathroomType.Shared;
            _roomPreferences.AddBathroomPreferences(bathroomType);
        }

        public void WithDormitoryPreferences(bool wantSingleDormitory, bool wantFurnishedDormitory)
        {
            DormitoryType dormitoryType = wantSingleDormitory ? DormitoryType.Single : DormitoryType.Shared;
            _roomPreferences.AddDormitoryPreferences(dormitoryType, wantFurnishedDormitory);
        }

        public void WithGaragePreferences(bool wantGarage)
        {
            _roomPreferences.AddGaragePreferences(wantGarage);
        }

        public void WithKitchenPreferences(bool wantStove, bool wantMicrowave, bool wantRefrigerator)
        {
            _roomPreferences.AddKitchenPreferences(wantStove, wantMicrowave, wantRefrigerator);
        }

        public void WithOtherPreferences(bool wantServiceArea, bool wantRecreationArea)
        {
            _roomPreferences.AddOtherRoomPreferences(wantServiceArea, wantRecreationArea);
        }
    }
}