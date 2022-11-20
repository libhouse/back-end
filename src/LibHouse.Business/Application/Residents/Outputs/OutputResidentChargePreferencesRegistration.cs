namespace LibHouse.Business.Application.Residents.Outputs
{
    public record OutputResidentChargePreferencesRegistration
    {
        public bool IsSuccess { get; init; }
        public string ChargePreferencesRegistrationMessage { get; init; }
    }
}