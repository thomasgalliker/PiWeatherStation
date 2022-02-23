﻿using System.Threading.Tasks;
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

        public Task<WeatherResponse> GetWeatherInfoAsync(double latitude, double longitude)
        {
            return Task.FromResult(new WeatherResponse
            {
                ConditionId = 1,
                Temperature = -27,
                UnitSystem = this.openWeatherMapConfiguration.UnitSystem,
            });
        }
    }
}