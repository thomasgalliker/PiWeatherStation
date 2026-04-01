using System;
using System.CommandLine;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WeatherDisplay.ConsoleApp.Commands;

namespace WeatherDisplay.ConsoleApp
{
    internal class Program
    {
        private static async Task<int> Main(string[] args)
        {
            Console.WriteLine(
                $"WeatherStation version {typeof(Program).Assembly.GetName().Version} {Environment.NewLine}" +
                $"Copyright(C) superdev GmbH. All rights reserved.{Environment.NewLine}");

            var serviceProvider = BuildServiceProvider();
            var parser = BuildParser(serviceProvider);

            if (args.Length == 0)
            {
                // Use default parameter 'start' in case no parameter is used
                args = new [] { StartCommand.CommandName };
            }

            var result = await parser.Parse(args).InvokeAsync().ConfigureAwait(false);

            if (args.Contains(StartCommand.CommandName))
            {
                Console.ReadLine();
            }

            return result;
        }

        private static RootCommand BuildParser(IServiceProvider serviceProvider)
        {
            var rootCommand = new RootCommand();
            //rootCommand.Description = $"Simplify nuget package administration.";

            rootCommand.Add(ProgramOptions.ClearOption);

            var commands = serviceProvider.GetServices<Command>();
            foreach (var command in commands)
            {
                rootCommand.Add(command);
            }

            return rootCommand;
        }

        private static IServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();

            var dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
            services.AddLogging(o =>
            {
                o.ClearProviders();
                o.SetMinimumLevel(LogLevel.Debug);
                o.AddDebug();
                o.AddSimpleConsole(c =>
                {
                    c.TimestampFormat = $"{dateTimeFormat.ShortDatePattern} {dateTimeFormat.LongTimePattern} ";
                });
            });

            IConfiguration config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();

            services.AddWeatherDisplay(config);

            services.AddSingleton<Command, StartCommand>();
            services.AddSingleton<Command, SilentCommand>();
            services.AddSingleton<Command, ResetCommand>();

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
