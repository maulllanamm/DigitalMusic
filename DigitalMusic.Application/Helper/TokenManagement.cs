namespace DigitalMusic.Application.Helper
{
    public class TokenManagement
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiryAccessMinutes { get; set; }
        public int ExpiryRefreshMinutes { get; set; }
    }
}
