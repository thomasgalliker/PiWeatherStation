using System.IO;
using System.Threading.Tasks;
using WeatherDisplay.Model.OpenWeatherMap;

namespace WeatherDisplay.Services
{
    public interface IOpenWeatherMapService
    {
        Task<WeatherInfo> GetCurrentWeatherAsync(double latitude, double longitude);

        Task<Stream> GetWeatherIconAsync(WeatherCondition weatherCondition);
    }
}