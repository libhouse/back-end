namespace LibHouse.Business.Application.Residents.Outputs
{
    public record OutputResidentServicesPreferencesRegistration
    {
        public bool IsSuccess { get; init; }
        public string ServicesPreferencesRegistrationMessage { get; init; }
    }
}