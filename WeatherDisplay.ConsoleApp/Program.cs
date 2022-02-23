using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.Linq;
using System.Threading.Tasks;
using DisplayService.ConsoleApp.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WeatherDisplay;

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

            services.AddLogging(o =>
            {
                o.ClearProviders();
                o.SetMinimumLevel(LogLevel.Debug);
                o.AddDebug();
                o.AddSimpleConsole(o =>
                {
                    o.SingleLine = true;
                    o.TimestampFormat = "hh:mm:ss ";
                });
            });

            IConfiguration config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();

            services.AddWeatherDisplay(config);

            services.AddSingleton<Command, StartCommand>();
            services.AddSingleton<Command, SilentCommand>();
            services.AddSingleton<Command, ClearCommand>();

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}