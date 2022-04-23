using System.IO;
using System.Threading.Tasks;
using WeatherDisplay.Model.OpenWeatherMap;

namespace WeatherDisplay.Services
{
    public interface IOpenWeatherMapService
    {
        Task<WeatherInfo> GetCurrentWeatherAsync(double latitude, double longitude);

        Task<WeatherForecast> GetWeatherForecastAsync(double latitude, double longitude);

        Task<Stream> GetWeatherIconAsync(WeatherCondition weatherCondition, IWeatherIconMapping weatherIconMapping = null);
        
        Task<OneCallWeatherInfo> GetWeatherOneCallAsync(double latitude, double longitude);
    }
}