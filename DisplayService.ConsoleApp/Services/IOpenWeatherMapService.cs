using System.Threading.Tasks;

namespace DisplayService.ConsoleApp.Service
{
    public interface IOpenWeatherMapService
    {
        Task<WeatherResponse> GetWeatherInfoAsync(double latitude, double longitude);
    }
}