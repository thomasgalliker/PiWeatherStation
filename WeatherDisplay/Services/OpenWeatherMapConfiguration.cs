namespace WeatherDisplay.Services
{
    public class OpenWeatherMapConfiguration : IOpenWeatherMapConfiguration
    {
        public OpenWeatherMapConfiguration()
        {
        }

        public string ApiKey { get; set; }

        public string UnitSystem { get; set; }
    }
}