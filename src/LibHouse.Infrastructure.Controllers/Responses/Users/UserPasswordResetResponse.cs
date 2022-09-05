namespace LibHouse.Infrastructure.Controllers.Responses.Users
{
    /// <summary>
    /// Representa os dados de resposta da redefinição de senha do usuário
    /// </summary>
    public record UserPasswordResetResponse
    {
        /// <summary>
        /// O token de redefinição de senha do usuário
        /// </summary>
        public string PasswordResetToken { get; init; }

        public UserPasswordResetResponse(string passwordResetToken)
        {
            PasswordResetToken = passwordResetToken;
        }
    }
}