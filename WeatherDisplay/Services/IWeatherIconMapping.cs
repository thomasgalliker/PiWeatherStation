using System.IO;
using System.Threading.Tasks;
using WeatherDisplay.Model.OpenWeatherMap;

namespace WeatherDisplay.Services
{
    public interface IWeatherIconMapping
    {
        Task<Stream> GetIconAsync(WeatherCondition weatherCondition);
    }
}