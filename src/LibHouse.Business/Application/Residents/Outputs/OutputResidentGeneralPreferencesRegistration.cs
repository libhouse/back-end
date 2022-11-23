namespace LibHouse.Business.Application.Residents.Outputs
{
    public record OutputResidentGeneralPreferencesRegistration
    {
        public bool IsSuccess { get; init; }
        public string GeneralPreferencesRegistrationMessage { get; init; }
    }
}