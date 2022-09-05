namespace LibHouse.Infrastructure.Authentication.Token.Settings
{
    public class AccessTokenSettings
    {
        public string Secret { get; set; }
        public int ExpiresInSeconds { get; set; }
        public string Issuer { get; set; }
        public string ValidIn { get; set; }
    }
}