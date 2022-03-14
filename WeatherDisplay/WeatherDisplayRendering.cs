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
            var weatherIconMapping = new HighContrastWeatherIconMapping();

            // Date header
            displayManager.AddRenderActions(
                () =>
                {
                    var dateTimeNow = dateTime.Now;
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
                        }
                    };
                });

            // Current weather info
            displayManager.AddRenderActionsAsync(
                async () =>
                {
                    var place = appSettings.Places.First();
                    var currentWeatherInfo = await openWeatherMapService.GetCurrentWeatherAsync(place.Latitude, place.Longitude);
                    var currentWeatherCondition = currentWeatherInfo.Weather.First();
                    var currentWeatherImage = await openWeatherMapService.GetWeatherIconAsync(currentWeatherCondition, weatherIconMapping);

                    var weatherForecast = await openWeatherMapService.GetWeatherForecast(place.Latitude, place.Longitude);
                    var groupedWeatherForecast = weatherForecast.Items.GroupBy(i => i.DateTime.Date).ToList();

                    var oneCallWeatherInfo = await openWeatherMapService.GetWeatherOneCallAsync(place.Latitude, place.Longitude);


                    var renderActions = new List<IRenderAction>
                    {
                        //new RenderActions.Rectangle // Masking layer for Location+Temperature
                        //{
                        //    X = 20,
                        //    Y = 200,
                        //    Height = 150,
                        //    Width = 300,
                        //    HorizontalAlignment = HorizontalAlignment.Left,
                        //    VerticalAlignment = VerticalAlignment.Center,
                        //    BackgroundColor = "#FFFFFF",
                        //},
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

                    // Display daily weather forecast
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
                                Value = $"{dailyWeatherForecast.Temperature.Min.Value:F0} / {dailyWeatherForecast.Temperature.Max:F0}",
                                ForegroundColor = "#000000",
                                BackgroundColor = "#FFFFFF",
                                FontSize = 20,
                                Bold= true,
                            },
                        };


                        renderActions.AddRange(dailyWeatherRenderActions);

                        xOffset = xOffset + spacing + widthPerDailyForecast;

                    }
                    return renderActions;
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
