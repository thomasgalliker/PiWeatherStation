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
using OpenWeatherMap.Models;
using RaspberryPi;
using WeatherDisplay.Extensions;
using WeatherDisplay.Model.Settings;
using WeatherDisplay.Resources;
using WeatherDisplay.Resources.Strings;
using WeatherDisplay.Services.DeepL;
using WeatherDisplay.Services.Hardware;
using WeatherDisplay.Services.Navigation;
using Temperature = OpenWeatherMap.Models.Temperature;

namespace WeatherDisplay.Pages.OpenWeatherMap
{
    public class OpenWeatherMapPage : INavigatedTo, INavigatedFrom
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

        public Task OnNavigatedToAsync(INavigationParameters navigationParameters)
        {
            var places = this.options.CurrentValue.Places.ToList();
            //if (!places.Any())
            //{
            //    throw new Exception(Translations.OpenWeatherMapPageErrorMissingPlacesConfigurationLine1);
            //}

            this.currentPlace = places.GetNextElement(this.currentPlace, defaultValue: places.FirstOrDefault());

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

            if (this.currentPlace != null)
            {
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

                        var dailyForecasts = oneCallWeatherInfo.DailyForecasts.ToList();
                        var dailyForecastToday = dailyForecasts.OrderBy(f => f.DateTime).First();

                        var currentWeatherInfo = oneCallWeatherInfo.CurrentWeather;
                        var currentWeatherCondition = currentWeatherInfo.Weather.First();
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
                                    Value = $"{currentWeatherInfo.Temperature:U}",
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
                                    Value = $"/ {dailyForecastToday.Humidity} {Translations.RelativeHumiditySuffix}",
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

                        UnitsNet.Temperature localTemperature = default;
                        UnitsNet.RelativeHumidity localHumidity = default;
                        UnitsNet.VolumeConcentration co2 = default;

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
                                        new RenderActions.StreamImage
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
                                Value = $"{dailyForecastToday.WindSpeed:0}m/s ({dailyForecastToday.WindDirection.GetSecondaryIntercardinalWindDirection().ToString("A")})",
                                ForegroundColor = "#000000",
                                BackgroundColor = "#FFFFFF",
                                FontSize = 20,
                                Bold = false,
                            },

                            // Atmospheric pressure
                            new RenderActions.StreamImage
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
                                Value = $"{dailyForecastToday.Pressure} ({dailyForecastToday.Pressure.Range:N})",
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
                                new RenderActions.StreamImage
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
                                    Value = $"{dailyWeatherForecast.Temperature.Min.Value:F0}/{dailyWeatherForecast.Temperature.Max:F0}{dailyWeatherForecast.Temperature.Max:U}",
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
            }
            else
            {
                // Error: Missing places configuration in appsettings.User.json
                this.displayManager.AddRenderActions(() =>
                {
                    return new IRenderAction[]
                    {
                        new RenderActions.Text
                        {
                            X = 20,
                            Y = 120,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = Translations.OpenWeatherMapPage_ErrorMissingPlacesConfigurationLine1,
                            ForegroundColor = "#000000",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 20,
                        },
                        new RenderActions.Text
                        {
                            X = 20,
                            Y = 140,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = Translations.OpenWeatherMapPage_ErrorMissingPlacesConfigurationLine2,
                            ForegroundColor = "#000000",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 20,
                        },
                    };
                });
            }

            return Task.CompletedTask;
        }

        public Task OnNavigatedFromAsync(INavigationParameters parameters)
        {
            this.currentPlace = null;
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
