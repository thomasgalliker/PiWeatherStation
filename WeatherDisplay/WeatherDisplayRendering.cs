using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using DisplayService.Model;
using DisplayService.Services;
using NCrontab;
using WeatherDisplay.Extensions;
using WeatherDisplay.Model;
using WeatherDisplay.Model.OpenWeatherMap;
using WeatherDisplay.Resources;
using WeatherDisplay.Services;

namespace WeatherDisplay
{
    public static class WeatherDisplayRendering
    {
        public static void AddWeatherRenderActions(this IDisplayManager displayManager, IOpenWeatherMapService openWeatherMapService, ITranslationService translationService, IDateTime dateTime, IAppSettings appSettings)
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
                            Y = 50,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Center,
                            Value = dateTimeNow.ToString("dddd, d. MMMM"),
                            ForegroundColor = "#FFFFFF",
                            FontSize = 70,
                            AdjustsFontSizeToFitWidth = true,
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
                },
                CrontabSchedule.Parse("0 0 * * *")); // TODO: Update every 24h - starting from 00:00

            // Current weather info
            displayManager.AddRenderActionsAsync(
                async () =>
                {
                    var dateTimeNow = dateTime.Now;
                    var place = appSettings.Places.First();

                    // Get current weather
                    var currentWeatherInfo = await openWeatherMapService.GetCurrentWeatherAsync(place.Latitude, place.Longitude);
                    var currentWeatherCondition = currentWeatherInfo.Weather.First();
                    var currentWeatherImage = await openWeatherMapService.GetWeatherIconAsync(currentWeatherCondition, weatherIconMapping);

                    var oneCallWeatherInfo = await openWeatherMapService.GetWeatherOneCallAsync(place.Latitude, place.Longitude);
                    var dailyForecasts = oneCallWeatherInfo.DailyForecasts.ToList();
                    var dailyForecastToday = dailyForecasts.OrderBy(f => f.DateTime).First();

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
                        new RenderActions.Text
                        {
                            X = 20,
                            Y = 120,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"{place.Name ?? currentWeatherInfo.Name}, um {dateTimeNow:t} Uhr",
                            ForegroundColor = "#000000",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 20,
                        },
                        new RenderActions.StreamImage
                        {
                            X = 20,
                            Y = 198,
                            Image = currentWeatherImage,
                            VerticalAlignment = VerticalAlignment.Center,
                            HorizontalAlignment = HorizontalAlignment.Left,
                        },
                        new RenderActions.Text
                        {
                            X = 140,
                            Y = 198,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Center,
                            Value = FormatTemperature(currentWeatherInfo.Main.Temperature),
                            ForegroundColor = "#000000",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 80,
                        }
                    };

                    var isLongWeatherDescription = currentWeatherCondition.Description.Length > 16;
                    var descriptionXPostion = isLongWeatherDescription ? 20 : 147;
                    var descriptionYPostion = isLongWeatherDescription ? 260 : 240;

                    currentWeatherRenderActions.Add(
                        new RenderActions.Text
                        {
                            X = descriptionXPostion,
                            Y = descriptionYPostion,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"{currentWeatherCondition.Description}",
                            ForegroundColor = "#000000",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 20,
                        });

                    // Weather alerts (if exists)
                    if (oneCallWeatherInfo.Alerts.Any())
                    {
                        var mostImportantAlert = oneCallWeatherInfo.Alerts
                            .OrderBy(a => a.StartTime >= dateTimeNow && a.EndTime <= dateTimeNow)
                            .ThenBy(a => a.StartTime)
                            .First();

                        var alertDisplayText = $"{mostImportantAlert.Description}";
                        if (oneCallWeatherInfo.Alerts.Count > 1)
                        {
                            alertDisplayText += $" (+{oneCallWeatherInfo.Alerts.Count - 1})";
                        }

                        try
                        {
                            var translatedTexts = await translationService.Translate(
                                targetLanguage: CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, 
                                texts: alertDisplayText);

                            alertDisplayText = translatedTexts.FirstOrDefault() ?? alertDisplayText;
                        }
                        catch
                        {
                            // Ignored
                        }

                        currentWeatherRenderActions.AddRange(new IRenderAction[]
                        {
                            new RenderActions.StreamImage
                            {
                                X = 20,
                                Y = 300,
                                Image = Icons.Alert(),
                                Width = 24,
                                Height = 24,
                                HorizontalAlignment = HorizontalAlignment.Left,
                                VerticalAlignment = VerticalAlignment.Top,
                            },
                            new RenderActions.Text
                            {
                                X = 56,
                                Y = 300 + 5,
                                AdjustsFontSizeToFitWidth = true,
                                HorizontalTextAlignment = HorizontalAlignment.Left,
                                VerticalTextAlignment = VerticalAlignment.Top,
                                Value = alertDisplayText,
                                ForegroundColor = "#000000",
                                BackgroundColor = "#FFFFFF",
                                FontSize = 20,
                            }
                        });
                    }

                    currentWeatherRenderActions.AddRange(new IRenderAction[]
                    {
                        // Sunrise
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
                            Y = 140 + 5,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"{dailyForecastToday.Sunrise.ToUniversalTime().WithOffset(oneCallWeatherInfo.TimezoneOffset):t}",
                            ForegroundColor = "#000000",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 20,
                            Bold = false,
                        },

                        // Sunset
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
                            Y = 180 + 5,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"{dailyForecastToday.Sunset.ToUniversalTime().WithOffset(oneCallWeatherInfo.TimezoneOffset):t}",
                            ForegroundColor = "#000000",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 20,
                            Bold = false,
                        },

                        // Minimum temperature
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
                            Y = 220 + 5,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = FormatTemperature(dailyForecastToday.Temperature.Min),
                            ForegroundColor = "#000000",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 20,
                            Bold = false,
                        },

                        // Maximum temperature
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
                            Y = 260 + 5,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = FormatTemperature(dailyForecastToday.Temperature.Max),
                            ForegroundColor = "#000000",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 20,
                            Bold = false,
                        },

                        // Daily amount of rain
                        new RenderActions.StreamImage
                        {
                            X = 500,
                            Y = 140,
                            Image = Icons.RainLight(),
                            Width = 24,
                            Height = 24,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top,
                        },
                        new RenderActions.Text
                        {
                            X = 540,
                            Y = 140 + 5,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"{FormatRain(dailyForecastToday.Rain)} ({(dailyForecastToday.Pop * 100):0}%)",
                            ForegroundColor = "#000000",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 20,
                            Bold = false,
                        },

                        // Wind
                        new RenderActions.StreamImage
                        {
                            X = 500,
                            Y = 180,
                            Image = Icons.Wind(),
                            Width = 24,
                            Height = 24,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top,
                        },
                        new RenderActions.Text
                        {
                            X = 540,
                            Y = 180 + 5,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"{dailyForecastToday.WindSpeed:0}m/s ({dailyForecastToday.WindDirection})",
                            ForegroundColor = "#000000",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 20,
                            Bold = false,
                        },

                        // Humidity
                        new RenderActions.StreamImage
                        {
                            X = 500,
                            Y = 220,
                            Image = Icons.Humidity(),
                            Width = 24,
                            Height = 24,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top,
                        },
                        new RenderActions.Text
                        {
                            X = 540,
                            Y = 220 + 5,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"{dailyForecastToday.Humidity} ({dailyForecastToday.Humidity.Range:N})",
                            ForegroundColor = "#000000",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 20,
                            Bold = false,
                        },

                        // Atmospheric pressure
                        new RenderActions.StreamImage
                        {
                            X = 500,
                            Y = 260,
                            Image = Icons.AtmosphericPressure(),
                            Width = 24,
                            Height = 24,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top,
                        },
                        new RenderActions.Text
                        {
                            X = 540,
                            Y = 260 + 5,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"{dailyForecastToday.Pressure} ({dailyForecastToday.Pressure.Range:N})",
                            ForegroundColor = "#000000",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 20,
                            Bold = false,
                        }
                    });

                    currentWeatherRenderActions.AddRange(new[]
                    {
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
                    });

                    if (appSettings.IsDebug)
                    {
                        currentWeatherRenderActions.AddRange(new[]
                        {
                                new RenderActions.Text
                                {
                                    X = 20,
                                    Y = 160 + 48,
                                    HorizontalTextAlignment = HorizontalAlignment.Left,
                                    VerticalTextAlignment = VerticalAlignment.Center,
                                    Value = $"{currentWeatherCondition.Id} / {currentWeatherCondition.IconId}",
                                    ForegroundColor = "#000000",
                                    BackgroundColor = "#FFFFFF",
                                    FontSize = 12,
                                    Bold= true,
                                },
                            });
                    }

                    // Display daily weather forecast
                    var numberOfForecastItems = 7;
                    dailyForecasts = oneCallWeatherInfo.DailyForecasts.Take(numberOfForecastItems).ToList();
                    var spacing = 20;
                    var widthPerDailyForecast = (800 - (numberOfForecastItems + 1) * spacing) / numberOfForecastItems;
                    var xOffset = spacing;

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
                CrontabSchedule.Parse("0 * * * *")); // TODO: Update every 1h - starting from 00:00
        }

        private static string FormatRain(double rain)
        {
            if (rain > 0d && rain < 1d)
            {
                return $"{rain:F1}mm";
            }

            return $"{rain:0}mm";
        }

        private static string FormatTemperature(Temperature temperature)
        {
            string formattedTemperature;
            //if (temperature.Value < 1d && temperature.Value > -1)
            //{
            //    formattedTemperature = temperature.ToString("0.#");
            //}
            //else
            {
                formattedTemperature = temperature.ToString("0");
            }

            return formattedTemperature;
        }

    }
}
