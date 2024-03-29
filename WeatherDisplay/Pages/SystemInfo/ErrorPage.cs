﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using DisplayService.Model;
using DisplayService.Services;
using NCrontab;
using WeatherDisplay.Model.Settings;
using WeatherDisplay.Services.Navigation;

namespace WeatherDisplay.Pages.SystemInfo
{
    public class ErrorPage : INavigatedTo
    {
        private readonly IDisplayManager displayManager;
        private readonly IDateTime dateTime;
        private readonly IAppSettings appSettings;

        public ErrorPage(
            IDisplayManager displayManager,
            IDateTime dateTime,
            IAppSettings appSettings)
        {
            this.displayManager = displayManager;
            this.dateTime = dateTime;
            this.appSettings = appSettings;
        }

        public class NavigationParameters : INavigationParameters
        {
            public Exception Exception { get; set; }
        }

        public Task OnNavigatedToAsync(INavigationParameters navigationParameters)
        {
            var parameters = (NavigationParameters)navigationParameters;

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

            // Error info
            this.displayManager.AddRenderActions(
                () =>
                {
                    var errorInfos = new List<IRenderAction>
                    {
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
                            Value = $"Ein Fehler ist aufgetreten.",
                            ForegroundColor = "#000000",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 20,
                            Bold = true,
                        },
                        new RenderActions.Text
                        {
                            X = 20,
                            Y = 160,
                            HorizontalTextAlignment = HorizontalAlignment.Left,
                            VerticalTextAlignment = VerticalAlignment.Top,
                            Value = $"Bitte Internetverbindung prüfen und Gerät ggf. neustarten.",
                            ForegroundColor = "#000000",
                            BackgroundColor = "#FFFFFF",
                            FontSize = 20,
                        },
                    };

                    if (this.appSettings.IsDebug)
                    {
                        errorInfos.Add(
                            new RenderActions.Text
                            {
                                X = 20,
                                Y = 200,
                                HorizontalTextAlignment = HorizontalAlignment.Left,
                                VerticalTextAlignment = VerticalAlignment.Top,
                                Value = parameters.Exception.Message,
                                ForegroundColor = "#000000",
                                BackgroundColor = "#FFFFFF",
                                FontSize = 20,
                            });
                    }

                    return errorInfos;
                });

            return Task.CompletedTask;
        }
    }
}
