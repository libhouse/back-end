using System.Text.Json.Serialization;

namespace LibHouse.Infrastructure.WebClients.LocationIQ.Outputs
{
    public record OutputForwardGeocoding
    {
        [JsonPropertyName("place_id")]
        public string PlaceId { get; init; }
        [JsonPropertyName("osm_type")]
        public string OsmType { get; init; }
        [JsonPropertyName("osm_id")]
        public string OsmId { get; init; }
        [JsonPropertyName("boundingbox")]
        public string[] BoundingBox { get; init; }
        [JsonPropertyName("lat")]
        public string Latitude { get; init; }
        [JsonPropertyName("lon")]
        public string Longitude { get; init; }
        [JsonPropertyName("display_name")]
        public string DisplayName { get; init; }
        [JsonPropertyName("class")]
        public string Class { get; init; }
        [JsonPropertyName("type")]
        public string Type { get; init; }
        [JsonPropertyName("importance")]
        public double Importance { get; init; }
    }
}