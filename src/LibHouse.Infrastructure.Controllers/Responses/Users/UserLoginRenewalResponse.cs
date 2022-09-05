using System;
using System.Collections.Generic;

namespace LibHouse.Infrastructure.Controllers.Responses.Users
{
    /// <summary>
    /// Representa os dados de autenticação de um usuário
    /// </summary>
    public record UserLoginRenewalResponse
    {
        /// <summary>
        /// Os dados essenciais de cadastro do usuário (nome completo, tipo de usuário, etc.)
        /// </summary>
        public UserLoginRenewalProfileResponse User { get; init; }

        /// <summary>
        /// O token de acesso atrelado ao usuário
        /// </summary>
        public string AccessToken { get; init; }

        /// <summary>
        /// A data e hora de expiração do token de acesso
        /// </summary>
        public DateTime ExpiresInAccessToken { get; init; }

        /// <summary>
        /// O token de renovação atrelado ao token de acesso do usuário
        /// </summary>
        public string RefreshToken { get; init; }

        /// <summary>
        /// A data e hora de expiração do refresh token
        /// </summary>
        public DateTime ExpiresInRefreshToken { get; init; }

        /// <summary>
        /// A lista de claims pertencentes ao usuário
        /// </summary>
        public IEnumerable<UserLoginRenewalClaimResponse> Claims { get; init; }

        public UserLoginRenewalResponse(
            UserLoginRenewalProfileResponse user,
            string accessToken,
            DateTime expiresInAccessToken,
            string refreshToken,
            DateTime expiresInRefreshToken,
            IEnumerable<UserLoginRenewalClaimResponse> claims)
        {
            User = user;
            AccessToken = accessToken;
            ExpiresInAccessToken = expiresInAccessToken;
            RefreshToken = refreshToken;
            ExpiresInRefreshToken = expiresInRefreshToken;
            Claims = claims;
        }
    }
}