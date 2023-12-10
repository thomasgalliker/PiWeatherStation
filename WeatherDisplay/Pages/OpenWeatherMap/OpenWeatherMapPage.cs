using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DisplayService.Model;
using DisplayService.Resources;
using DisplayService.Services;
using Iot.Device.Bmxx80;
using Iot.Device.Scd4x;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NCrontab;
using OpenWeatherMap;
using OpenWeatherMap.Extensions;
using OpenWeatherMap.Models;
using OpenWeatherMap.Resources.Icons;
using UnitsNet;
using WeatherDisplay.Extensions;
using WeatherDisplay.Model.Settings;
using WeatherDisplay.Resources;
using WeatherDisplay.Resources.Strings;
using WeatherDisplay.Services.DeepL;
using WeatherDisplay.Services.Hardware;
using WeatherDisplay.Services.Navigation;
using WeatherDisplay.Utils;

namespace WeatherDisplay.Pages.OpenWeatherMap
{
    public class OpenWeatherMapPage : IPage, INavigatedTo, INavigatedFrom
    {
        private readonly ILogger logger;
        private readonly IDisplayManager displayManager;
        private readonly IOpenWeatherMapService openWeatherMapService;
        private readonly ITranslationService translationService;
        private readonly IDateTime dateTime;
        private readonly IAppSettings appSettings;
        private readonly IOptionsMonitor<OpenWeatherMapPageOptions> options;
        private readonly ISensorAccessService sensorAccessService;
        private readonly IWeatherIconMapping weatherIconMapping;
        private Place currentPlace = null;

        public OpenWeatherMapPage(
            ILogger<OpenWeatherMapPage> logger,
            IDisplayManager displayManager,
            IOpenWeatherMapService openWeatherMapService,
            ITranslationService translationService,
            IDateTime dateTime,
            IAppSettings appSettings,
            IOptionsMonitor<OpenWeatherMapPageOptions> options,
            ISensorAccessService sensorAccessService)
        {
            this.logger = logger;
            this.displayManager = displayManager;
            this.openWeatherMapService = openWeatherMapService;
            this.translationService = translationService;
            this.dateTime = dateTime;
            this.appSettings = appSettings;
            this.options = options;
            this.sensorAccessService = sensorAccessService;
            this.weatherIconMapping = new HighContrastWeatherIconMapping();
        }

        [Obsolete]
        public Task OnNavigatedToAsync(INavigationParameters navigationParameters)
        {
            var places = this.options.CurrentValue.Places.ToList();

            this.currentPlace = places.GetNextElement(this.currentPlace, defaultValue: places.FirstOrDefault());
            if (this.currentPlace == null)
            {
                throw new Exception(Translations.OpenWeatherMapPage_ErrorMissingPlacesConfiguration);
            }

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
                            Value = $"{Translations.OpenWeatherMapPage_SourceName} / v{fvi.ProductVersion}",
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

                    var oneCallWeatherInfo = await this.openWeatherMapService.GetWeatherOneCallAsync(this.currentPlace.Latitude, this.currentPlace.Longitude, oneCallOptions);
                    var currentWeather = oneCallWeatherInfo.CurrentWeather;
                    var dailyForecasts = oneCallWeatherInfo.DailyForecasts.ToList();
                    var dailyForecastToday = dailyForecasts.OrderBy(f => f.DateTime).First();

                    var currentWeatherCondition = currentWeather.Weather.First();
                    var currentWeatherImage = await this.openWeatherMapService.GetWeatherIconAsync(currentWeatherCondition, this.weatherIconMapping);

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
                                Value = string.Format(Translations.PlaceAndDateTime, this.currentPlace.Name, $"{dateTimeNow:t}"),
                                ForegroundColor = "#000000",
                                BackgroundColor = "#FFFFFF",
                                FontSize = 20,
                            },
                            new RenderActions.BitmapImage
                            {
                                X = 20,
                                Y = 198,
                                Image = currentWeatherImage,
                                //BackgroundColor = Colors.Magenta,
                                VerticalAlignment = VerticalAlignment.Center,
                                HorizontalAlignment = HorizontalAlignment.Left,
                            },
                    };

