namespace LibHouse.Business.Application.Localizations.Outputs
{
    public record OutputPostalCodeSearch
    {
        public string PostalCode { get; init; }
        public string Street { get; init; }
        public string Complement { get; init; }
        public string Localization { get; init; }
        public string Neighborhood { get; init; }
        public string FederativeUnit { get; init; }
        public bool IsSuccess { get; init; }
        public string PostalCodeSearchMessage { get; init; }
    }
}