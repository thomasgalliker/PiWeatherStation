using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DisplayService.ConsoleApp.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WeatherDisplay.Extensions;

namespace DisplayService.ConsoleApp
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
                args = new string[] { StartCommand.CommandName };
            }

            var result = await parser.InvokeAsync(args).ConfigureAwait(false);

            if (args.Contains(StartCommand.CommandName))
            {
                Console.ReadLine();
            }

            return result;
        }

        private static Parser BuildParser(IServiceProvider serviceProvider)
        {
            var rootCommand = new RootCommand();
            //rootCommand.Description = $"Simplify nuget package administration.";

            rootCommand.AddGlobalOption(ProgramOptions.ClearOption);

            var commandLineBuilder = new CommandLineBuilder(rootCommand);

            var commands = serviceProvider.GetServices<Command>();
            foreach (var command in commands)
            {
                commandLineBuilder.Command.Add(command);
            }

            return commandLineBuilder
                .UseDefaults()
                .Build();
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