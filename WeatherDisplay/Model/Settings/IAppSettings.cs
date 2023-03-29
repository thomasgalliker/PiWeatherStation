using System.Collections.Generic;
using System.Globalization;

namespace WeatherDisplay.Model.Settings
{
    public interface IAppSettings
    {
        string Title { get; set; }

        CultureInfo CultureInfo { get; set; }

        bool RunSetup { get; set; }

        bool IsDebug { get; set; }

        AccessPointSettings AccessPoint { get; set; }

        ICollection<DisplaySetting> Displays { get; set; }

        ICollection<ButtonMapping> ButtonMappings { get; set; }
    }
}