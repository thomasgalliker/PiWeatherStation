using System;
using System.IO;
using System.Threading.Tasks;
using DisplayService.Resources;
using WeatherDisplay.Model.OpenWeatherMap;

namespace WeatherDisplay.Services
{
    public class NullOpenWeatherMapService : IOpenWeatherMapService
    {
        private readonly IOpenWeatherMapConfiguration openWeatherMapConfiguration;

        public NullOpenWeatherMapService(IOpenWeatherMapConfiguration openWeatherMapConfiguration)
        {
            this.openWeatherMapConfiguration = openWeatherMapConfiguration;
        }

        public Task<WeatherInfo> GetCurrentWeatherAsync(double latitude, double longitude)
        {
            return Task.FromResult(new WeatherInfo
            {
                Main = new TemperatureInfo
                {
                    Temperature = new Temperature(-27d, TemperatureUnit.Celsius),
                }
            });
        }

        public Task<Stream> GetWeatherIconAsync(WeatherCondition weatherCondition)
        {
            //return TestImages.WeatherIcon;
            throw new NotImplementedException();
        }
    }
}