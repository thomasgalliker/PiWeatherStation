using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WeatherDisplay.Api.Updater.Models;
using WeatherDisplay.Api.Updater.Services;

namespace WeatherDisplay.Api.Updater
{
    internal class Program
    {
        private static async Task<int> Main(string[] args)
        {
            Console.WriteLine(
                $"WeatherDisplay.Api.Updater version {typeof(Program).Assembly.GetName().Version} {Environment.NewLine}" +
                $"Copyright(C) superdev GmbH. All rights reserved.{Environment.NewLine}");

            if (args.Length == 0)
            {
                Console.WriteLine($"Invalid arguments");
                return -1;
            }

            var updateRequestJson = DecodeBase64(args[0]);

            var serviceProvider = BuildServiceProvider();
            var updateExecutorService = serviceProvider.GetRequiredService<IUpdateExecutorService>();

            Console.WriteLine("CommandLine: {0}", Environment.CommandLine);
            Console.WriteLine("args[0]: {0}", updateRequestJson);

            var updateRequestDto = JsonConvert.DeserializeObject<UpdateRequestDto>(updateRequestJson.Replace("'", ""));
            Console.WriteLine("updateRequestDto.DownloadUrl: {0}", updateRequestDto.DownloadUrl);

            var currentDirectory = updateRequestDto.WorkingDirectory;
            Environment.CurrentDirectory = Path.GetFullPath(currentDirectory);

            await updateExecutorService.RunAsync(updateRequestDto);

            return 0;
        }

        private static IServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddLogging(o =>
            {
                o.ClearProviders();
                o.SetMinimumLevel(LogLevel.Debug);
                o.AddDebug();
                o.AddSimpleConsole(c =>
                {
                    //c.TimestampFormat = $"{dateTimeFormat.ShortDatePattern} {dateTimeFormat.LongTimePattern} ";
                });
            });

            services.AddSingleton<IUpdateExecutorService, UpdateExecutorService>();
            services.AddSingleton<IProcessFactory, SystemProcessFactory>();

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }

        public static string DecodeBase64(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
            {
                return string.Empty;
            }

            var valueBytes = Convert.FromBase64String(base64String);
            return Encoding.UTF8.GetString(valueBytes);
        }
    }
}