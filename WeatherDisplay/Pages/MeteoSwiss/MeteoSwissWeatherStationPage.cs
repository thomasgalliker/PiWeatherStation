using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DisplayService.Model;
using DisplayService.Services;
using MeteoSwissApi;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NCrontab;
using OpenWeatherMap.Resources.Icons;
using UnitsNet;
using WeatherDisplay.Model.Settings;
using WeatherDisplay.Resources;
using WeatherDisplay.Resources.Strings;
using WeatherDisplay.Services.Navigation;
using WeatherDisplay.Utils;
using Place = WeatherDisplay.Pages.MeteoSwiss.MeteoSwissPlace;

namespace WeatherDisplay.Pages.MeteoSwiss
{
    public class MeteoSwissWeatherStationPage : INavigatedTo, INavigatedFrom
    {
        private readonly ILogger logger;
        private readonly IDisplayManager displayManager;
        private readonly ISwissMetNetService swissMetNetService;
        private readonly IDateTime dateTime;
        private readonly IAppSettings appSettings;
        private readonly IOptionsMonitor<MeteoSwissWeatherPageOptions> options;
        private IReadOnlyCollection<Place> places;

        public MeteoSwissWeatherStationPage(
            ILogger<MeteoSwissWeatherStationPage> logger,
            IDisplayManager displayManager,
            ISwissMetNetService swissMetNetService,
            IDateTime dateTime,
            IAppSettings appSettings,
            IOptionsMonitor<MeteoSwissWeatherPageOptions> options)
        {
            this.logger = logger;
            this.displayManager = displayManager;
            this.swissMetNetService = swissMetNetService;
            this.dateTime = dateTime;
            this.appSettings = appSettings;
            this.options = options;
        }

