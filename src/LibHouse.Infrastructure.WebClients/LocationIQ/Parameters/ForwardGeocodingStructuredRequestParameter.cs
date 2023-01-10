namespace LibHouse.Infrastructure.WebClients.LocationIQ.Parameters
{
    public record ForwardGeocodingStructuredRequestParameter
    {
        public string Street { get; init; }
        public string City { get; init; }
        public string County { get; init; }
        public string State { get; init; }
        public string Country { get; init; }
        public int Limit { get; init; }

        internal bool IsValid()
        {
            return !string.IsNullOrEmpty(Street)
                && !string.IsNullOrEmpty(City)
                && !string.IsNullOrEmpty(County)
                && !string.IsNullOrEmpty(State)
                && !string.IsNullOrEmpty(Country)
                && Limit > 0;
        }
    }
}