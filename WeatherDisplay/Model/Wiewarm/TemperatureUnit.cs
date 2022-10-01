using System.ComponentModel;

namespace WeatherDisplay.Model.Wiewarm
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