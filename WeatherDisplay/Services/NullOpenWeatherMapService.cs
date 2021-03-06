using System;
using System.IO;
using System.Threading.Tasks;
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

        public Task<WeatherForecast> GetWeatherForecastAsync(double latitude, double longitude)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> GetWeatherIconAsync(WeatherCondition weatherCondition, IWeatherIconMapping weatherIconMapping = null)
        {
            //return TestImages.WeatherIcon;
            throw new NotImplementedException();
        }

        public Task<OneCallWeatherInfo> GetWeatherOneCallAsync(double latitude, double longitude, OneCallOptions oneCallOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<AirPollutionInfo> GetAirPollutionAsync(double latitude, double longitude)
        {
            throw new NotImplementedException();
        }
    }
}