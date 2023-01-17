using System.Threading.Tasks;

namespace WeatherDisplay.Services.Astronomy
{
    public interface ISpaceWeatherService
    {
        Task<PlanetaryKIndexForecast[]> GetPlanetaryKIndexForecastAsync();
    }
}