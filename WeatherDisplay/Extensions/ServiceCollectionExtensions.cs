using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherDisplay.Model.Settings;
using WeatherDisplay.Pages;
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

            // TODO Try to minimize boiler-plate code with this method
            //services.AddConfigurationBindings(configuration).Bind<AppSettings>("AppSettings");

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
            var openWeatherMapSection = configuration.GetSection("OpenWeatherMap");
            services.AddOpenWeatherMap(openWeatherMapSection);
            services.AddMeteoSwissApi(o =>
            {
                o.Language = appSettings.CultureInfo.TwoLetterISOLanguageName;
                o.SwissMetNet.CacheExpiration = TimeSpan.FromMinutes(20);
            });

            services.AddSingleton<ISpaceWeatherService, SpaceWeatherService>();

            services.AddSingleton<INetworkManager, NetworkManager>();
            services.AddSingleton<ITranslationService, DeepLTranslationService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.RegisterAllPages();

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

            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(ti => ti.IsInterface == false && ti.GetInterfaces().Contains(typeof(T))));
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

        public static void RegisterAllPages(this IServiceCollection services, Assembly[] assemblies = null)
        {
            if (assemblies == null)
            {
                assemblies = new[] { Assembly.GetExecutingAssembly() };
            }

            var pageInterfaceType = typeof(IPage);
            var lifetime = ServiceLifetime.Singleton;

            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes
                .Where(ti => ti.IsInterface == false &&
                             ti.GetInterfaces().Contains(pageInterfaceType)));

            foreach (var pageType in typesFromAssemblies)
            {
                // Register page 'as self'
                services.Add(new ServiceDescriptor(pageType, pageType, lifetime));

                // Register page as IPage interface
                services.Add(new ServiceDescriptor(pageInterfaceType, (IServiceProvider p) => p.GetRequiredService(pageType), lifetime));
            }
        }
    }
}