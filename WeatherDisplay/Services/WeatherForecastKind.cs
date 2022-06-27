namespace WeatherDisplay.Services
{
    public enum WeatherForecastKind
    {
        /// <summary>
        /// 5 day / 3 hour forecast (max. 40 timestamps).
        /// https://openweathermap.org/forecast5
        /// </summary>
        Default = 0,

        /// <summary>
        /// Hourly forecast for 4 days (max. 96 timestamps).
        /// </summary>
        Hourly = 1,

        /// <summary>
        /// 16 day / daily forecast (max. 17 timestamps).
        /// </summary>
        Daily,
    }
}