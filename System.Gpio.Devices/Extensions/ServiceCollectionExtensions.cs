using System.Device.Devices;
using System.Device.Gpio;
using System.Device.I2c;
using System.Gpio.Devices.Utils;
using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;

namespace System.Gpio.Devices.Extensions
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
                services.AddSingleton<ISensorFactory, SensorFactory>();
                services.AddSingleton<IGpioController, GpioControllerWrapper>();
            }
            //#if DEBUG
            else if (osplatform == OSPlatform.Windows)
            {
                services.AddSingleton<ISensorFactory, SensorFactoryMock>();
                services.AddSingleton<IGpioController, GpioControllerMock>();
            }
            //#endif
        }
    }
}