namespace LibHouse.Business.Application.Residents.Outputs
{
    public record OutputResidentLocalizationPreferencesRegistration
    {
        public bool IsSuccess { get; init; }
        public string LocalizationPreferencesRegistrationMessage { get; init; }
    }
}