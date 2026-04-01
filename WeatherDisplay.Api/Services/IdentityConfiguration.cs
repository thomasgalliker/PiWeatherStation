namespace WeatherDisplay.Api.Services
{
    public class IdentityConfiguration : IIdentityConfiguration
    {
        public const string SectionName = "Identity";

        public string JwtKey { get; set; }

        public string JwtExpireDays { get; set; }

        public string JwtIssuer { get; set; }
    }
}