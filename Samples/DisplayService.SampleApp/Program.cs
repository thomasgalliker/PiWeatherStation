using System;
using System.Device.Gpio;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using DisplayService.Devices;
using DisplayService.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DisplayService.SampleApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var assemblyVersion = Assembly.GetExecutingAssembly()
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                .InformationalVersion;

            Console.WriteLine(
                $"DisplayService.SampleApp version {assemblyVersion} {Environment.NewLine}" +
                $"Copyright(C) superdev GmbH. All rights reserved.{Environment.NewLine}");

            // This sample console app runs with Microsoft.Extensions.DependencyInjection.
            // However, you can also manually construct the dependency trees if you wish so.
            var serviceCollection = new ServiceCollection();

            var dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
            serviceCollection.AddLogging(o =>
            {
                o.ClearProviders();
                o.SetMinimumLevel(LogLevel.Debug);
                o.AddSimpleConsole(c =>
                {
                    c.TimestampFormat = $"{dateTimeFormat.ShortDatePattern} {dateTimeFormat.LongTimePattern} ";
                });
            });
            serviceCollection.AddDisplayService();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var display = serviceProvider.GetRequiredService<IDisplay>();
            Console.WriteLine($"Display:");
            Console.WriteLine($"Type: {display.GetType().Name}");
            Console.WriteLine($"Resolution: {display.Width} x {display.Height}");

            //var displayManager = serviceProvider.GetRequiredService<IDisplayManager>();
            //await displayManager.StartAsync();

            Console.ReadKey();
        }
    }
}