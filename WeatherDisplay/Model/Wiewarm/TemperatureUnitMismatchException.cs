using System;

namespace WeatherDisplay.Model.Wiewarm
{
    public class TemperatureUnitMismatchException : Exception
    {
        internal TemperatureUnitMismatchException(TemperatureUnit temperatureUnit1, TemperatureUnit temperatureUnit2)
           : base($"This operation cannot be performed between {temperatureUnit1} and {temperatureUnit2}.")
        { }
    }
}