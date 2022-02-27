using System;
using System.Linq;
using DisplayService.Services;
using DisplayService.Settings;
using InvoiceScanner.Api.Services.System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherDisplay.Model;
using WeatherDisplay.Services;

namespace WeatherDisplay.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddWeatherDisplay(this IServiceCollection services, IConfiguration configuration)
        {
            // Initialize app settings
            var appSettings = new AppSettings();
            var appSettingsSection = configuration.GetSection("AppSettings");
            appSettingsSection.Bind(appSettings);

            var openWeatherMapConfiguration = new OpenWeatherMapConfiguration();
            var openWeatherMapSection = configuration.GetSection("OpenWeatherMap");
            openWeatherMapSection.Bind(openWeatherMapConfiguration);

            // Initialize display
            IDisplay display;
            if (appSettings.IsDebug)
            {
                display = new NullDisplayService();
            }
            else
            {
                try
                {
                    var displayConfig = appSettings.Displays.First(); // Supports only one display at the time
                    switch (displayConfig.DriverType)
                    {
                        case "WaveShareDisplay":
                            display = new WaveShareDisplay(displayConfig.Driver);
                            break;
                        default:
                            throw new NotSupportedException($"DriverType '{displayConfig.DriverType}' is not supported");
                    }
                }
                catch (Exception)
                {
                    display = new NullDisplayService();
                }
            }

            // TODO: Load from appsettings
            IRenderSettings renderSettings = new RenderSettings
            {
                BackgroundColor = "#FFFFFFFF",
            };
            renderSettings.Resize(display.Width, display.Height);

            // Register services
            services.AddSingleton<IAppSettings>(appSettings);
            services.AddSingleton<ICacheService, CacheService>(); // TODO Move to separate ServiceCollectionExtensions
            services.AddSingleton<IDateTime, SystemDateTime>(); // TODO Move to separate ServiceCollectionExtensions
            services.AddSingleton<ITimerServiceFactory, TimerServiceFactory>(); // TODO Move to separate ServiceCollectionExtensions
            services.AddSingleton<IRenderService, RenderService>(); // TODO Move to separate ServiceCollectionExtensions
            services.AddSingleton(renderSettings);
            services.AddSingleton(display);
            services.AddSingleton<IDisplayManager, DisplayManager>();
            services.AddSingleton<IOpenWeatherMapConfiguration>(openWeatherMapConfiguration);

            if (appSettings.IsDebug)
            {
                services.AddSingleton<IOpenWeatherMapService, NullOpenWeatherMapService>();
            }
            else
            {
                services.AddSingleton<IOpenWeatherMapService, OpenWeatherMapService>();
            }
        }
    }
}