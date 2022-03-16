using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DisplayService.Model;
using DisplayService.Services;
using WeatherDisplay.Model;
using WeatherDisplay.Model.OpenWeatherMap;
using WeatherDisplay.Resources;
using WeatherDisplay.Services;

namespace WeatherDisplay
{
    public static class WeatherDisplayRendering
    {
        public static void AddWeatherRenderActions(this IDisplayManager displayManager, IOpenWeatherMapService openWeatherMapService, IDateTime dateTime, IAppSettings appSettings)
        {
            var weatherIconMapping = new HighContrastWeatherIconMapping();

            // Date header
            displayManager.AddRenderActions(
                () =>
                {
                    var dateTimeNow = dateTime.Now;

                    var assembly = Assembly.GetExecutingAssembly();
                    var fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
                    var productVersion = fvi.ProductVersion;

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
                            Y = 20,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = dateTimeNow.ToString("dddd, dd. MMMM"),
                            ForegroundColor = "#FFFFFF",
                            FontSize = 70,
                            Bold = true,
                        },
                        
                        // Version
                        new RenderActions.Text
                        {
                            X = 798,
                            Y = 88,
                            HorizontalTextAlignment = HorizontalAlignment.Right,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"v{productVersion}",
                            ForegroundColor = "#FFFFFF",
                            BackgroundColor = "#000000",
                            FontSize = 12,
                            Bold = false,
                        },
                    };
                });

