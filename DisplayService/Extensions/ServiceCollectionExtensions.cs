using System;
using DisplayService;
using DisplayService.Devices;
using DisplayService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using IDateTime = DisplayService.Services.IDateTime;
using SystemDateTime = DisplayService.Services.SystemDateTime;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDisplayService(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            // Configuration
            serviceCollection.Configure<DisplayOptions>(configuration);

            // Register services
            serviceCollection.AddDisplayService();
        }

        public static void AddDisplayService(this IServiceCollection serviceCollection, Action<DisplayOptions> options = null)
        {
            // ====== Configuration ======
            if (options != null)
            {
                serviceCollection.Configure(options);
            }

            // ====== Services ======
            serviceCollection.AddScheduler();

            serviceCollection.AddSingleton<IDisplay>(s =>
            {
                var displayOptions = s.GetRequiredService<IOptions<DisplayOptions>>().Value;

                IDisplay display;
                try
                {
                    switch (displayOptions.DriverType)
                    {
                        case "WaveShareDisplay":
                            var waveShareDisplayLogger = s.GetRequiredService<ILogger<WaveShareDisplay>>();
                            display = new WaveShareDisplay(waveShareDisplayLogger, displayOptions.Driver);
                            break;
                        case "NullDisplay":
                            var nullDisplayLogger = s.GetRequiredService<ILogger<NullDisplay>>();
                            display = new NullDisplay(nullDisplayLogger);
                            break;
                        default:
                            throw new NotSupportedException($"DriverType '{displayOptions.DriverType ?? "null"}' is not supported");
                    }
                }
                catch (Exception ex)
                {
                    var logger = s.GetRequiredService<ILogger<IServiceCollection>>();
                    logger.LogError(ex, $"{nameof(AddDisplayService)} failed with exception while resolving display '{displayOptions.DriverType}'");
                    display = new NullDisplay(s.GetRequiredService<ILogger<NullDisplay>>());
                }

                return display;
            });

            serviceCollection.AddSingleton<IRenderSettings>(s =>
            {
                var displayOptions = s.GetRequiredService<IOptions<DisplayOptions>>().Value;
                var renderSettings = new RenderSettings(displayOptions.Width, displayOptions.Height, displayOptions.Rotation);
                return renderSettings;
            });

            serviceCollection.AddSingleton<IDateTime, SystemDateTime>();
            serviceCollection.AddSingleton<ICacheService, CacheService>();
            serviceCollection.AddSingleton<IRenderService, RenderService>();
            serviceCollection.AddSingleton<IDisplayManager, DisplayManager>();
        }
    }
}