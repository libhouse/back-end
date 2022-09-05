namespace LibHouse.Infrastructure.Controllers.Responses.Users
{
    /// <summary>
    /// Representa os dados de criação do registro do usuário.
    /// </summary>
    public record UserRegistrationResponse
    {
        /// <summary>
        /// O token de registro do usuário.
        /// </summary>
        public string RegistrationToken { get; init; }

        public UserRegistrationResponse(string registrationToken)
        {
            RegistrationToken = registrationToken;
        }
    }
}