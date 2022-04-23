namespace WeatherDisplay.Services
{
    public class OpenWeatherMapConfiguration : IOpenWeatherMapConfiguration
    {
        public string ApiKey { get; set; }

        public string UnitSystem { get; set; }
        
        public string Language { get; set; }

        public bool VerboseLogging { get; set; }
    }
}