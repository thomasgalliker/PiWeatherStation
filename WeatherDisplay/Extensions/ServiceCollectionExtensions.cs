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
using WeatherDisplay.Model;
using WeatherDisplay.Services;
using WeatherDisplay.Services.Astronomy;
using WeatherDisplay.Services.DeepL;
using WeatherDisplay.Services.Hardware;
using WeatherDisplay.Services.Navigation;
using WeatherDisplay.Services.QR;
using WeatherDisplay.Services.Wiewarm;
using IDateTime = DisplayService.Services.IDateTime;
using SystemDateTime = DisplayService.Services.SystemDateTime;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddWeatherDisplay(this IServiceCollection services, IConfiguration configuration)
        {
            // ====== App settings ======
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

            var displayConfig = appSettings.Displays.First(); // Supports only one display at the time

            // ====== Display ======
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

            // ====== Services ======
            services.AddSingleton<ICacheService, CacheService>(); // TODO Move to separate ServiceCollectionExtensions
            services.AddSingleton<IDateTime, SystemDateTime>(); // TODO Move to separate ServiceCollectionExtensions
            services.AddSingleton<IRenderService, RenderService>(); // TODO Move to separate ServiceCollectionExtensions
            services.AddSingleton(renderSettings);
            services.AddSingleton<IDisplayManager, DisplayManager>();
            services.AddSingleton<IOpenWeatherMapService, OpenWeatherMapService>();

            services.AddSingleton<IMeteoSwissWeatherServiceConfiguration, MeteoSwissWeatherServiceConfiguration>();
            services.AddSingleton<IMeteoSwissWeatherService, MeteoSwissWeatherService>();

            services.AddSingleton<ISpaceWeatherService, SpaceWeatherService>();

            services.AddSingleton<INetworkManager, NetworkManager>();
            services.AddSingleton<ITranslationService, DeepLTranslationService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.RegisterAllTypesAsSelf<INavigatedTo>(lifetime: ServiceLifetime.Singleton);

            services.AddSingleton<IWiewarmService, WiewarmService>();
            services.AddSingleton<IQRCodeService, QRCodeService>();

            services.AddScheduler();

            // ====== Hardware access ======
            services.AddGpioDevices();
            services.AddIotDevices();
            services.AddSingleton<IButtonsAccessService, ButtonsAccessService>();
            services.AddSingleton<ISensorAccessService, SensorAccessService>();
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