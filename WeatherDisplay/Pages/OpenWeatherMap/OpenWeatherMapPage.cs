using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DisplayService.Model;
using DisplayService.Services;
using Microsoft.Extensions.Options;
using NCrontab;
using OpenWeatherMap;
using OpenWeatherMap.Models;
using WeatherDisplay.Extensions;
using WeatherDisplay.Model;
using WeatherDisplay.Resources;
using WeatherDisplay.Services.DeepL;

namespace WeatherDisplay.Pages.OpenWeatherMap
{
    public class OpenWeatherMapPage : INavigatedAware
    {
        private readonly IDisplayManager displayManager;
        private readonly IOpenWeatherMapService openWeatherMapService;
        private readonly ITranslationService translationService;
        private readonly IDateTime dateTime;
        private readonly IAppSettings appSettings;
        private readonly IOptionsMonitor<OpenWeatherMapPageOptions> options;

        private Place currentPlace = null;

        public OpenWeatherMapPage(
            IDisplayManager displayManager,
            IOpenWeatherMapService openWeatherMapService,
            ITranslationService translationService,
            IDateTime dateTime,
            IAppSettings appSettings,
            IOptionsMonitor<OpenWeatherMapPageOptions> options)
        {
            this.displayManager = displayManager;
            this.openWeatherMapService = openWeatherMapService;
            this.translationService = translationService;
            this.dateTime = dateTime;
            this.appSettings = appSettings;
            this.options = options;
        }

        public Task OnNavigatedToAsync(INavigationParameters navigationParameters)
        {
            var places = this.options.CurrentValue.Places.ToList();
            var place = this.currentPlace = places.GetNextElement(this.currentPlace, defaultValue: places.First());

            var weatherIconMapping = new HighContrastWeatherIconMapping();

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
                    // Get current weather & daily forecasts
                    var oneCallOptions = new OneCallOptions
                    {
                        IncludeCurrentWeather = true,
                        IncludeDailyForecasts = true,
                        IncludeMinutelyForecasts = true,
                        IncludeHourlyForecasts = true,
                    };

                    var oneCallWeatherInfo = await this.openWeatherMapService.GetWeatherOneCallAsync(place.Latitude, place.Longitude, oneCallOptions);

                    var dailyForecasts = oneCallWeatherInfo.DailyForecasts.ToList();
                    var dailyForecastToday = dailyForecasts.OrderBy(f => f.DateTime).First();

                    var currentWeatherInfo = oneCallWeatherInfo.CurrentWeather;
                    var currentWeatherCondition = currentWeatherInfo.Weather.First();
                    var currentWeatherImage = await this.openWeatherMapService.GetWeatherIconAsync(currentWeatherCondition, weatherIconMapping);

                    var dateTimeNow = this.dateTime.Now;

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
                            Value = $"{place.Name}, um {dateTimeNow:t} Uhr",
                            ForegroundColor = "#000000",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 20,
                        },
                        new RenderActions.StreamImage
                        {
                            X = 20,
                            Y = 198,
                            Image = currentWeatherImage,
                            //BackgroundColor = Colors.Magenta,
                            VerticalAlignment = VerticalAlignment.Center,
                            HorizontalAlignment = HorizontalAlignment.Left,
                        },
                    };

                    var temperatureStackLayout = new RenderActions.StackLayout
                    {
                        Width = 200,
                        Height = 70,
                        X = 140,
                        Y = 198,
                        Orientation = StackOrientation.Horizontal,
                        VerticalAlignment = VerticalAlignment.Center,
                        //BackgroundColor = Colors.Magenta,
                        Children = new List<IRenderAction>
                        {
                            new RenderActions.Text
                            {
                                HorizontalTextAlignment = HorizontalAlignment.Left,
                                VerticalTextAlignment = VerticalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Center,
                                Value = currentWeatherInfo.Temperature.ToString("N0"),
                                ForegroundColor = "#000000",
                                BackgroundColor = "#FFFFFF",
                                FontSize = 70,
                                AdjustsFontSizeToFitWidth = true,
                                AdjustsFontSizeToFitHeight = true,
                            },
                            new RenderActions.Text
                            {
                                Y = 1,
                                HorizontalTextAlignment = HorizontalAlignment.Left,
                                VerticalTextAlignment = VerticalAlignment.Bottom,
                                VerticalAlignment = VerticalAlignment.Center,
                                Value =  currentWeatherInfo.Temperature.ToString("U"),
                                ForegroundColor = "#000000",
                                BackgroundColor = "#FFFFFF",
                                FontSize = 35,
                            }
                        }
                    };
                    currentWeatherRenderActions.Add(temperatureStackLayout);

