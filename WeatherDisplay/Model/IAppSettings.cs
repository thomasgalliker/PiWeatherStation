using System.Collections.Generic;
using System.Globalization;

namespace WeatherDisplay.Model
{
    public interface IAppSettings
    {
        string Title { get; set; }

        CultureInfo CultureInfo { get; set; }

        bool IsDebug { get; set; }

        WaterTemperatureDisplayCompilationOptions WaterTemperatureDisplayCompilation { get; set; }

        OpenWeatherDisplayCompilationOptions OpenWeatherDisplayCompilation { get; set; }

        ICollection<DisplaySetting> Displays { get; set; }
        
        ICollection<ButtonMapping> ButtonMappings { get; set; }
    }
}