using System;
using System.Collections.Generic;
using System.Linq;
using DisplayService.Model;
using DisplayService.Services;
using WeatherDisplay.Model;
using WeatherDisplay.Model.OpenWeatherMap;
using WeatherDisplay.Services;

namespace WeatherDisplay
{
    public static class WeatherDisplayRendering
    {
        public static void AddWeatherRenderActions(this IDisplayManager displayManager, IOpenWeatherMapService openWeatherMapService, IDateTime dateTime, IAppSettings appSettings)
        {
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
                    var dateTimeNow = dateTime.Now;
                    return new List<IRenderAction>
                    {
                        new RenderActions.Rectangle // Masking layer for Date
                        {
                            X = 782,
                            Y = 19,
                            Height = 25,
                            Width = 270,
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
                    var dateTimeNow = dateTime.Now;
                    return new List<IRenderAction>
                    {
                        new RenderActions.Rectangle // Masking layer for Time
                        {
                            X = 780,
                            Y = 50,
                            Height = 25,
                            Width = 200,
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
                    var weatherInfo = await openWeatherMapService.GetCurrentWeatherAsync(place.Latitude, place.Longitude);
                    var firstWeather = weatherInfo.WeatherConditions.FirstOrDefault();
                    var weatherIconImage = await openWeatherMapService.GetWeatherIconAsync(firstWeather);

                    return new List<IRenderAction>
                    {
                        new RenderActions.Rectangle // Masking layer for Location+Temperature
                        {
                            X = 400,
                            Y = 250,
                            Height = 150,
                            Width = 300,
                            VerticalAlignment = VerticalAlignment.Center,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            BackgroundColor = "#FFFFFF",
                        },
                        new RenderActions.Text
                        {
                            X = 400,
                            Y = 200,
                            HorizontalTextAlignment = HorizontalAlignment.Center,
                            VerticalTextAlignment = VerticalAlignment.Center,
                            Value = $"{place.Name ?? weatherInfo.Name}",
                            ForegroundColor = "#191919",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 20,
                        },
                        new RenderActions.Text
                        {
                            X = 400,
                            Y = 240,
                            HorizontalTextAlignment = HorizontalAlignment.Center,
                            VerticalTextAlignment = VerticalAlignment.Center,
                            Value = FormatTemperature(weatherInfo.Main.Temperature),
                            ForegroundColor = "#000000",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 80,
                        },
                        new RenderActions.Text
                        {
                            X = 100,
                            Y = 200,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Center,
                            Value = $"Sunrise {weatherInfo.AdditionalInformation.Sunrise:t}",
                            ForegroundColor = "#191919",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 20,
                        },
                        new RenderActions.Text
                        {
                            X = 100,
                            Y = 240,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Center,
                            Value = $"Sunset {weatherInfo.AdditionalInformation.Sunset:t}",
                            ForegroundColor = "#191919",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 20,
                        },
                        new RenderActions.Text
                        {
                            X = 100,
                            Y = 280,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Center,
                            Value = $"Min. Temp. {FormatTemperature(weatherInfo.Main.MinimumTemperature)}",
                            ForegroundColor = "#191919",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 20,
                        },
                        new RenderActions.Text
                        {
                            X = 100,
                            Y = 320,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Center,
                            Value = $"Max. Temp. {FormatTemperature(weatherInfo.Main.MaximumTemperature)}",
                            ForegroundColor = "#191919",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 20,
                        },
                        new RenderActions.StreamImage
                        {
                            X = 600,
                            Y = 200,
                            Image = weatherIconImage,
                        }
                    };
                },
                TimeSpan.FromHours(1));
        }

        private static string FormatTemperature(Temperature temperature)
        {
            string formattedTemperature;
            if (temperature.Value < 10d && temperature.Value > -10)
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
