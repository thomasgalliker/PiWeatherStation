using System;
using System.Linq;
using System.Reflection;
using MeteoSwissApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenWeatherMap;
using WeatherDisplay.Model.Settings;
using WeatherDisplay.Services;
using WeatherDisplay.Services.Astronomy;
using WeatherDisplay.Services.DeepL;
using WeatherDisplay.Services.Hardware;
using WeatherDisplay.Services.Navigation;
using WeatherDisplay.Services.QR;
using WeatherDisplay.Services.Wiewarm;

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
            
            var accessPointSettings = new AccessPointSettings();
            var accessPointSection = configuration.GetSection("AccessPoint");
            accessPointSection.Bind(accessPointSettings);
            services.AddSingleton<AccessPointSettings>(accessPointSettings);

            // ====== Display ======
            var displayOptionsConfiguration = configuration.GetSection("DisplayOptions");
            services.AddDisplayService(displayOptionsConfiguration);

            // ====== Services ======
            services.AddSingleton<IOpenWeatherMapService, OpenWeatherMapService>();

            services.AddSingleton<IMeteoSwissWeatherServiceOptions, MeteoSwissWeatherServiceOptions>();
            services.AddSingleton<IMeteoSwissWeatherService, MeteoSwissWeatherService>();
            services.AddSingleton<ISwissMetNetServiceOptions, SwissMetNetServiceOptions>();
            services.AddSingleton<ISwissMetNetService, SwissMetNetService>();

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