                    var weatherDescription = currentWeatherCondition.Description.Replace("ß", "ss");
                    var isLongWeatherDescription = weatherDescription.Length > 16;
                    var descriptionXPostion = isLongWeatherDescription ? 20 : 147;
                    var descriptionYPostion = isLongWeatherDescription ? 260 : 240;

                    currentWeatherRenderActions.Add(
                        new RenderActions.Text
                        {
                            X = descriptionXPostion,
                            Y = descriptionYPostion,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = weatherDescription,
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
                            var translatedTexts = await this.translationService.Translate(
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
                    else
                    {
                        // If there are no weather alerst and the air pollution is not good,
                        // we display some air pollution information.

                        var airPollutionInfo = await this.openWeatherMapService.GetAirPollutionAsync(place.Latitude, place.Longitude);
                        if (airPollutionInfo.Items.FirstOrDefault() is AirPollutionInfoItem airPollutionInfoItem /*&&
                            airPollutionInfoItem.Main.AirQuality > AirQuality.Good*/)
                        {
                            var airPollutionInfoText = $"Air Quality: {airPollutionInfoItem.Main.AirQuality:N}";

                            currentWeatherRenderActions.AddRange(new IRenderAction[]
                            {
                                new RenderActions.Text
                                {
                                    X = 360 + 12,
                                    Y = 300,
                                    HorizontalTextAlignment = HorizontalAlignment.Center,
                                    VerticalTextAlignment = VerticalAlignment.Top,
                                    Value = "UV",
                                    ForegroundColor = "#000000",
                                    BackgroundColor = "#FFFFFF",
                                    FontSize = 14,
                                    Bold = true,
                                },
                                new RenderActions.Text
                                {
                                    X = 360 + 12,
                                    Y = 300 + 24,
                                    HorizontalTextAlignment = HorizontalAlignment.Center,
                                    VerticalTextAlignment = VerticalAlignment.Bottom,
                                    Value = dailyForecastToday.UVIndex.ToString("F0"),
                                    ForegroundColor = "#000000",
                                    BackgroundColor = "#FFFFFF",
                                    FontSize = 14,
                                    Bold = true,
                                },
                                new RenderActions.Text
                                {
                                    X = 400,
                                    Y = 300 + 5,
                                    AdjustsFontSizeToFitWidth = true,
                                    HorizontalTextAlignment = HorizontalAlignment.Left,
                                    VerticalTextAlignment = VerticalAlignment.Top,
                                    Value = dailyForecastToday.UVIndex.Range.ToString("N"),
                                    ForegroundColor = "#000000",
                                    BackgroundColor = "#FFFFFF",
                                    FontSize = 20,
                                },
                                new RenderActions.StreamImage
                                {
                                    X = 500,
                                    Y = 300,
                                    Image = Icons.Earth(),
                                    Width = 24,
                                    Height = 24,
                                    HorizontalAlignment = HorizontalAlignment.Left,
                                    VerticalAlignment = VerticalAlignment.Top,
                                },
                                new RenderActions.Text
                                {
                                    X = 540,
                                    Y = 300 + 5,
                                    AdjustsFontSizeToFitWidth = true,
                                    HorizontalTextAlignment = HorizontalAlignment.Left,
                                    VerticalTextAlignment = VerticalAlignment.Top,
                                    Value = airPollutionInfoText,
                                    ForegroundColor = "#000000",
                                    BackgroundColor = "#FFFFFF",
                                    FontSize = 20,
                                }
                            });
                        }

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
                            Value = $"{FormatRain(dailyForecastToday.Rain)} ({dailyForecastToday.Pop * 100:0}%)",
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

                    if (this.appSettings.IsDebug)
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
                    var widthPerDailyForecast = (800 - ((numberOfForecastItems + 1) * spacing)) / numberOfForecastItems;
                    var xOffset = spacing;

                    for (var i = 0; i < dailyForecasts.Count; i++)
                    {
                        var dailyWeatherForecast = dailyForecasts[i];
                        var xCenter = xOffset + (widthPerDailyForecast / 2);

                        var dailyWeatherCondition = dailyWeatherForecast.Weather.First();
                        var dailyWeatherImage = await this.openWeatherMapService.GetWeatherIconAsync(dailyWeatherCondition, weatherIconMapping);

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

                        if (this.appSettings.IsDebug)
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
                CrontabSchedule.Parse("*/15 * * * *")); // Update every 15mins

            return Task.CompletedTask;
        }

        private static string FormatRain(double rain)
        {
            return rain > 0d && rain < 1d ? $"{rain:F1}mm" : $"{rain:0}mm";
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
