using System;
using System.Collections.Generic;

namespace LibHouse.Infrastructure.Authentication.Token.Models
{
    public class AccessToken
    {
        public string Id { get; }
        public string Value { get; }
        public DateTime ExpiresIn { get; }
        public IEnumerable<AccessTokenClaim> Claims { get; }
        public RefreshToken RefreshToken { get; }

        public AccessToken(
            string id, 
            string value,
            DateTime expiresIn,
            IEnumerable<AccessTokenClaim> claims, 
            RefreshToken refreshToken)
        {
            Id = id;
            Value = value;
            ExpiresIn = expiresIn;
            Claims = claims;
            RefreshToken = refreshToken;
        }

        public AccessToken(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return $"Access token {Id}: {Value}";
        }
    }
}