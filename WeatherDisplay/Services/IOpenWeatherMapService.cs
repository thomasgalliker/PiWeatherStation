using System;
using System.IO;
using System.Threading.Tasks;
using WeatherDisplay.Model.OpenWeatherMap;

namespace WeatherDisplay.Services
{
    public interface IOpenWeatherMapService
    {
        Task<WeatherInfo> GetCurrentWeatherAsync(double latitude, double longitude);

        Task<WeatherForecast> GetWeatherForecastAsync(double latitude, double longitude, WeatherForecastOptions options = null);

        Task<Stream> GetWeatherIconAsync(WeatherCondition weatherCondition, IWeatherIconMapping weatherIconMapping = null);
        
        Task<OneCallWeatherInfo> GetWeatherOneCallAsync(double latitude, double longitude, OneCallOptions oneCallOptions = null);

        Task<OneCallWeatherInfo> GetWeatherOneCallHistoricAsync(double latitude, double longitude, DateTime dateTime, bool onlyCurrent = false);

        Task<AirPollutionInfo> GetAirPollutionAsync(double latitude, double longitude);
    }
}