            // Current weather info
            displayManager.AddRenderActionsAsync(
                async () =>
                {
                    var place = appSettings.Places.First();

                    // Get current weather
                    var currentWeatherInfo = await openWeatherMapService.GetCurrentWeatherAsync(place.Latitude, place.Longitude);
                    var currentWeatherCondition = currentWeatherInfo.Weather.First();
                    var currentWeatherImage = await openWeatherMapService.GetWeatherIconAsync(currentWeatherCondition, weatherIconMapping);

                    var currentWeatherRenderActions = new List<IRenderAction>
                    {
                        // Current location + current temperature
                        new RenderActions.Rectangle
                        {
                            X = 0,
                            Y = 100,
                            Width = 800,
                            Height = 380,
                            BackgroundColor = "#FFFFFF",
                        },
                        new RenderActions.StreamImage
                        {
                            X = 20,
                            Y = 200,
                            Image = currentWeatherImage,
                        },
                        new RenderActions.Text
                        {
                            X = 20,
                            Y = 120,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"{place.Name ?? currentWeatherInfo.Name}, um {currentWeatherInfo.Date:t} Uhr",
                            ForegroundColor = "#000000",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 20,
                        },
                        new RenderActions.Text
                        {
                            X = 140,
                            Y = 240,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Center,
                            Value = FormatTemperature(currentWeatherInfo.Main.Temperature),
                            ForegroundColor = "#000000",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 80,
                        },
                        new RenderActions.Text
                        {
                            X = 140,
                            Y = 280,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"{currentWeatherCondition.Description}",
                            ForegroundColor = "#000000",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 20,
                        },

                        // Sunrise + sunset
                        new RenderActions.StreamImage
                        {
                            X = 360,
                            Y = 140,
                            Image = Icons.Sunrise72(),
                            Width = 24,
                            Height = 24,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top,
                        },
                        new RenderActions.Text
                        {
                            X = 400,
                            Y = 140,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"{currentWeatherInfo.AdditionalInformation.Sunrise:t}",
                            ForegroundColor = "#000000",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 20,
                            Bold = false,
                        },
                        new RenderActions.StreamImage
                        {
                            X = 360,
                            Y = 180,
                            Image = Icons.Sunset72(),
                            Width = 24,
                            Height = 24,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top,
                        },
                        new RenderActions.Text
                        {
                            X = 400,
                            Y = 180,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"{currentWeatherInfo.AdditionalInformation.Sunset:t}",
                            ForegroundColor = "#000000",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 20,
                            Bold = false,
                        },

                        // Minimum + maximum temperature
                        new RenderActions.StreamImage
                        {
                            X = 360,
                            Y = 220,
                            Image = Icons.TemperatureMinus(),
                            Width = 24,
                            Height = 24,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top,
                        },
                        new RenderActions.Text
                        {
                            X = 400,
                            Y = 220,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"{FormatTemperature(currentWeatherInfo.Main.MinimumTemperature)}",
                            ForegroundColor = "#000000",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 20,
                            Bold = false,
                        },
                        new RenderActions.StreamImage
                        {
                            X = 360,
                            Y = 260,
                            Image = Icons.TemperaturePlus(),
                            Width = 24,
                            Height = 24,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top,
                        },
                        new RenderActions.Text
                        {
                            X = 400,
                            Y = 260,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"{FormatTemperature(currentWeatherInfo.Main.MaximumTemperature)}",
                            ForegroundColor = "#000000",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 20,
                            Bold = false,
                        },

                        // Divider line to separated current weather and weather forecast
                        new RenderActions.Rectangle
                        {
                            X = 0,
                            Y = 340,
                            Height = 4,
                            Width = 800,
                            BackgroundColor = "#000000",
                        },
                        new RenderActions.Rectangle
                        {
                            X = 0,
                            Y = 480,
                            Height = 4,
                            Width = 800,
                            VerticalAlignment = VerticalAlignment.Bottom,
                            BackgroundColor = "#000000",
                        }
                    };

                    if (appSettings.IsDebug)
                    {
                        currentWeatherRenderActions.AddRange(new[]
                        {
                                new RenderActions.Text
                                {
                                    X = 20,
                                    Y = 240,
                                    HorizontalTextAlignment = HorizontalAlignment.Left,
                                    VerticalTextAlignment = VerticalAlignment.Top,
                                    Value = $"{currentWeatherCondition.Id} / {currentWeatherCondition.IconId}",
                                    ForegroundColor = "#000000",
                                    BackgroundColor = "#FFFFFF",
                                    FontSize = 12,
                                    Bold= true,
                                },
                            });
                    }

                    // Display daily weather forecast
                    var oneCallWeatherInfo = await openWeatherMapService.GetWeatherOneCallAsync(place.Latitude, place.Longitude);

                    var numberOfForecastItems = 7;
                    var spacing = 20;
                    var widthPerDailyForecast = (800 - (numberOfForecastItems + 1) * spacing) / numberOfForecastItems;
                    var xOffset = spacing;
                    var dailyForecasts = oneCallWeatherInfo.DailyForecasts.Take(numberOfForecastItems).ToList();

                    for (var i = 0; i < dailyForecasts.Count; i++)
                    {
                        var dailyWeatherForecast = dailyForecasts[i];
                        var xCenter = xOffset + widthPerDailyForecast / 2;

                        var dailyWeatherCondition = dailyWeatherForecast.Weather.First();
                        var dailyWeatherImage = await openWeatherMapService.GetWeatherIconAsync(dailyWeatherCondition, weatherIconMapping);

                        var dailyWeatherRenderActions = new List<IRenderAction>
                        {
                            new RenderActions.Text
                            {
                                X = xCenter,
                                Y = 360,
                                HorizontalTextAlignment = HorizontalAlignment.Center,
                                VerticalTextAlignment = VerticalAlignment.Top,
                                Value = $"{dailyWeatherForecast.DateTime:ddd}",
                                ForegroundColor = "#000000",
                                BackgroundColor = "#FFFFFF",
                                FontSize = 20,
                                Bold = true,
                            },
                            new RenderActions.StreamImage
                            {
                                X = xCenter,
                                Y = 390,
                                Image = dailyWeatherImage,
                                Width = 48,
                                Height = 48,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Top,
                            },
                            new RenderActions.Text
                            {
                                X = xCenter,
                                Y = 450,
                                HorizontalTextAlignment = HorizontalAlignment.Center,
                                VerticalTextAlignment = VerticalAlignment.Top,
                                Value = $"{dailyWeatherForecast.Temperature.Min.Value:F0}/{dailyWeatherForecast.Temperature.Max:F0}",
                                ForegroundColor = "#000000",
                                BackgroundColor = "#FFFFFF",
                                FontSize = 20,
                                Bold= false,
                            },
                        };

                        if (appSettings.IsDebug)
                        {
                            dailyWeatherRenderActions.AddRange(new[]
                            {
                                new RenderActions.Text
                                {
                                    X = xCenter,
                                    Y = 390,
                                    HorizontalTextAlignment = HorizontalAlignment.Center,
                                    VerticalTextAlignment = VerticalAlignment.Top,
                                    Value = $"{dailyWeatherCondition.Id} / {dailyWeatherCondition.IconId}",
                                    ForegroundColor = "#000000",
                                    BackgroundColor = "#FFFFFF",
                                    FontSize = 12,
                                    Bold= true,
                                },
                            });
                        }

                        currentWeatherRenderActions.AddRange(dailyWeatherRenderActions);

                        xOffset = xOffset + spacing + widthPerDailyForecast;

                    }
                    return currentWeatherRenderActions;
                },
                TimeSpan.FromHours(1));
        }

        private static string FormatTemperature(Temperature temperature)
        {
            string formattedTemperature;
            if (temperature.Value < 1d && temperature.Value > -1)
            {
                formattedTemperature = temperature.ToString("0.#");
            }
            else
            {
                formattedTemperature = temperature.ToString("0");
            }

            return formattedTemperature;
        }

    }
}
