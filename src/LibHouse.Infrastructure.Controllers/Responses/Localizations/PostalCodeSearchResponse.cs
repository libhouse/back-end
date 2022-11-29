namespace LibHouse.Infrastructure.Controllers.Responses.Localizations
{
    /// <summary>
    /// Representa os dados de endereço de um código postal
    /// </summary>
    public record PostalCodeSearchResponse
    {
        /// <summary>
        /// O nome do logradouro associado ao código postal
        /// </summary>
        public string Street { get; init; }
        /// <summary>
        /// O complemento do logradouro associado ao código postal
        /// </summary>
        public string Complement { get; init; }
        /// <summary>
        /// O nome da cidade do logradouro associado ao código postal
        /// </summary>
        public string Localization { get; init; }
        /// <summary>
        /// O nome do bairro do logradouro associado ao código postal
        /// </summary>
        public string Neighborhood { get; init; }
        /// <summary>
        /// A sigla da unidade federativa do logradouro associado ao código postal
        /// </summary>
        public string FederativeUnit { get; init; }
    }
}