namespace LibHouse.Business.Entities.Residents.Preferences.Rooms.Builders
{
    public interface IRoomPreferencesBuilder
    {
        void WithDormitoryPreferences(bool wantSingleDormitory, bool wantFurnishedDormitory);
        void WithKitchenPreferences(bool wantStove, bool wantMicrowave, bool wantRefrigerator);
        void WithGaragePreferences(bool wantGarage);
        void WithBathroomPreferences(bool wantPrivateBathroom);
        void WithOtherPreferences(bool wantServiceArea, bool wantRecreationArea);
        RoomPreferences GetRoomPreferences();
    }
}