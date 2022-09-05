using System;
using System.Collections.Generic;

namespace LibHouse.Business.Application.Users.Gateways.Outputs
{
    public record OutputUserLoginRenewalGateway
    {
        public bool IsSuccess { get; init; }
        public string LoginRenewalMessage { get; init; }
        public string AccessToken { get; init; }
        public DateTime? ExpiresInAccessToken { get; init; }
        public string RefreshToken { get; init; }
        public DateTime? ExpiresInRefreshToken { get; init; }
        public IEnumerable<KeyValuePair<string, string>> Claims { get; init; }

        public OutputUserLoginRenewalGateway(
            bool isSuccess = false,
            string loginRenewalMessage = "",
            string accessToken = "",
            DateTime? expiresInAccessToken = null,
            string refreshToken = "",
            DateTime? expiresInRefreshToken = null,
            IEnumerable<KeyValuePair<string, string>> claims = null)
        {
            IsSuccess = isSuccess;
            LoginRenewalMessage = loginRenewalMessage;
            AccessToken = accessToken;
            ExpiresInAccessToken = expiresInAccessToken;
            RefreshToken = refreshToken;
            ExpiresInRefreshToken = expiresInRefreshToken;
            Claims = claims;
        }
    }
}