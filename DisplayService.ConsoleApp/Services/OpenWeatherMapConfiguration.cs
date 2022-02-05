namespace DisplayService.ConsoleApp.Service
{
    public class OpenWeatherMapConfiguration : IOpenWeatherMapConfiguration
    {
        public string ApiKey => "001c4dffbe586e8e2542fb379031bc99";

        public string UnitSystem => "metric";
    }
}