namespace WeatherDisplay.Api.Services
{
    public class IdentityOptions
    {
        public const string SectionName = "Identity";

        public string JwtKey { get; set; }

        public int JwtExpireDays { get; set; }

        public string JwtIssuer { get; set; }
    }
}