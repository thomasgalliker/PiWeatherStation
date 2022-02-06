using System.Threading.Tasks;

namespace DisplayService.ConsoleApp.Services
{
    public interface IOpenWeatherMapService
    {
        Task<WeatherResponse> GetWeatherInfoAsync(double latitude, double longitude);
    }
}