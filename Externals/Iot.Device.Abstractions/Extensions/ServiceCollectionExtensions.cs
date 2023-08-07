using System.Runtime.InteropServices;
using Iot.Device.Bmxx80;
using Iot.Device.Scd4x;
using Iot.Device.Utils;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds registrations for services provided by System.Gpio.Devices.
        /// </summary>
        public static void AddIotDevices(this IServiceCollection services)
        {
            var osplatform = OperatingSystemHelper.GetOperatingSystem();
            if (osplatform == OSPlatform.Linux)
            {
                services.AddSingleton<IBme680Factory, Bme680Factory>();
                services.AddSingleton<IScd4xFactory, Scd4xFactory>();
            }
            else if (osplatform == OSPlatform.Windows)
            {
                services.AddSingleton<IBme680Factory, Bme680FactoryMock>();
                services.AddSingleton<IScd4xFactory, Scd4xFactoryMock>();
            }
        }
    }
}