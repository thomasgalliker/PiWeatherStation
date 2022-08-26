﻿using System;
using System.Linq;
using System.Reflection;
using DisplayService.Services;
using DisplayService.Settings;
using MeteoSwissApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NCrontab.Scheduler;
using WeatherDisplay.Compilations;
using WeatherDisplay.Model;
using WeatherDisplay.Services.DeepL;
using WeatherDisplay.Services.OpenWeatherMap;
using WeatherDisplay.Services.Wiewarm;
using IDateTime = DisplayService.Services.IDateTime;
using SystemDateTime = DisplayService.Services.SystemDateTime;

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

            var deepLTranslationConfiguration = new DeepLTranslationConfiguration();
            var deepLTranslationSection = configuration.GetSection("DeepL");
            deepLTranslationSection.Bind(deepLTranslationConfiguration);

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
            services.AddSingleton<IOpenWeatherMapService, OpenWeatherMapService>();
            
            services.AddSingleton<IMeteoSwissWeatherServiceConfiguration, MeteoSwissWeatherServiceConfiguration>();
            services.AddSingleton<IMeteoSwissWeatherService, MeteoSwissWeatherService>();

            services.AddSingleton<IDeepLTranslationConfiguration>(deepLTranslationConfiguration);
            services.AddSingleton<ITranslationService, DeepLTranslationService>();
            services.AddSingleton<IDisplayCompilationService, DisplayCompilationService>();
            services.RegisterAllTypes<IDisplayCompilation>(lifetime: ServiceLifetime.Singleton);

            services.AddSingleton<IWiewarmService, WiewarmService>();

            services.AddSingleton<IScheduler>(x => new Scheduler(x.GetRequiredService<ILogger<Scheduler>>()));
        }

        public static void RegisterAllTypes<T>(this IServiceCollection services, Assembly[] assemblies = null, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            if (assemblies == null)
            {
                assemblies = new[] { Assembly.GetExecutingAssembly() };
            }

            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T))));
            foreach (var type in typesFromAssemblies)
            {
                services.Add(new ServiceDescriptor(typeof(T), type, lifetime));
            }
        }
    }
}