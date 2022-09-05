namespace LibHouse.Infrastructure.Authentication.Token.Models
{
    public class AccessTokenClaim
    {
        public string Value { get; }

        public string Type { get; }

        public AccessTokenClaim(string value, string type)
        {
            Value = value;
            Type = type;
        }
    }
}