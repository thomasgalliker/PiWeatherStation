using System.Threading.Tasks;
using WeatherDisplay.Model.OpenWeatherMap;

namespace WeatherDisplay.Services
{
    public interface IOpenWeatherMapService
    {
        Task<WeatherResponse> GetWeatherInfoAsync(double latitude, double longitude);
    }
}