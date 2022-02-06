using System.Threading.Tasks;

namespace DisplayService.ConsoleApp.Services
{
    public class NullOpenWeatherMapService : IOpenWeatherMapService
    {
        public Task<WeatherResponse> GetWeatherInfoAsync(double latitude, double longitude)
        {
            return Task.FromResult(new WeatherResponse
            {
                ConditionId = 1,
                Temperature = -27,
                UnitSystem = "metric",
            });
        }
    }
}