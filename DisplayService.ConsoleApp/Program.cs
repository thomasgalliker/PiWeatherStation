using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DisplayService.ConsoleApp.Commands;
using DisplayService.ConsoleApp.Model;
using DisplayService.ConsoleApp.Services;
using DisplayService.Services;
using DisplayService.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DisplayService.ConsoleApp
{
    class Program
    {
        static async Task<int> Main(string[] args)
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

            var appSettings = new AppSettings();
            var appSettingsSection = config.GetSection("AppSettings");
            appSettingsSection.Bind(appSettings);

            CultureInfo.CurrentCulture = appSettings.CultureInfo;
            CultureInfo.CurrentUICulture = appSettings.CultureInfo;

            var openWeatherMapConfiguration = new OpenWeatherMapConfiguration();
            var openWeatherMapSection = config.GetSection("OpenWeatherMap");
            openWeatherMapSection.Bind(openWeatherMapConfiguration);

            IDisplay display;
            if (appSettings.IsDebug)
            {
                display = new NullDisplayService();
            }
            else
            {
                try
                {
                    var displayConfig = appSettings.Displays.First(); // Supports only one display at the time
                    switch (displayConfig.DriverType)
                    {
                        case "WaveShareDisplay":
                            display = new WaveShareDisplay(displayConfig.Driver);
                            break;
                        default:
                            throw new NotSupportedException($"DriverType '{displayConfig.DriverType}' is not supported");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Failed to initialize display");
                    display = new NullDisplayService();
                }
            }

            // TODO: Load from appsettings
            IRenderSettings renderSettings = new RenderSettings
            {
                BackgroundColor = "#FFFFFFFF",
            };
            renderSettings.Resize(display.Width, display.Height);

            services.AddSingleton<Command, StartCommand>();
            services.AddSingleton<Command, SilentCommand>();
            services.AddSingleton<Command, ClearCommand>();

            services.AddSingleton<IAppSettings>(appSettings);
            services.AddSingleton<IRenderService, RenderService>();
            services.AddSingleton<IRenderSettings>(renderSettings);
            services.AddSingleton<IDisplay>(display);
            services.AddSingleton<IDisplayManager, DisplayManager>();
            services.AddSingleton<IOpenWeatherMapConfiguration>(openWeatherMapConfiguration);

            if (appSettings.IsDebug)
            {
                services.AddSingleton<IOpenWeatherMapService, NullOpenWeatherMapService>();
            }
            else
            {
                services.AddSingleton<IOpenWeatherMapService, OpenWeatherMapService>();
            }

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}