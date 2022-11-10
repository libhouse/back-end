using LibHouse.Business.Entities.Residents.Preferences.Rooms;

namespace LibHouse.Business.Entities.Residents.Preferences
{
    public class ResidentPreferences
    {
        private RoomPreferences RoomPreferences { get; set; }

        public void AddRoomPreferences(RoomPreferences roomPreferences)
        {
            RoomPreferences = roomPreferences;
        }

        public bool HaveRoomPreferences()
        {
            return RoomPreferences is not null;
        }
    }
}