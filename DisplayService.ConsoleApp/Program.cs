using System;
using System.Collections.Generic;
using System.Timers;
using DisplayService.Model;
using DisplayService.Services;
using DisplayService.Settings;

namespace DisplayService.ConsoleApp
{
    class MainClass
    {
        private static IDisplay displayService;
        private static IRenderService renderService;
        private static ICacheService cacheService;

        public static void Main(string[] args)
        {
            // Setup services, wire-up dependencies
            // TODO: Use Microsoft DependencyInjection
            //displayService = new WsEPaperDisplayService("WaveShare7In5_V2");

            displayService = new NullDisplayService();

            IRenderSettings renderSettings = new RenderSettings(); // TODO: Load from appsettings
            renderSettings.Resize(displayService.Width, displayService.Height);

            var displayManager = new DisplayManager(renderSettings, displayService);
            /*
            displayManager.AddRenderActions(
                new List<Func<IRenderAction>>
                {
                    () => new RenderActions.Clear(),
                    () => new RenderActions.Text
                    {
                        X = 0,
                        Y = 0,
                        Value = $"{DateTime.Now:O}",
                        FontSize = 40,
                    },
                    () => new RenderActions.Text
                    {
                        X = 0,
                        Y = 60,
                        Value = $"Test",
                        FontSize = 40,
                    },
                },
                TimeSpan.FromSeconds(20));
            */
            displayManager.AddRenderActions(
                new List<Func<IRenderAction>>
                {
                    () => new RenderActions.Text
                    {
                        X = 0,
                        Y = 0,
                        HorizAlign = -1,
                        VertAlign = -1,
                        Value = $"Display Demo",
                        FontSize = 44,
                        Bold = true,
                    },
                },
                TimeSpan.FromDays(1));
            displayManager.AddRenderActions(
                new List<Func<IRenderAction>>
                {
                    () => new RenderActions.Text
                    {
                        X = 800,
                        Y = 11,
                        HorizAlign = 1,
                        VertAlign = -1,
                        Value = $"{DateTime.Now:G}",
                        FontSize = 22,
                    },
                },
                TimeSpan.FromSeconds(20));
            displayManager.Start();

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();

            /*
            cacheService = new CacheService();
            renderService = new RenderService((RenderSettings)renderSettings, cacheService);

            // TODO: InitializeFromCache()
            if (cacheService.Exists())
            {
                renderService.Image(new RenderActions.Image
                {
                    X = 0,
                    Y = 0,
                    Filename = cacheService.CacheFile
                });
            }

            // Initialize display
            UpdateDisplay();

            // Start timer to automatically refresh the display
            ITimerService updateTimer = new TimerService()
            {
                TargetMillisecond = 1000,
                ToleranceMillisecond = 0,
                Enabled = true
            };
            updateTimer.Elapsed += OnUpdateTimerElapsed;
            updateTimer.Start();

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();

            updateTimer.Elapsed -= OnUpdateTimerElapsed;
            updateTimer.Stop();
            */
        }

        private static void OnUpdateTimerElapsed(object sender, ElapsedEventArgs e)
        {
            UpdateDisplay();
        }

        private static void UpdateDisplay()
        {
            try
            {
                renderService.Clear();
                renderService.Text(new RenderActions.Text
                {
                    X = 0,
                    Y = 0,
                    Value = $"{DateTime.Now:O}",
                    FontSize = 40,
                });
                renderService.Text(new RenderActions.Text
                {
                    X = 0,
                    Y = 60,
                    Value = $"Test",
                    FontSize = 40,
                });

                var bitmapStream = renderService.GetScreen(); // TODO: Use using/Dispose
                displayService.DisplayImage(bitmapStream);
                cacheService.SaveToCache(bitmapStream);
                bitmapStream.Close(); // TODO Remove?
                bitmapStream.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An exception occurred trying to update display. " + ex.Message);
            }
        }
    }
}
