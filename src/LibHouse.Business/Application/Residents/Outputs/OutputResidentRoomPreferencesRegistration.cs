namespace LibHouse.Business.Application.Residents.Outputs
{
    public record OutputResidentRoomPreferencesRegistration
    {
        public bool IsSuccess { get; init; }
        public string RoomPreferencesRegistrationMessage { get; init; }
    }
}