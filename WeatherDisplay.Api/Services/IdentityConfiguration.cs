namespace WeatherDisplay.Api.Services
{
    public class IdentityConfiguration : IIdentityConfiguration
    {
        public string JwtKey { get; set; }

        public string JwtExpireDays { get; set; }

        public string JwtIssuer { get; set; }
    }
}