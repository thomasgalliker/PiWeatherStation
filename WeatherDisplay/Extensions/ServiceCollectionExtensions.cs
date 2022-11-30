using System;
using System.Linq;
using System.Reflection;
using DisplayService.Services;
using DisplayService.Settings;
using MeteoSwissApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NCrontab.Scheduler;
using OpenWeatherMap;
using WeatherDisplay.Compilations;
using WeatherDisplay.Model;
using WeatherDisplay.Model.MeteoSwiss;
using WeatherDisplay.Services.DeepL;
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
            services.AddSingleton<IAppSettings>(appSettings); // TODO: Remove IAppSettings and make properties virtual
            services.Configure<AppSettings>(appSettingsSection);

            // TODO Try to minimize boiler-plate code with this method
            //services.AddConfigurationBindings(configuration).Bind<AppSettings>("AppSettings");

            var openWeatherMapConfiguration = new OpenWeatherMapConfiguration();
            var openWeatherMapSection = configuration.GetSection("OpenWeatherMap");
            openWeatherMapSection.Bind(openWeatherMapConfiguration);
            services.AddSingleton<IOpenWeatherMapConfiguration>(openWeatherMapConfiguration);

            var deepLTranslationConfiguration = new DeepLTranslationConfiguration();
            var deepLTranslationSection = configuration.GetSection("DeepL");
            deepLTranslationSection.Bind(deepLTranslationConfiguration);
            services.AddSingleton<IDeepLTranslationConfiguration>(deepLTranslationConfiguration);

            //var meteoSwissWeatherDisplayCompilationOptions = new MeteoSwissWeatherDisplayCompilationOptions();
            //var meteoSwissWeatherDisplayCompilationSection = configuration.GetSection("MeteoSwissWeatherDisplayCompilation");
            //meteoSwissWeatherDisplayCompilationSection.Bind(meteoSwissWeatherDisplayCompilationOptions);
            //services.AddSingleton(meteoSwissWeatherDisplayCompilationOptions);

            //var openWeatherDisplayCompilationOptions = new OpenWeatherDisplayCompilationOptions();
            //var openWeatherDisplayCompilationSection = configuration.GetSection("OpenWeatherDisplayCompilation");
            //openWeatherDisplayCompilationSection.Bind(openWeatherDisplayCompilationOptions);
            //services.AddSingleton(openWeatherDisplayCompilationOptions);

            //var temperatureWeatherDisplayCompilationOptions = new TemperatureWeatherDisplayCompilationOptions();
            //var temperatureWeatherDisplayCompilationSection = configuration.GetSection("TemperatureWeatherDisplayCompilation");
            //temperatureWeatherDisplayCompilationSection.Bind(temperatureWeatherDisplayCompilationOptions);
            //services.AddSingleton(temperatureWeatherDisplayCompilationOptions);

            //var waterTemperatureDisplayCompilationOptions = new WaterTemperatureDisplayCompilationOptions();
            //var waterTemperatureDisplayCompilationSection = configuration.GetSection("WaterTemperatureDisplayCompilation");
            //waterTemperatureDisplayCompilationSection.Bind(waterTemperatureDisplayCompilationOptions);
            //services.AddSingleton(waterTemperatureDisplayCompilationOptions);

            var displayConfig = appSettings.Displays.First(); // Supports only one display at the time

            // Initialize display
            services.AddSingleton(x =>
            {
                IDisplay display;
                if (appSettings.IsDebug)
                {
                    display = new NullDisplayService(x.GetRequiredService<ILogger<NullDisplayService>>());
                }
                else
                {
                    try
                    {
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
                        display = new NullDisplayService(x.GetRequiredService<ILogger<NullDisplayService>>());
                    }
                }

                return display;
            });

            // TODO: Load from appsettings
            IRenderSettings renderSettings = new RenderSettings
            {
                BackgroundColor = "#FFFFFFFF",
            };
            renderSettings.Resize(displayConfig.Width, displayConfig.Height); // TODO: Refactor this

            // Register services
            services.AddSingleton<ICacheService, CacheService>(); // TODO Move to separate ServiceCollectionExtensions
            services.AddSingleton<IDateTime, SystemDateTime>(); // TODO Move to separate ServiceCollectionExtensions
            services.AddSingleton<ITimerServiceFactory, TimerServiceFactory>(); // TODO Move to separate ServiceCollectionExtensions
            services.AddSingleton<IRenderService, RenderService>(); // TODO Move to separate ServiceCollectionExtensions
            services.AddSingleton(renderSettings);
            services.AddSingleton<IDisplayManager, DisplayManager>();
            services.AddSingleton<IOpenWeatherMapService, OpenWeatherMapService>();

            services.AddSingleton<IMeteoSwissWeatherServiceConfiguration, MeteoSwissWeatherServiceConfiguration>();
            services.AddSingleton<IMeteoSwissWeatherService, MeteoSwissWeatherService>();

            services.AddSingleton<ITranslationService, DeepLTranslationService>();
            services.AddSingleton<IDisplayCompilationService, DisplayCompilationService>();
            services.RegisterAllTypesAsSelf<IDisplayCompilation>(lifetime: ServiceLifetime.Singleton);

            services.AddSingleton<IWiewarmService, WiewarmService>();

            services.AddSingleton<IScheduler>(x => new Scheduler(x.GetRequiredService<ILogger<Scheduler>>()));
        }

        public static CustomConfigurationBinder AddConfigurationBindings(this IServiceCollection services, IConfiguration configuration)
        {
            return new CustomConfigurationBinder(services, configuration);
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

        public static void RegisterAllTypesAsSelf<T>(this IServiceCollection services, Assembly[] assemblies = null, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            if (assemblies == null)
            {
                assemblies = new[] { Assembly.GetExecutingAssembly() };
            }

            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T))));
            foreach (var type in typesFromAssemblies)
            {
                services.Add(new ServiceDescriptor(type, type, lifetime));
            }
        }
    }
}