        [Obsolete]
        public Task OnNavigatedToAsync(INavigationParameters navigationParameters)
        {
            this.places = this.options.CurrentValue.Places.ToList();

            if (this.places == null || !this.places.Any())
            {
                throw new Exception(Translations.MeteoSwissWeatherPage_ErrorMissingPlacesConfiguration);
            }

            // Date header
            this.displayManager.AddRenderActionsAsync(
                async () =>
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
                            Value = $"{Translations.MeteoSwissWeatherPage_SourceName} / v{fvi.ProductVersion}",
                            ForegroundColor = "#FFFFFF",
                            BackgroundColor = "#000000",
                            FontSize = 12,
                            Bold = false,
                        },
                    };
                },
                CrontabSchedule.Parse("0 0 * * *")); // Update every day at 00:00


            // Current weather station infos
            this.displayManager.AddRenderActionsAsync(
                async () =>
                {
                    var dateTimeNow = this.dateTime.Now;

                    var renderActions = new List<IRenderAction>
                    {

                    };

                    var dividerLineHeight = 4;
                    var yOffsetStart = 100;
                    var totalHeight = 480 - yOffsetStart;
                    var spacing = 20;
                    var rowHeight = 24;
                    var rowSpacing = 5;
                    var heightPerWeatherStation = spacing + 82 + spacing + dividerLineHeight;
                    var numberOfPlaces = (int)(double)totalHeight / (heightPerWeatherStation);

                    var places = this.places.Take(numberOfPlaces).ToList();
                    var numberOfWeatherStations = places.Count;
                    var yOffset = yOffsetStart + spacing;

                    foreach (var place in places)
                    {
                        var stationCode = place.WeatherStation;
                        var weatherStation = await this.swissMetNetService.GetWeatherStationAsync(stationCode, cacheExpiration: TimeSpan.FromMinutes(20));
                        var latestMeasurement = await this.swissMetNetService.GetLatestMeasurementAsync(stationCode, cacheExpiration: TimeSpan.FromMinutes(20));

                        renderActions.AddRange(new IRenderAction[]
                        {
                            // Station image
                            new RenderActions.SvgImage
                            {
                                X = 20,
                                Y = yOffset,
                                Image = Images.GetWeatherStation2(),
                                Width = 64,
                                Height = 64,
                                HorizontalAlignment = HorizontalAlignment.Left,
                                VerticalAlignment = VerticalAlignment.Top,
                            },
                            // Station code
                            new RenderActions.Text
                            {
                                X = 20 + 32,
                                Y = yOffset + 3 * (rowHeight + rowSpacing) - 10,
                                HorizontalTextAlignment = HorizontalAlignment.Center,
                                VerticalTextAlignment = VerticalAlignment.Bottom,
                                Value = weatherStation.StationCode,
                                ForegroundColor = "#000000",
                                BackgroundColor = "#FFFFFF",
                                FontSize = 30,
                                Bold = false,
                            },
                            // Place name + altitude + measurement time
                            new RenderActions.BitmapImage
                            {
                                X = spacing + 64 + spacing,
                                Y = yOffset,
                                Image = Icons.Location(),
                                Width = 24,
                                Height = 24,
                                HorizontalAlignment = HorizontalAlignment.Left,
                                VerticalAlignment = VerticalAlignment.Top,
                            },
                            new RenderActions.Text
                            {
                                X = spacing + 64 + spacing + 40,
                                Y = yOffset + 5,
                                HorizontalTextAlignment = HorizontalAlignment.Left,
                                VerticalTextAlignment = VerticalAlignment.Top,
                                Value = string.Format(Translations.PlaceAndDateTime,
                                    $"{weatherStation.Place}" +
                                    $"{(weatherStation.Location.Altitude is Length altitude  ? $", {altitude} {Translations.AboveSeaLevelAbbreviationLabelText}" : "")}",
                                    $"{latestMeasurement.Date.ToLocalTime():t}"),
                                ForegroundColor = "#000000",
                                BackgroundColor = "#FFFFFF",
                                FontSize = 20,
                                Bold = false,
                            },
                            // Temperature
                            new RenderActions.BitmapImage
                            {
                                X = spacing + 64 + spacing,
                                Y = yOffset + 1 * (rowHeight + rowSpacing),
                                Image = Icons.Temperature(),
                                Width = 24,
                                Height = 24,
                                HorizontalAlignment = HorizontalAlignment.Left,
                                VerticalAlignment = VerticalAlignment.Top,
                            },
                            new RenderActions.Text
                            {
                                X = spacing + 64 + spacing + 40,
                                Y = yOffset + 5 + 1 * (rowHeight + rowSpacing),
                                HorizontalTextAlignment = HorizontalAlignment.Left,
                                VerticalTextAlignment = VerticalAlignment.Top,
                                Value = MeteoFormatter.FormatTemperature(latestMeasurement.AirTemperature, decimalPlaces: 1),
                                ForegroundColor = "#000000",
                                BackgroundColor = "#FFFFFF",
                                FontSize = 20,
                            },
                            // Humidity
                            new RenderActions.BitmapImage
                            {
                                X = spacing + 64 + spacing,
                                Y = yOffset + 2 * (rowHeight + rowSpacing),
                                Image = Icons.Humidity(),
                                Width = 24,
                                Height = 24,
                                HorizontalAlignment = HorizontalAlignment.Left,
                                VerticalAlignment = VerticalAlignment.Top,
                            },
                            new RenderActions.Text
                            {
                                X = spacing + 64 + spacing + 40,
                                Y = yOffset + 5 + 2 * (rowHeight + rowSpacing),
                                HorizontalTextAlignment = HorizontalAlignment.Left,
                                VerticalTextAlignment = VerticalAlignment.Top,
                                Value = MeteoFormatter.FormatHumidity(latestMeasurement.RelativeAirHumidity, decimalPlaces: 1),
                                ForegroundColor = "#000000",
                                BackgroundColor = "#FFFFFF",
                                FontSize = 20,
                            },
                            // Sunshine duration
                            new RenderActions.BitmapImage
                            {
                                X = spacing + 64 + spacing + 200,
                                Y = yOffset + 1 * (rowHeight + rowSpacing),
                                Image = Icons.Sun(),
                                Width = 24,
                                Height = 24,
                                HorizontalAlignment = HorizontalAlignment.Left,
                                VerticalAlignment = VerticalAlignment.Top,
                            },
                            new RenderActions.Text
                            {
                                X = spacing + 64 + spacing + 200 + 40,
                                Y = yOffset + 5 + 1 * (rowHeight + rowSpacing),
                                HorizontalTextAlignment = HorizontalAlignment.Left,
                                VerticalTextAlignment = VerticalAlignment.Top,
                                Value = $"{(latestMeasurement.SunshineDuration is Duration duration ? $"{duration.Value / 10 * 100}%" : "-")}",
                                ForegroundColor = "#000000",
                                BackgroundColor = "#FFFFFF",
                                FontSize = 20,
                            },
                            // Rain
                            new RenderActions.BitmapImage
                            {
                                X = spacing + 64 + spacing + 200,
                                Y = yOffset + 2 * (rowHeight + rowSpacing),
                                Image = Icons.Rain(),
                                Width = 24,
                                Height = 24,
                                HorizontalAlignment = HorizontalAlignment.Left,
                                VerticalAlignment = VerticalAlignment.Top,
                            },
                            new RenderActions.Text
                            {
                                X = spacing + 64 + spacing + 200 + 40,
                                Y = yOffset + 5 + 2 * (rowHeight + rowSpacing),
                                HorizontalTextAlignment = HorizontalAlignment.Left,
                                VerticalTextAlignment = VerticalAlignment.Top,
                                Value = MeteoFormatter.FormatPrecipitation(latestMeasurement.Precipitation),
                                ForegroundColor = "#000000",
                                BackgroundColor = "#FFFFFF",
                                FontSize = 20,
                            },
                            // Pressure
                            new RenderActions.BitmapImage
                            {
                                X = spacing + 64 + spacing + 380,
                                Y = yOffset + 1 * (rowHeight + rowSpacing),
                                Image = Icons.AtmosphericPressure(),
                                Width = 24,
                                Height = 24,
                                HorizontalAlignment = HorizontalAlignment.Left,
                                VerticalAlignment = VerticalAlignment.Top,
                            },
                            new RenderActions.Text
                            {
                                X = spacing + 64 + spacing + 380 + 40,
                                Y = yOffset + 5 + 1 * (rowHeight + rowSpacing),
                                HorizontalTextAlignment = HorizontalAlignment.Left,
                                VerticalTextAlignment = VerticalAlignment.Top,
                                Value = MeteoFormatter.FormatPressure(latestMeasurement.PressureQFE),
                                ForegroundColor = "#000000",
                                BackgroundColor = "#FFFFFF",
                                FontSize = 20,
                            },
                            // Wind
                            new RenderActions.BitmapImage
                            {
                                X = spacing + 64 + spacing + 380,
                                Y = yOffset + 2 * (rowHeight + rowSpacing),
                                Image = Icons.Wind(),
                                Width = 24,
                                Height = 24,
                                HorizontalAlignment = HorizontalAlignment.Left,
                                VerticalAlignment = VerticalAlignment.Top,
                            },
                            new RenderActions.Text
                            {
                                X = spacing + 64 + spacing + 380 + 40,
                                Y = yOffset + 5 + 2 * (rowHeight + rowSpacing),
                                HorizontalTextAlignment = HorizontalAlignment.Left,
                                VerticalTextAlignment = VerticalAlignment.Top,
                                Value = MeteoFormatter.FormatWind(latestMeasurement.WindSpeed, latestMeasurement.WindDirection),
                                ForegroundColor = "#000000",
                                BackgroundColor = "#FFFFFF",
                                FontSize = 20,
                            },
                        });

                        renderActions.AddRange(new[]
                        {
                            // Divider line between weather stations
                            new RenderActions.Rectangle
                            {
                                X = 0,
                                Y = yOffset + heightPerWeatherStation - spacing,
                                Height = dividerLineHeight,
                                Width = 800,
                                BackgroundColor = "#000000",
                            },
                        });

                        yOffset = yOffset + heightPerWeatherStation;
                    }

                    //var numberOfForecastItems = 7;
                    //dailyForecasts = dailyForecasts.Take(numberOfForecastItems).ToList();

                    //var spacing = 20;
                    //var widthPerDailyForecast = (800 - ((numberOfForecastItems + 1) * spacing)) / numberOfForecastItems;
                    //var xOffset = spacing;

                    //for (var i = 0; i < dailyForecasts.Count; i++)
                    //{
                    //    var dailyWeatherForecast = dailyForecasts[i];
                    //    var xCenter = xOffset + (widthPerDailyForecast / 2);

                    //    var dailyWeatherImage = await this.meteoSwissWeatherService.GetWeatherIconAsync(dailyWeatherForecast.IconDayV2, this.weatherIconMapping);

                    //    var dailyWeatherRenderActions = new List<IRenderAction>
                    //    {
                    //            new RenderActions.Text
                    //            {
                    //                X = xCenter,
                    //                Y = 360,
                    //                HorizontalTextAlignment = HorizontalAlignment.Center,
                    //                VerticalTextAlignment = VerticalAlignment.Top,
                    //                Value = $"{dailyWeatherForecast.DayDate:ddd}",
                    //                ForegroundColor = "#000000",
                    //                BackgroundColor = "#FFFFFF",
                    //                FontSize = 20,
                    //                Bold = true,
                    //            },
                    //            new RenderActions.SvgImage
                    //            {
                    //                X = xCenter,
                    //                Y = 390,
                    //                Image = dailyWeatherImage,
                    //                Width = 48,
                    //                Height = 48,
                    //                HorizontalAlignment = HorizontalAlignment.Center,
                    //                VerticalAlignment = VerticalAlignment.Top,
                    //            },
                    //            new RenderActions.Text
                    //            {
                    //                X = xCenter,
                    //                Y = 450,
                    //                HorizontalTextAlignment = HorizontalAlignment.Center,
                    //                VerticalTextAlignment = VerticalAlignment.Top,
                    //                Value = $"{dailyWeatherForecast.TemperatureMin.Value:N0}/{dailyWeatherForecast.TemperatureMax.Value:N0}{dailyWeatherForecast.TemperatureMax:A0}",
                    //                ForegroundColor = "#000000",
                    //                BackgroundColor = "#FFFFFF",
                    //                FontSize = 20,
                    //                Bold= false,
                    //            },
                    //    };


                    //    renderActions.AddRange(dailyWeatherRenderActions);

                    //    xOffset = xOffset + spacing + widthPerDailyForecast;

                    //}

                    return renderActions;
                },
                CrontabSchedule.Parse("*/15 * * * *")); // Update every 15mins

            return Task.CompletedTask;
        }

        public Task OnNavigatedFromAsync(INavigationParameters parameters)
        {
            this.places = null;
            return Task.CompletedTask;
        }
    }
}
