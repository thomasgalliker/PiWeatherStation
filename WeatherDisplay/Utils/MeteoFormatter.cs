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
            var formattedTemperatureValue = FormatTemperature(temperature?.Value, decimalPlaces);

            if (temperature is not Temperature temperatureValue)
            {
                return formattedTemperatureValue;
            }

            return $"{formattedTemperatureValue} {temperatureValue:A}";
        }

        internal static string FormatTemperature(double? temperature, int decimalPlaces = 0)
        {
            if (temperature is not double temperatureValue)
            {
                return "-";
            }

            return $"{((decimal)temperatureValue).ToString($"N{decimalPlaces}")}";
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

            var formattedWindSpeed = $"{windSpeedString}{(windDirection is Angle windDirectionValue ? $" ({AngleExtensions.ToIntercardinalWindDirection(windDirectionValue)})" : "")}";
            return formattedWindSpeed;
        }

        internal static string FormatPrecipitation(Length? precipitation, Ratio? pop = null)
        {
            if (precipitation is not Length precipitationValue)
            {
                return "-";
            }

            string precipitationString = null;

            if (precipitationValue.Value is > 0d and < 1d)
            {
                precipitationString = $"< {Length.From(1d, precipitationValue.Unit):N0}";
            }
            else
            {
                precipitationString = $"{(decimal)precipitationValue.Value:N0} {precipitationValue:A}";
            }

            var formattedPrecipitation = $"{precipitationString}{(pop is Ratio popValue ? $" ({popValue})" : "")}";
            return formattedPrecipitation;
        }

        internal static string FormatPressure(Pressure? pressure)
        {
            if (pressure is not Pressure pressureValue)
            {
                return "-";
            }

            return $"{(decimal)pressureValue.Value:N0} {pressureValue:A} ({PressureExtensions.GetRange(pressureValue):N})";
        }

        internal static string FormatHumidity(RelativeHumidity? relativeHumidity, int decimalPlaces = 0)
        {
            if (relativeHumidity is not RelativeHumidity relativeHumidityValue)
            {
                return "-";
            }

            return $"{((decimal)relativeHumidityValue.Value).ToString($"N{decimalPlaces}")} {relativeHumidityValue:A}";
        }
    }
}
