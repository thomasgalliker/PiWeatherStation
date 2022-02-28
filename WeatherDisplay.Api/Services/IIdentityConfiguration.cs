namespace WeatherDisplay.Api.Services
{
    public interface IIdentityConfiguration
    {
        string JwtKey { get; }

        string JwtExpireDays { get; }

        string JwtIssuer { get; }
    }
}