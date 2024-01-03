using System;
using System.Linq;
using WeatherDisplay.Extensions;

namespace WeatherDisplay
{
    public static class App
    {
        public static class Pages
        {
            private static readonly string[] SystemPages = { SetupPage, SystemInfoPage };

            public static bool IsSystemPage(string currentPage)
            {
                return SystemPages.Contains(currentPage);
            }
            
            public static string GetNextSystemPage(string currentPage)
            {
                var page = SystemPages.GetNextElement(currentPage);
                return page;
            }

            public const string SetupPage = nameof(WeatherDisplay.Pages.SystemInfo.SetupPage);
            public const string SystemInfoPage = nameof(WeatherDisplay.Pages.SystemInfo.SystemInfoPage);
            public const string ErrorPage = nameof(WeatherDisplay.Pages.SystemInfo.ErrorPage);
            
            public const string OpenWeatherMapPage = nameof(WeatherDisplay.Pages.OpenWeatherMap.OpenWeatherMapPage);
            public const string TemperatureDiagramPage = nameof(WeatherDisplay.Pages.OpenWeatherMap.TemperatureDiagramPage);
            public const string MeteoSwissWeatherPage = nameof(WeatherDisplay.Pages.MeteoSwiss.MeteoSwissWeatherPage);
            public const string MeteoSwissWeatherStationPage = nameof(WeatherDisplay.Pages.MeteoSwiss.MeteoSwissWeatherStationPage);

        }
    }
}
