using System.Device.Gpio;
using System.Device.Utils;
using System.Runtime.InteropServices;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds registrations for services provided by System.Gpio.Devices.
        /// </summary>
        public static void AddGpioDevices(this IServiceCollection services)
        {
            var osplatform = OperatingSystemHelper.GetOperatingSystem();
            if (osplatform == OSPlatform.Linux)
            {
                services.AddSingleton<IGpioController, GpioControllerWrapper>();
            }
            else if (osplatform == OSPlatform.Windows)
            {
                services.AddSingleton<IGpioController, GpioControllerMock>();
            }
        }
    }
}