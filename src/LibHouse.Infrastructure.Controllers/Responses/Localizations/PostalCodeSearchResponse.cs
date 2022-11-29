namespace LibHouse.Infrastructure.Controllers.Responses.Localizations
{
    public record PostalCodeSearchResponse
    {
        public string Street { get; init; }
        public string Complement { get; init; }
        public string Localization { get; init; }
        public string Neighborhood { get; init; }
        public string FederativeUnit { get; init; }
    }
}