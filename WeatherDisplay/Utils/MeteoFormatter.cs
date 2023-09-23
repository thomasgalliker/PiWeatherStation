using System;
using UnitsNet;
using AngleExtensions = OpenWeatherMap.Extensions.AngleExtensions;
using PressureExtensions = OpenWeatherMap.Extensions.PressureExtensions;

namespace WeatherDisplay.Utils
{
    internal static class MeteoFormatter
    {
        internal static string FormatTemperature(Temperature? temperature, int decimalPlaces = 0)
        {
            if (temperature is not Temperature temperatureValue)
            {
                return "-";
            }

            return $"{temperatureValue.ToString($"N{decimalPlaces}")}";
        }

        internal static string FormatWind(Speed? windSpeed, Angle? windDirection)
        {
            if (windSpeed is not Speed windSpeedValue)
            {
                return "-";
            }

            string windSpeedString = null;

            if (windSpeedValue.Value <= 0d)
            {
                windSpeedString = $"{Speed.From(0d, windSpeedValue.Unit):G0}";
            }
            else if (windSpeedValue.Value is > 0d and < 1d)
            {
                windSpeedString = $"< {Speed.From(1d, windSpeedValue.Unit):G0}";
            }
            else
            {
                windSpeedString = $"{windSpeedValue:G1}";
            }

            var value = $"{windSpeedString}{(windDirection is Angle windDirectionValue ? $" ({AngleExtensions.ToIntercardinalWindDirection(windDirectionValue)})" : "")}";
            return value;
        }

        internal static string FormatPrecipitation(Length? precipitation, Ratio? pop = null)
        {
            if (precipitation is not Length precipitationValue)
            {
                return "-";
            }

            string precipitationString = null;

            if (precipitationValue.Value == 0d)
            {
                precipitationString = $"{precipitationValue:N0}";
            }
            else if (precipitationValue.Value is > 0d and < 1d)
            {
                precipitationString = $"< {Length.From(1d, precipitationValue.Unit):N0}";
            }
            else
            {
                precipitationString = $"{precipitationValue:N0}";
            }

            var value = $"{precipitationString}{(pop is Ratio popValue ? $" ({popValue})" : "")}";
            return value;
        }

        internal static string FormatPressure(Pressure? pressure)
        {
            if (pressure is not Pressure pressureValue)
            {
                return "-";
            }

            return $"{pressureValue:N0} ({PressureExtensions.GetRange(pressureValue):N})";
        }

        internal static string FormatHumidity(RelativeHumidity? relativeHumidity, int decimalPlaces = 0)
        {
            if (relativeHumidity is not RelativeHumidity relativeHumidityValue)
            {
                return "-";
            }

            return $"{relativeHumidityValue.ToString($"N{decimalPlaces}")}";
        }
    }
}
