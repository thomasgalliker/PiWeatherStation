namespace WeatherDisplay.Services
{
    public class OneCallOptions
    {
        public static readonly OneCallOptions Default = new OneCallOptions();

        /// <summary>
        /// Indicates if the CurrentWeather property should be requested.
        /// </summary>
        public bool IncludeCurrentWeather { get; set; } = true;

        /// <summary>
        /// Indicates if the DailyForecasts property should be requested.
        /// </summary>
        public bool IncludeDailyForecasts { get; set; } = true;

        /// <summary>
        /// Indicates if the MinutelyForecasts property should be requested.
        /// </summary>
        public bool IncludeMinutelyForecasts { get; set; } = true;

        /// <summary>
        /// Indicates if the HourlyForecasts property should be requested.
        /// </summary>
        public bool IncludeHourlyForecasts { get; set; } = true;
    }
}