                    var outdoorTempStackLayout = new RenderActions.StackLayout
                    {
                        X = 140,
                        Y = 198,
                        Width = 200,
                        Height = 70,
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
                                    Value = currentWeather.Temperature.Value.ToString("N0"),
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
                                    Value = $"{currentWeather.Temperature:A}",
                                    ForegroundColor = "#000000",
                                    BackgroundColor = "#FFFFFF",
                                    FontSize = 35,
                                    Bold = true,
                                },
                                new RenderActions.Text
                                {
                                    Y = 60,
                                    X = -30,
                                    HorizontalTextAlignment = HorizontalAlignment.Left,
                                    VerticalTextAlignment = VerticalAlignment.Bottom,
                                    Value = $"/ {currentWeather.Humidity.Value:0}% {Translations.RelativeHumiditySuffix}",
                                    ForegroundColor = "#000000",
                                    BackgroundColor = "#FFFFFF",
                                    FontSize = 20,
                                    Bold = false,
                                }
                        }
                    };
                    currentWeatherRenderActions.Add(outdoorTempStackLayout);

                    // Display local temperature and humidity if the selected place is current place
                    // and the temperature sensor is present

                    Temperature localTemperature = default;
                    RelativeHumidity localHumidity = default;
                    VolumeConcentration co2 = default;

                    if (this.currentPlace.IsCurrentPlace)
                    {
                        if (this.sensorAccessService.Scd41 is IScd4x scd41)
                        {
                            localTemperature = scd41.Temperature;
                            localHumidity = scd41.RelativeHumidity;
                            co2 = scd41.Co2;
                        }
                        else if (this.sensorAccessService.Bme680 is IBme680 bme680)
                        {
                            try
                            {
                                var bme680ReadResult = await bme680.ReadAsync();

                                if (bme680ReadResult != null &&
                                    bme680ReadResult.Temperature != null &&
                                    bme680ReadResult.Humidity != null)
                                {

                                    localTemperature = bme680ReadResult.Temperature.Value;
                                    localHumidity = bme680ReadResult.Humidity.Value;
                                }
                            }
                            catch (Exception ex)
                            {
                                this.logger.LogError(ex, "Failed to read temperature/humidity from BME680");
                            }
                        }
                        else
                        {
                            this.logger.LogWarning("No local temp/humidity/co2 sensor present");
                        }

                        if (localTemperature != default & localHumidity != default)
                        {
                            var indoorTempStackLayout = new RenderActions.StackLayout
                            {
                                X = 140 - 24 - 6,
                                Y = 240,
                                Width = 200,
                                Height = 35,
                                Orientation = StackOrientation.Horizontal,
                                VerticalAlignment = VerticalAlignment.Top,
                                //BackgroundColor = Colors.Cyan,
                                Spacing = 6,
                                Children = new List<IRenderAction>
                                {
                                        new RenderActions.BitmapImage
                                        {
                                            X = 0,
                                            Y = 0,
                                            Image = Icons.TemperatureIndoor(),
                                            Width = 24,
                                            Height = 24,
                                            HorizontalAlignment = HorizontalAlignment.Left,
                                            VerticalAlignment = VerticalAlignment.Top,
                                        },
                                        new RenderActions.Text
                                        {
                                            X = 0,
                                            Y = 5,
                                            HorizontalTextAlignment = HorizontalAlignment.Left,
                                            VerticalTextAlignment = VerticalAlignment.Top,
                                            Value = $"{localTemperature.Value:0.#}{localTemperature:A}",
                                            ForegroundColor = "#000000",
                                            BackgroundColor = "#FFFFFF",
                                            FontSize = 20,
                                            Bold = true,
                                        },
                                        new RenderActions.Text
                                        {
                                            X = 0,
                                            Y = 5,
                                            HorizontalTextAlignment = HorizontalAlignment.Left,
                                            VerticalTextAlignment = VerticalAlignment.Top,
                                            Value = $"/ {localHumidity.Value:0}% {Translations.RelativeHumiditySuffix}",
                                            ForegroundColor = "#000000",
                                            BackgroundColor = "#FFFFFF",
                                            FontSize = 20,
                                            Bold = false,
                                        }
                                }
                            };

                            currentWeatherRenderActions.Add(indoorTempStackLayout);
                        }
                    }

                    // Weather alerts (if exists)
                    if (oneCallWeatherInfo.Alerts.Any())
                    {
                        var mostImportantAlert = oneCallWeatherInfo.Alerts
                            .OrderBy(a => a.StartTime >= dateTimeNow && a.EndTime <= dateTimeNow)
                            .ThenBy(a => a.StartTime)
                            .First();

                        var alertDisplayText = mostImportantAlert.EventName.Substring(mostImportantAlert.EventName.IndexOf(' ')).Trim();

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

                        if (alertDisplayText.Length > 25)
                        {
                            alertDisplayText = $"{alertDisplayText.Substring(0, 22)}...";
                        }

                        if (oneCallWeatherInfo.Alerts.Count > 1)
                        {
                            alertDisplayText += $" (+{oneCallWeatherInfo.Alerts.Count - 1})";
                        }

                        currentWeatherRenderActions.AddRange(new IRenderAction[]
                        {
                                new RenderActions.BitmapImage
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
                        // If there are no weather alerts we display the weather description
                        // as well as air pollution information.

                        currentWeatherRenderActions.Add(
                            new RenderActions.Text
                            {
                                X = 20,
                                Y = 300,
                                HorizontalTextAlignment = HorizontalAlignment.Left,
                                VerticalTextAlignment = VerticalAlignment.Top,
                                Value = currentWeatherCondition.Description.Replace("ß", "ss"),
                                ForegroundColor = "#000000",
                                BackgroundColor = "#FFFFFF",
                                FontSize = 20,
                            });
                    }

                    var airPollutionInfo = await this.openWeatherMapService.GetAirPollutionAsync(this.currentPlace.Latitude, this.currentPlace.Longitude);
                    if (airPollutionInfo.Items.FirstOrDefault() is AirPollutionInfoItem airPollutionInfoItem)
                    {
                        var airPollutionInfoText = $"{Translations.AirQualityLabelText}: {airPollutionInfoItem.Main.AirQuality:N}";

                        currentWeatherRenderActions.AddRange(new IRenderAction[]
                        {
                                    new RenderActions.Text
                                    {
                                        X = 360 + 12,
                                        Y = 300,
                                        HorizontalTextAlignment = HorizontalAlignment.Center,
                                        VerticalTextAlignment = VerticalAlignment.Top,
                                        Value = Translations.UltraViolettAbbreviation,
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
                                        Value = oneCallWeatherInfo.CurrentWeather.UVIndex.ToString("F0"),
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
                                        Value = currentWeather.UVIndex.Range.ToString("N"),
                                        ForegroundColor = "#000000",
                                        BackgroundColor = "#FFFFFF",
                                        FontSize = 20,
                                    },
                                    new RenderActions.BitmapImage
                                    {
                                        X = 500,
                                        Y = 260,
                                        Image = Icons.Earth(),
                                        Width = 24,
                                        Height = 24,
                                        HorizontalAlignment = HorizontalAlignment.Left,
                                        VerticalAlignment = VerticalAlignment.Top,
                                    },
                                    new RenderActions.Text
                                    {
                                        X = 540,
                                        Y = 260 + 5,
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

                    currentWeatherRenderActions.AddRange(new IRenderAction[]
                    {
                            // Sunrise
                            new RenderActions.BitmapImage
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
                                Value = $"{currentWeather.Sunrise.ToUniversalTime().WithOffset(oneCallWeatherInfo.TimezoneOffset):t}",
                                ForegroundColor = "#000000",
                                BackgroundColor = "#FFFFFF",
                                FontSize = 20,
                                Bold = false,
                            },

                            // Sunset
                            new RenderActions.BitmapImage
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
                                Value = $"{currentWeather.Sunset.ToUniversalTime().WithOffset(oneCallWeatherInfo.TimezoneOffset):t}",
                                ForegroundColor = "#000000",
                                BackgroundColor = "#FFFFFF",
                                FontSize = 20,
                                Bold = false,
                            },

                            // Minimum temperature
                            new RenderActions.BitmapImage
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
                                Value = MeteoFormatter.FormatTemperature(dailyForecastToday.Temperature.Min),
                                ForegroundColor = "#000000",
                                BackgroundColor = "#FFFFFF",
                                FontSize = 20,
                                Bold = false,
                            },

                            // Maximum temperature
                            new RenderActions.BitmapImage
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
                                Value = MeteoFormatter.FormatTemperature(dailyForecastToday.Temperature.Max),
                                ForegroundColor = "#000000",
                                BackgroundColor = "#FFFFFF",
                                FontSize = 20,
                                Bold = false,
                            },

                            // Daily amount of rain
                            new RenderActions.BitmapImage
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
                                Value = MeteoFormatter.FormatPrecipitation(dailyForecastToday.Rain, dailyForecastToday.Pop),
                                ForegroundColor = "#000000",
                                BackgroundColor = "#FFFFFF",
                                FontSize = 20,
                                Bold = false,
                            },

                            // Wind
                            new RenderActions.BitmapImage
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
                                Value = MeteoFormatter.FormatWind(currentWeather.WindSpeed, currentWeather.WindDirection),
                                ForegroundColor = "#000000",
                                BackgroundColor = "#FFFFFF",
                                FontSize = 20,
                                Bold = false,
                            },

                            // Atmospheric pressure
                            new RenderActions.BitmapImage
                            {
                                X = 500,
                                Y = 220,
                                Image = Icons.AtmosphericPressure(),
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
                                Value = MeteoFormatter.FormatPressure(currentWeather.Pressure),
                                ForegroundColor = "#000000",
                                BackgroundColor = "#FFFFFF",
                                FontSize = 20,
                                Bold = false,
                            }
                    });

                    if (co2 != default)
                    {
                        currentWeatherRenderActions.AddRange(new IRenderAction[]
                        {
                                // CO2
                                new RenderActions.BitmapImage
                                {
                                    X = 500,
                                    Y = 300,
                                    Image = Icons.IndoorEmpty(),
                                    Width = 24,
                                    Height = 24,
                                    HorizontalAlignment = HorizontalAlignment.Left,
                                    VerticalAlignment = VerticalAlignment.Top,
                                },
                                new RenderActions.Text
                                {
                                    X = 500 + 24,
                                    Y = 300 + 14,
                                    HorizontalTextAlignment = HorizontalAlignment.Right,
                                    VerticalTextAlignment = VerticalAlignment.Center,
                                    Value = $"{Translations.CarbonDioxideAbbreviation}",
                                    ForegroundColor = Colors.Black,
                                    FontSize = 10,
                                    Bold = true,
                                },
                                new RenderActions.Text
                                {
                                    X = 540,
                                    Y = 300 + 5,
                                    AdjustsFontSizeToFitWidth = true,
                                    HorizontalTextAlignment = HorizontalAlignment.Left,
                                    VerticalTextAlignment = VerticalAlignment.Top,
                                    Value = $"{co2}",
                                    ForegroundColor = "#000000",
                                    BackgroundColor = "#FFFFFF",
                                    FontSize = 20,
                                }
                        });
                    }

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
                        currentWeatherRenderActions.Add(
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
                                Bold = true,
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
                        var dailyWeatherImage = await this.openWeatherMapService.GetWeatherIconAsync(dailyWeatherCondition, this.weatherIconMapping);

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
                                new RenderActions.BitmapImage
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
                                    Value = $"{dailyWeatherForecast.Temperature.Min.Value:N0}/{dailyWeatherForecast.Temperature.Max.Value:N0}{dailyWeatherForecast.Temperature.Max:A0}",
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

        public Task OnNavigatedFromAsync(INavigationParameters parameters)
        {
            this.currentPlace = null;
            return Task.CompletedTask;
        }
    }
}
