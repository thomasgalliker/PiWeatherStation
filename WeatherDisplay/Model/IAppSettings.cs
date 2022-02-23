using System.Collections.Generic;
using System.Globalization;

namespace WeatherDisplay.Model
{
    public interface IAppSettings
    {
        string Title { get; set; }
        CultureInfo CultureInfo { get; set; }
        bool IsDebug { get; set; }
        List<AppSettings.DisplaySetting> Displays { get; set; }
        List<Place> Places { get; set; }
    }
}