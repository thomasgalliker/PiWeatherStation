using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;
using System.Threading.Tasks;
using DisplayService.ConsoleApp.Model;
using DisplayService.ConsoleApp.Services;
using DisplayService.Model;
using DisplayService.Services;

namespace DisplayService.ConsoleApp.Commands
{
    public class StartCommand : Command
    {
        public const string CommandName = "start";

        public StartCommand(IDisplayManager displayManager, IOpenWeatherMapService openWeatherMapService, IAppSettings appSettings) : base(CommandName, "Starts the scheduled rendering process")
        {
            this.Handler = new StartCommandHandler(displayManager);


            displayManager.AddRenderActions(
                () => new List<IRenderAction>
                {
                    new RenderActions.Rectangle
                    {
                        X = 0,
                        Y = 0,
                        Height = 90,
                        Width = 800,
                        BackgroundColor = "#999999",
                    },
                    new RenderActions.Text
                    {
                        X = 20,
                        Y = 20,
                        HorizontalTextAlignment = HorizontalAlignment.Left,
                        VerticalTextAlignment = VerticalAlignment.Top,
                        Value = appSettings.Title,
                        ForegroundColor = "#191919",
                        FontSize = 70,
                        Bold = true,
                    },
                    new RenderActions.Rectangle
                    {
                        X = 0,
                        Y = 440,
                        Height = 40,
                        Width = 800,
                        BackgroundColor = "#999999",
                    }
                });

            displayManager.AddRenderActions(
                () =>
                {
                    var dateTimeNow = DateTime.Now;
                    return new List<IRenderAction>
                    {
                        new RenderActions.Rectangle
                        {
                            X = 780,
                            Y = 19,
                            Height = 25,
                            Width = 300,
                            VerticalAlignment = VerticalAlignment.Top,
                            HorizontalAlignment = HorizontalAlignment.Right,
                            BackgroundColor = "#999999",
                        },
                        new RenderActions.Text
                        {
                            X = 780,
                            Y = 20,
                            HorizontalTextAlignment = HorizontalAlignment.Right,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"{dateTimeNow:dddd}, {dateTimeNow:M}",
                            ForegroundColor = "#999999",
                            BackgroundColor = "#191919",
                            FontSize = 22,
                        }
                    };
                },
                TimeSpan.FromDays(1)); // TODO: Use crontab-like scheduling

            displayManager.AddRenderActions(
                () =>
                {
                    var dateTimeNow = DateTime.Now;
                    return new List<IRenderAction>
                    {
                        new RenderActions.Rectangle
                        {
                            X = 780,
                            Y = 19,
                            Height = 25,
                            Width = 300,
                            VerticalAlignment = VerticalAlignment.Top,
                            HorizontalAlignment = HorizontalAlignment.Right,
                            BackgroundColor = "#999999",
                        },
                        new RenderActions.Text
                        {
                            X = 780,
                            Y = 50,
                            HorizontalTextAlignment = HorizontalAlignment.Right,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"{dateTimeNow:t}",
                            ForegroundColor = "#999999",
                            BackgroundColor = "#191919",
                            FontSize = 22,
                        }
                    };
                },
                TimeSpan.FromMinutes(1));

            displayManager.AddRenderActionsAsync(
                async () =>
                {
                    var place = appSettings.Places.First();
                    var weatherResponse = await openWeatherMapService.GetWeatherInfoAsync(place.Latitude, place.Longitude);

                    return new List<IRenderAction>
                    {
                        new RenderActions.Text
                        {
                            X = 400,
                            Y = 200,
                            HorizontalTextAlignment = HorizontalAlignment.Center,
                            VerticalTextAlignment = VerticalAlignment.Center,
                            Value = $"{place.Name ?? weatherResponse.LocationName}",
                            ForegroundColor = "#191919",
                            BackgroundColor = "#00FFFFFF",
                            FontSize = 20,
                        },
                        new RenderActions.Text
                        {
                            X = 400,
                            Y = 240,
                            HorizontalTextAlignment = HorizontalAlignment.Center,
                            VerticalTextAlignment = VerticalAlignment.Center,
                            Value = FormatTemperature(weatherResponse),
                            ForegroundColor = "#191919",
                            BackgroundColor = "#00FFFFFF",
                            FontSize = 80,
                        }
                    };
                },
                TimeSpan.FromHours(1));
        }

        private static string FormatTemperature(WeatherResponse weatherResponse)
        {
            switch (weatherResponse.UnitSystem)
            {
                case "metric":
                    return $"{weatherResponse.Temperature:F1}°C";
                case "imperial":
                    return $"{weatherResponse.Temperature:F1}°F";
                default:
                    throw new NotSupportedException($"Unit system {weatherResponse.UnitSystem} not supported");
            }
        }

        private class StartCommandHandler : ICommandHandler
        {
            private readonly IDisplayManager displayManager;

            public StartCommandHandler(IDisplayManager displayManager)
            {
                this.displayManager = displayManager;
            }

            public async Task<int> InvokeAsync(InvocationContext context)
            {
                await this.displayManager.StartAsync();

                return 0;
            }
        }
    }
}