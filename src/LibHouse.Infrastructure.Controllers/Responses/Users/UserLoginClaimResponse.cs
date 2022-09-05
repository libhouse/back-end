namespace LibHouse.Infrastructure.Controllers.Responses.Users
{
    /// <summary>
    /// Representa uma claim pertencente ao usuário
    /// </summary>
    public record UserLoginClaimResponse
    {
        /// <summary>
        /// O valor associado à claim
        /// </summary>
        public string Value { get; init; }

        /// <summary>
        /// O nome de identificação da claim
        /// </summary>
        public string Type { get; init; }

        public UserLoginClaimResponse(string value, string type)
        {
            Value = value;
            Type = type;
        }
    }
}