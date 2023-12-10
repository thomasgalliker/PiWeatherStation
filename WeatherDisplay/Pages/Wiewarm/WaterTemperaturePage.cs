using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DisplayService.Model;
using DisplayService.Services;
using Microsoft.Extensions.Options;
using NCrontab;
using WeatherDisplay.Resources.Strings;
using WeatherDisplay.Services.Navigation;
using WeatherDisplay.Services.Wiewarm;

namespace WeatherDisplay.Pages.Wiewarm
{
    public class WaterTemperaturePage : IPage, INavigatedTo
    {
        private readonly IDisplayManager displayManager;
        private readonly IWiewarmService wiewarmService;
        private readonly IDateTime dateTime;
        private readonly IOptionsMonitor<WaterTemperaturePageOptions> options;

        public WaterTemperaturePage(
            IDisplayManager displayManager,
            IWiewarmService wiewarmService,
            IDateTime dateTime,
            IOptionsMonitor<WaterTemperaturePageOptions> options)
        {
            this.displayManager = displayManager;
            this.wiewarmService = wiewarmService;
            this.dateTime = dateTime;
            this.options = options;
        }

        public Task OnNavigatedToAsync(INavigationParameters navigationParameters)
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
                            Value = $"{Translations.WaterTemperaturePage_SourceName} / v{fvi.ProductVersion}",
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
                    var places = this.options.CurrentValue.Places;

                    // Search baths by search terms
                    var searchTerms = places.ToArray();

                    var searchTasks = searchTerms.Select(s => this.wiewarmService.SearchBathsAsync(s));
                    var baths = (await Task.WhenAll(searchTasks))
                        .SelectMany(b => b)
                        .ToList();

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
                                Value = $"{bath.Name} ({bath.Canton}), {basin.Name}, um {dateString} Uhr",
                                ForegroundColor = "#000000",
                                BackgroundColor = "#FFFFFF",
                                FontSize = 20,
                            };
                            basinRenderActions.Add(basinRenderAction);

                            var temperatureRenderAction = new RenderActions.Text
                            {
                                X = 780,
                                Y = 120 + verticalOffset,
                                HorizontalTextAlignment = HorizontalAlignment.Right,
                                VerticalTextAlignment = VerticalAlignment.Top,
                                Value = $"{basin.Temperature}",
                                ForegroundColor = "#000000",
                                BackgroundColor = "#FFFFFF",
                                FontSize = 20,
                                Bold = true,
                            };
                            basinRenderActions.Add(temperatureRenderAction);

                            verticalOffset += 40;
                        }
                    }


                    return basinRenderActions;
                },
                CrontabSchedule.Parse("*/15 * * * *")); // Update every 15mins

            return Task.CompletedTask;
        }
    }
}
