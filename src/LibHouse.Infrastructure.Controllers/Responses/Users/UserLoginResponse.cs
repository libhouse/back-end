using System;
using System.Collections.Generic;

namespace LibHouse.Infrastructure.Controllers.Responses.Users
{
    /// <summary>
    /// Representa os dados de autenticação de um usuário
    /// </summary>
    public record UserLoginResponse
    {
        /// <summary>
        /// Os dados essenciais de cadastro do usuário (nome completo, tipo de usuário, etc.)
        /// </summary>
        public UserLoginProfileResponse User { get; init; }

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
        public IEnumerable<UserLoginClaimResponse> Claims { get; init; }

        public UserLoginResponse(
            UserLoginProfileResponse user,
            string accessToken,
            DateTime expiresInAccessToken,
            string refreshToken,
            DateTime expiresInRefreshToken,
            IEnumerable<UserLoginClaimResponse> claims)
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