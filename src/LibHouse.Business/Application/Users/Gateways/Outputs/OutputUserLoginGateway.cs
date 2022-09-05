using System;
using System.Collections.Generic;

namespace LibHouse.Business.Application.Users.Gateways.Outputs
{
    public record OutputUserLoginGateway
    {
        public bool IsSuccess { get; init; }
        public string LoginMessage { get; init; }
        public string AccessToken { get; init; }
        public DateTime? ExpiresInAccessToken { get; init; }
        public string RefreshToken { get; init; }
        public DateTime? ExpiresInRefreshToken { get; init; }
        public IEnumerable<KeyValuePair<string, string>> Claims { get; init; }

        public OutputUserLoginGateway(
            bool isSuccess = false, 
            string loginMessage = "", 
            string accessToken = "", 
            DateTime? expiresInAccessToken = null,
            string refreshToken = "",
            DateTime? expiresInRefreshToken = null,
            IEnumerable<KeyValuePair<string, string>> claims = null)
        {
            IsSuccess = isSuccess;
            LoginMessage = loginMessage;
            AccessToken = accessToken;
            ExpiresInAccessToken = expiresInAccessToken;
            RefreshToken = refreshToken;
            ExpiresInRefreshToken = expiresInRefreshToken;
            Claims = claims;
        }
    }
}