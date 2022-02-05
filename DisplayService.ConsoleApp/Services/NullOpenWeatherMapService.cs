using System.Threading.Tasks;

namespace DisplayService.ConsoleApp.Service
{
    public class NullOpenWeatherMapService : IOpenWeatherMapService
    {
        public Task<WeatherResponse> GetWeatherInfoAsync(double longitude, double latitude)
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