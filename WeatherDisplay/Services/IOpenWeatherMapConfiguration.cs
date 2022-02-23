namespace WeatherDisplay.Services
{
    public interface IOpenWeatherMapConfiguration
    {
        string ApiKey { get; }

        string UnitSystem { get; }
    }
}