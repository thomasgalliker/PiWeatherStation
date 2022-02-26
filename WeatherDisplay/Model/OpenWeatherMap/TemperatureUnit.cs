using System.ComponentModel;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    public enum TemperatureUnit
    {
        [Description("K")]
        Kelvin = 0,

        [Description("°C")]
        Celsius,

        [Description("°F")]
        Fahrenheit,
    }
}