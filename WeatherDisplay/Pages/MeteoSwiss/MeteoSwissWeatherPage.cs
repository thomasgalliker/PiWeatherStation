using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DisplayService.Model;
using DisplayService.Resources;
using DisplayService.Services;
using Iot.Device.Bmxx80;
using Iot.Device.Scd4x;
using MeteoSwissApi;
using MeteoSwissApi.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NCrontab;
using UnitsNet;
using WeatherDisplay.Extensions;
using WeatherDisplay.Model.Settings;
using WeatherDisplay.Resources;
using WeatherDisplay.Resources.Strings;
using WeatherDisplay.Services.Hardware;
using WeatherDisplay.Services.Navigation;
using WeatherDisplay.Utils;
using Place = WeatherDisplay.Pages.MeteoSwiss.MeteoSwissPlace;

namespace WeatherDisplay.Pages.MeteoSwiss
{
    public class MeteoSwissWeatherPage : INavigatedTo, INavigatedFrom
    {
        private readonly ILogger logger;
        private readonly IDisplayManager displayManager;
        private readonly IMeteoSwissWeatherService meteoSwissWeatherService;
        private readonly ISwissMetNetService swissMetNetService;
        private readonly IDateTime dateTime;
        private readonly IAppSettings appSettings;
        private readonly IOptionsMonitor<MeteoSwissWeatherPageOptions> options;
        private readonly ISensorAccessService sensorAccessService;
        private readonly IWeatherIconMapping weatherIconMapping;
        private Place currentPlace = null;

        public MeteoSwissWeatherPage(
            ILogger<MeteoSwissWeatherPage> logger,
            IDisplayManager displayManager,
            IMeteoSwissWeatherService openWeatherMapService,
            ISwissMetNetService swissMetNetService,
            IDateTime dateTime,
            IAppSettings appSettings,
            IOptionsMonitor<MeteoSwissWeatherPageOptions> options,
            ISensorAccessService sensorAccessService)
        {
            this.logger = logger;
            this.displayManager = displayManager;
            this.meteoSwissWeatherService = openWeatherMapService;
            this.swissMetNetService = swissMetNetService;
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
                throw new Exception(Translations.MeteoSwissWeatherPage_ErrorMissingPlacesConfiguration);
            }

