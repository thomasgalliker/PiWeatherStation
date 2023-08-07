using System.Collections.Generic;
using System.Globalization;

namespace WeatherDisplay.Model.Settings
{
    public interface IAppSettings
    {
        CultureInfo CultureInfo { get; set; }

        bool RunSetup { get; set; }

        bool IsDebug { get; set; }

        AccessPointSettings AccessPoint { get; set; }

        ICollection<ButtonMapping> ButtonMappings { get; set; }
    }
}