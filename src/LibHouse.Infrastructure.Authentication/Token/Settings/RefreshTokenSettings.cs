namespace LibHouse.Infrastructure.Authentication.Token.Settings
{
    public class RefreshTokenSettings
    {
        public int TokenLength { get; set; }
        public int ExpiresInMonths { get; set; }
    }
}