            // Date header
            this.displayManager.AddRenderActionsAsync(
                async () =>
                {
                    var weatherStation = await this.swissMetNetService.GetWeatherStationAsync(this.currentPlace.WeatherStation);

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
                            Value = $"{Translations.MeteoSwissWeatherPage_SourceName} / {weatherStation.StationCode} / v{fvi.ProductVersion}",
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
                        var latestMeasurement = await this.swissMetNetService.GetLatestMeasurementAsync(this.currentPlace.WeatherStation, cacheExpiration: TimeSpan.FromMinutes(20));

                        var weatherInfo = await this.meteoSwissWeatherService.GetCurrentWeatherAsync(this.currentPlace.Plz);
                        var currentWeatherInfo = weatherInfo.CurrentWeather;

                        var forecastInfo = await this.meteoSwissWeatherService.GetForecastAsync(this.currentPlace.Plz);
                        var dailyForecasts = forecastInfo.Forecast.OrderBy(f => f.DayDate).ToList();
                        var dailyForecastToday = dailyForecasts.First();

                        var currentWeatherImage = await this.meteoSwissWeatherService.GetWeatherIconAsync(currentWeatherInfo.IconV2, this.weatherIconMapping);

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
                            new RenderActions.SvgImage
                            {
                                X = 20,
                                Y = 198,
                                Image = currentWeatherImage,
                                Width = 92,
                                Height = 92,
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
                                    Value = $"{currentWeatherInfo.Temperature.Value:N0}",
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
                                    Value = $"{currentWeatherInfo.Temperature:A0}",
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
                                    Value = $"/ {latestMeasurement.RelativeAirHumidity.Value.Value:0}% {Translations.RelativeHumiditySuffix}",
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
                        if (weatherInfo.Warnings.Any())
                        {
                            var mostImportantAlert = weatherInfo.Warnings
                                .OrderBy(a => a.ValidFrom >= dateTimeNow && a.ValidTo <= dateTimeNow)
                                .ThenBy(a => a.ValidFrom)
                                .First();

                            var alertDisplayText = $"{mostImportantAlert.WarnType} ({mostImportantAlert.WarnLevel.Level}/{mostImportantAlert.WarnLevel})";
                            if (weatherInfo.Warnings.Count > 1)
                            {
                                alertDisplayText += $" (+{weatherInfo.Warnings.Count - 1})";
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
                            currentWeatherRenderActions.Add(
                                new RenderActions.Text
                                {
                                    X = 20,
                                    Y = 300,
                                    HorizontalTextAlignment = HorizontalAlignment.Left,
                                    VerticalTextAlignment = VerticalAlignment.Top,
                                    Value = weatherInfo.CurrentWeather.WeatherCondition.ToString("G", this.appSettings.CultureInfo),
                                    ForegroundColor = "#000000",
                                    BackgroundColor = "#FFFFFF",
                                    FontSize = 20,
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
                                Value = $"{weatherInfo.Graph.Sunrise.First().ToLocalTime():t}",
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
                                Value = $"{weatherInfo.Graph.Sunset.First().ToLocalTime():t}",
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
                                Value = FormatTemperature(dailyForecastToday.TemperatureMin),
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
                                Value = FormatTemperature(dailyForecastToday.TemperatureMax),
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
                                Value = MeteoFormatter.FormatPrecipitation(dailyForecastToday.Precipitation),
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
                                Value = MeteoFormatter.FormatWind(latestMeasurement.WindSpeed, latestMeasurement.WindDirection),
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
                                Value = $"{latestMeasurement.PressureQFE.Value:N0}", // ({latestMeasurement.Pressure.Range:N})",
                                ForegroundColor = "#000000",
                                BackgroundColor = "#FFFFFF",
                                FontSize = 20,
                                Bold = false,
                            }
                        });

                        if (co2 != null)
                        {
                            currentWeatherRenderActions.AddRange(new IRenderAction[]
                            {
                                // CO2
                                new RenderActions.BitmapImage
                                {
                                    X = 500,
                                    Y = 260,
                                    Image = Icons.IndoorEmpty(),
                                    Width = 24,
                                    Height = 24,
                                    HorizontalAlignment = HorizontalAlignment.Left,
                                    VerticalAlignment = VerticalAlignment.Top,
                                },
                                new RenderActions.Text
                                {
                                    X = 500 + 24,
                                    Y = 260 + 14,
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
                                    Y = 260 + 5,
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
                                    Value = $"{dailyForecastToday.IconDayV2}",
                                    ForegroundColor = "#000000",
                                    BackgroundColor = "#FFFFFF",
                                    FontSize = 12,
                                    Bold = true,
                                });
                        }

                        // Display daily weather forecast
                        var numberOfForecastItems = 7;
                        dailyForecasts = dailyForecasts.Take(numberOfForecastItems).ToList();

                        var spacing = 20;
                        var widthPerDailyForecast = (800 - ((numberOfForecastItems + 1) * spacing)) / numberOfForecastItems;
                        var xOffset = spacing;

                        for (var i = 0; i < dailyForecasts.Count; i++)
                        {
                            var dailyWeatherForecast = dailyForecasts[i];
                            var xCenter = xOffset + (widthPerDailyForecast / 2);

                            var dailyWeatherImage = await this.meteoSwissWeatherService.GetWeatherIconAsync(dailyWeatherForecast.IconDayV2, this.weatherIconMapping);

                            var dailyWeatherRenderActions = new List<IRenderAction>
                            {
                                new RenderActions.Text
                                {
                                    X = xCenter,
                                    Y = 360,
                                    HorizontalTextAlignment = HorizontalAlignment.Center,
                                    VerticalTextAlignment = VerticalAlignment.Top,
                                    Value = $"{dailyWeatherForecast.DayDate:ddd}",
                                    ForegroundColor = "#000000",
                                    BackgroundColor = "#FFFFFF",
                                    FontSize = 20,
                                    Bold = true,
                                },
                                new RenderActions.SvgImage
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
                                    Value = $"{dailyWeatherForecast.TemperatureMin.Value:N0}/{dailyWeatherForecast.TemperatureMax.Value:N0}{dailyWeatherForecast.TemperatureMax:A0}",
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
                                        Value = $"{dailyWeatherForecast.IconDayV2}",
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
                //this.displayManager.AddRenderActions(() =>
                //{
                //    return new IRenderAction[]
                //    {
                //        new RenderActions.Text
                //        {
                //            X = 20,
                //            Y = 120,
                //            HorizontalTextAlignment = HorizontalAlignment.Left,
                //            VerticalTextAlignment = VerticalAlignment.Top,
                //            Value = Translations.MeteoSwissWeatherPage_ErrorMissingPlacesConfigurationLine1,
                //            ForegroundColor = "#000000",
                //            BackgroundColor = "#FFFFFF",
                //            FontSize = 20,
                //        },
                //        new RenderActions.Text
                //        {
                //            X = 20,
                //            Y = 140,
                //            HorizontalTextAlignment = HorizontalAlignment.Left,
                //            VerticalTextAlignment = VerticalAlignment.Top,
                //            Value = Translations.MeteoSwissWeatherPage_ErrorMissingPlacesConfigurationLine2,
                //            ForegroundColor = "#000000",
                //            BackgroundColor = "#FFFFFF",
                //            FontSize = 20,
                //        },
                //    };
                //});
            }

            return Task.CompletedTask;
        }

        public Task OnNavigatedFromAsync(INavigationParameters parameters)
        {
            this.currentPlace = null;
            return Task.CompletedTask;
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
