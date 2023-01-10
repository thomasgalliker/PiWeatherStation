using System;
using UnitsNet;
using WeatherDisplay.Resources.Strings;

namespace WeatherDisplay.Utils
{
    /// <summary>
    /// Volatile organic compounds (VOCs) are the major pollutants in indoor air, which significantly impact indoor air quality and thus influencing human health.
    /// </summary>
    /// <remarks>
    /// https://en.wikipedia.org/wiki/Volatile_organic_compound
    /// https://en.wikipedia.org/wiki/Air_quality_index#CAQI
    /// https://aqicn.org/scale/
    /// </remarks>
    internal static class IndoorAirQuality
    {
        private static readonly double HumidityScoreRatio = 0.25d;
        private static readonly double GasResistanceScoreRatio = 1d - HumidityScoreRatio;

        private static readonly RelativeHumidity OptimalHumidity = RelativeHumidity.FromPercent(40);
        private static readonly RelativeHumidity OptimalHumidityLow = RelativeHumidity.FromPercent(38);
        private static readonly RelativeHumidity OptimalHumidityHigh = RelativeHumidity.FromPercent(42);

        private static readonly ElectricResistance GasResistanceLow = ElectricResistance.FromKiloohms(5);
        private static readonly ElectricResistance GasResistanceHigh = ElectricResistance.FromKiloohms(50);

        /// <summary>
        /// Calculates the indoor air quality index (IAQ) from <paramref name="humidity"/> and <paramref name="gasResistance"/>.
        /// Humidity score is weighed 25% while the gas resistance reading is wighted 75%.
        /// </summary>
        /// <param name="humidity"></param>
        /// <param name="gasResistance"></param>
        /// <returns></returns>
        public static Ratio CalculateIAQ(RelativeHumidity humidity, ElectricResistance gasResistance)
        {
            double humidityScore;

            if (humidity >= OptimalHumidityLow && humidity <= OptimalHumidityHigh)
            {
                humidityScore = HumidityScoreRatio * 100d;
            }
            else
            {
                if (humidity < OptimalHumidityLow)
                {
                    humidityScore = HumidityScoreRatio / OptimalHumidity.Value * humidity.Value * 100d;
                }
                else
                {
                    humidityScore = ((-HumidityScoreRatio / (100d - OptimalHumidity.Value) * humidity.Value) + (5d / 12d)) * 100d;
                }
            }

            if (gasResistance > GasResistanceHigh)
            {
                gasResistance = GasResistanceHigh;
            }

            if (gasResistance < GasResistanceLow)
            {
                gasResistance = GasResistanceLow;
            }

            var gasScore = ((GasResistanceScoreRatio / (GasResistanceHigh.Value - GasResistanceLow.Value) * gasResistance.Value) - (GasResistanceLow.Value * (GasResistanceScoreRatio / (GasResistanceHigh.Value - GasResistanceLow.Value)))) * 100d;

            var iaq = (int)(humidityScore + gasScore);
            return Ratio.FromPercent(iaq);
        }

        public static string GetIAQRange(Ratio iaq)
        {
            if (iaq == null)
            {
                throw new ArgumentNullException(nameof(iaq));
            }

            if (iaq.Value < 0 || iaq.Value > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(iaq));
            }

            var score500 = (100 - iaq.Value) * 5;

            if (score500 >= 0 && score500 <= 50)
            {
                return IndoorAirQualityStrings.Good;
            }
            else if (score500 >= 51 && score500 <= 100)
            {
                return IndoorAirQualityStrings.Moderate;
            }
            else if (score500 >= 101 && score500 <= 150)
            {
                return IndoorAirQualityStrings.UnhealthyForSensitiveGroups;
            }
            else if (score500 >= 151 && score500 <= 200)
            {
                return IndoorAirQualityStrings.Unhealthy;
            }
            else if (score500 >= 201 && score500 <= 300)
            {
                return IndoorAirQualityStrings.VeryUnhealthy;
            }

            return IndoorAirQualityStrings.Hazardous;
        }
    }
}
