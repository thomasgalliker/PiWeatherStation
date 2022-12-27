using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace WeatherDisplay.Extensions
{
    public class CustomConfigurationBinder
    {
        private readonly IConfiguration configuration;
        private readonly IServiceCollection services;

        public CustomConfigurationBinder(IServiceCollection services, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.services = services;
        }

        public CustomConfigurationBinder Bind<T>(string key) where T : class, new()
        {
            var bind = new T();
            this.configuration.Bind(key, bind);
            this.services.TryAddSingleton(bind);
            return this;
        }
    }
}