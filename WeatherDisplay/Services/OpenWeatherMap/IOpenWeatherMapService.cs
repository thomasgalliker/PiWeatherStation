using System;
using System.IO;
using System.Threading.Tasks;
using WeatherDisplay.Model.OpenWeatherMap;

namespace WeatherDisplay.Services.OpenWeatherMap
{
    public interface IOpenWeatherMapService
    {
        Task<WeatherInfo> GetCurrentWeatherAsync(double latitude, double longitude);

        /// <summary>
        /// Hourly forecast for 4 days (max. 96 timestamps).
        /// https://openweathermap.org/api/hourly-forecast
        /// </summary>
        Task<WeatherForecast> GetWeatherForecast4Async(double latitude, double longitude, int? count = null);

        /// <summary>
        /// 5 day / 3 hour forecast (max. 40 timestamps).
        /// https://openweathermap.org/forecast5
        /// </summary>
        Task<WeatherForecast> GetWeatherForecast5Async(double latitude, double longitude, int? count = null);

        /// <summary>
        /// 16 day / daily forecast (max. 17 timestamps).
        /// https://openweathermap.org/forecast16
        /// </summary>
        Task<WeatherForecast> GetWeatherForecast16Async(double latitude, double longitude, int? count = null);

        Task<Stream> GetWeatherIconAsync(WeatherCondition weatherCondition, IWeatherIconMapping weatherIconMapping = null);

        Task<OneCallWeatherInfo> GetWeatherOneCallAsync(double latitude, double longitude, OneCallOptions oneCallOptions = null);

        Task<OneCallWeatherInfo> GetWeatherOneCallHistoricAsync(double latitude, double longitude, DateTime dateTime, bool onlyCurrent = false);

        Task<AirPollutionInfo> GetAirPollutionAsync(double latitude, double longitude);
    }
}