using LibHouse.Business.Entities.Residents.Preferences.Rooms;
using System;

namespace LibHouse.Business.Entities.Residents.Preferences
{
    public class ResidentPreferences
    {
        private Resident Resident { get; }
        private Guid ResidentId { get; }
        private RoomPreferences RoomPreferences { get; set; }

        public void AddRoomPreferences(RoomPreferences roomPreferences)
        {
            RoomPreferences = roomPreferences;
        }

        public RoomPreferences GetRoomPreferences()
        {
            return RoomPreferences;
        }

        public bool HaveRoomPreferences()
        {
            return RoomPreferences is not null;
        }

        public override string ToString() => $"Resident {ResidentId} preferences";
    }
}