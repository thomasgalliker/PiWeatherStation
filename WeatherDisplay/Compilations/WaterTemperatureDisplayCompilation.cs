﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DisplayService.Model;
using DisplayService.Services;
using NCrontab;
using WeatherDisplay.Model;
using WeatherDisplay.Model.OpenWeatherMap;
using WeatherDisplay.Services.OpenWeatherMap;
using WeatherDisplay.Services.Wiewarm;

namespace WeatherDisplay.Compilations
{
    public class WaterTemperatureDisplayCompilation : IDisplayCompilation
    {
        private readonly IDisplayManager displayManager;
        private readonly IWiewarmService wiewarmService;
        private readonly IDateTime dateTime;
        private readonly IAppSettings appSettings;

        public WaterTemperatureDisplayCompilation(
            IDisplayManager displayManager,
            IWiewarmService wiewarmService,
            IDateTime dateTime,
            IAppSettings appSettings)
        {
            this.displayManager = displayManager;
            this.wiewarmService = wiewarmService;
            this.dateTime = dateTime;
            this.appSettings = appSettings;
        }

        public string Name => "WaterTemperatureDisplayCompilation";

        public void AddRenderActions()
        {
            // Date header
            this.displayManager.AddRenderActions(
                () =>
                {
                    var assembly = Assembly.GetExecutingAssembly();
                    var fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);

                    return new List<IRenderAction>
                    {
                        new RenderActions.Rectangle
                        {
                            X = 0,
                            Y = 0,
                            Height = 100,
                            Width = 800,
                            BackgroundColor = "#000000",
                        },
                        new RenderActions.Text
                        {
                            X = 20,
                            Y = 50,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Center,
                            Value = this.dateTime.Now.ToString("dddd, d. MMMM"),
                            ForegroundColor = "#FFFFFF",
                            FontSize = 70,
                            AdjustsFontSizeToFitWidth = true,
                            AdjustsFontSizeToFitHeight = true,
                            Bold = true,
                        },
                        
                        // Version
                        new RenderActions.Text
                        {
                            X = 798,
                            Y = 88,
                            HorizontalTextAlignment = HorizontalAlignment.Right,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"v{fvi.ProductVersion}",
                            ForegroundColor = "#FFFFFF",
                            BackgroundColor = "#000000",
                            FontSize = 12,
                            Bold = false,
                        },
                    };
                },
                CrontabSchedule.Parse("0 0 * * *")); // Update every day at 00:00


            // Current weather info
            this.displayManager.AddRenderActionsAsync(
                async () =>
                {
                    var place = this.appSettings.Places.First();

                    // Get current weather & daily forecasts
                    var oneCallOptions = new OneCallOptions
                    {
                        IncludeCurrentWeather = true,
                        IncludeDailyForecasts = true,
                        IncludeMinutelyForecasts = true,
                        IncludeHourlyForecasts = true,
                    };

                    var searchTerms = new[] { "Bern", "Zug", "Luzern" };

                    var searchTasks = searchTerms.Select(s => this.wiewarmService.SearchBathsAsync(s));
                    var baths = (await Task.WhenAll(searchTasks))
                        .SelectMany(b => b)
                        .ToList();

                    //var bath = await this.wiewarmService.GetBathByIdAsync(17);

                    var dateTimeNow = this.dateTime.Now;

                    var basinRenderActions = new List<IRenderAction>();

                    var verticalOffset = 0;

                    foreach (var bath in baths)
                    {
                        foreach (var basin in bath.Basins.Where(b => b.Date.Date == dateTimeNow.Date))
                        {
                            var dateString = basin.Date.Date == dateTimeNow.Date
                                ? $"{basin.Date:t}"
                                : $"{basin.Date:d} {basin.Date:t}";
                            var basinRenderAction = new RenderActions.Text
                            {
                                X = 20,
                                Y = 120 + verticalOffset,
                                HorizontalTextAlignment = HorizontalAlignment.Left,
                                VerticalTextAlignment = VerticalAlignment.Top,
                                Value = $"{bath.Name} ({bath.Canton}), {basin.Name}, um {dateString} Uhr --> {basin.Temp}°C",
                                ForegroundColor = "#000000",
                                BackgroundColor = "#FFFFFF",
                                FontSize = 20,
                            };
                            basinRenderActions.Add(basinRenderAction);

                            verticalOffset += 40;
                        }
                    }


                    return basinRenderActions;
                },
                CrontabSchedule.Parse("*/15 * * * *")); // Update every 15mins
        }

        private static (Temperature Min, Temperature Max) TemperatureRangeSelector(IEnumerable<TemperatureSet> s)
        {
            return (s.Min(x => x.Min) - 1, s.Max(x => x.Max) + 1);
        }
    }
}
