using System;
using System.Device.Gpio;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SampleApp.BMxx80
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var assemblyVersion = Assembly.GetExecutingAssembly()
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                .InformationalVersion;

            Console.WriteLine(
                $"SampleApp.BMxx80 version {assemblyVersion} {Environment.NewLine}" +
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
            serviceCollection.AddGpioDevices();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            if (args.Length == 1)
            {
                switch (args[0])
                {

                    case "bme680":
                        await Bme680Sample.RunAsync();
                        break;


                }
                Console.ReadKey();

                // Retrieve general system information such as CPU sensor data.
                var systemInfoService = serviceProvider.GetRequiredService<IGpioController>();
                //var cpuSensorsStatus = systemInfoService.GetCpuSensorsStatus();
                //Console.WriteLine($"CPU Sensors Status:");
                //Console.WriteLine($"Temperature: {cpuSensorsStatus.Temperature}°C");
                //Console.WriteLine($"Voltage: {cpuSensorsStatus.Voltage}V");
                //Console.WriteLine($"CurrentlyThrottled: {cpuSensorsStatus.CurrentlyThrottled}");
                //Console.WriteLine();

                //var memoryInfo = systemInfoService.GetMemoryInfo();
                //Console.WriteLine($"Memory Info:");
                //Console.WriteLine($"RAM Total: {memoryInfo.RandomAccessMemory.Total / 1024 / 1024} MB");
                //Console.WriteLine($"RAM Used: {memoryInfo.RandomAccessMemory.Used / 1024 / 1024} MB");
                //Console.WriteLine($"RAM Free: {memoryInfo.RandomAccessMemory.Free / 1024 / 1024} MB");
                //Console.WriteLine();


                //Console.ReadKey();
            }
        }
    }
}