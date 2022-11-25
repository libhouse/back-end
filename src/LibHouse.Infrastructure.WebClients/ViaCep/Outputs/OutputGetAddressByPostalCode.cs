using System.Text.Json.Serialization;

namespace LibHouse.Infrastructure.WebClients.ViaCep.Outputs
{
    public record OutputGetAddressByPostalCode
    {
        [JsonPropertyName("cep")]
        public string PostalCode { get; init; }
        [JsonPropertyName("logradouro")]
        public string Street { get; init; }
        [JsonPropertyName("complemento")]
        public string Complement { get; init; }
        [JsonPropertyName("localidade")]
        public string Localization { get; init; }
        [JsonPropertyName("bairro")]
        public string Neighborhood { get; init; }
        [JsonPropertyName("uf")]
        public string FederativeUnit { get; init; }
        [JsonPropertyName("ibge")]
        public string IBGECode { get; init; }
        [JsonPropertyName("ddd")]
        public string DDD { get; init; }
        [JsonPropertyName("siafi")]
        public string SIAFICode { get; init; }
    }
}