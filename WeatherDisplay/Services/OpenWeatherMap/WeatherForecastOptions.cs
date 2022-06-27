namespace WeatherDisplay.Services.OpenWeatherMap
{
    public class WeatherForecastOptions
    {
        public static readonly WeatherForecastOptions Default = new WeatherForecastOptions();

        public WeatherForecastKind WeatherForecastKind { get; set; } = WeatherForecastKind.Default;

        public int Count { get; set; }
    }
}