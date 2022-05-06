using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WeatherDisplay.Resources.Strings;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    public class AirQuality : IComparable, IComparable<AirQuality>, IComparable<int>, IFormattable
    {
        public static readonly AirQuality Good = new AirQuality(nameof(Good), 1);
        public static readonly AirQuality Fair = new AirQuality(nameof(Fair), 2);
        public static readonly AirQuality Moderate = new AirQuality(nameof(Moderate), 3);
        public static readonly AirQuality Poor = new AirQuality(nameof(Poor), 4);
        public static readonly AirQuality VeryPoor = new AirQuality(nameof(VeryPoor), 5);

        public static readonly IEnumerable<AirQuality> All = new List<AirQuality>
        {
            Good, Fair, Moderate, Poor, VeryPoor
        };

        private readonly string resourceId;

        private AirQuality(string resourceId, int value)
        {
            this.resourceId = resourceId;
            this.Value = value;
        }

        public int Value { get; private set; }

        public static AirQuality FromValue(int value)
        {
            var airQuality = All.SingleOrDefault(x => x.Value == value);
            if (airQuality == null)
            {
                throw new ArgumentOutOfRangeException(nameof(value), $"Value must be between {All.Min(x => x.Value)} and {All.Max(x => x.Value)}");
            }

            return airQuality;
        }

        public static bool operator >(AirQuality a1, AirQuality a2)
        {
            return a1.Value > a2.Value;
        }

        public static bool operator <(AirQuality a1, AirQuality a2)
        {
            return a1.Value < a2.Value;
        }

        public int CompareTo(object obj)
        {
            if (obj is AirQuality airQuality)
            {
                return this.Value.CompareTo(airQuality.Value);
            }

            return this.Value.CompareTo(obj);
        }

        public int CompareTo(AirQuality other)
        {
            return this.Value.CompareTo(other.Value);
        }

        public int CompareTo(int other)
        {
            return this.Value.CompareTo(other);
        }

        public override string ToString()
        {
            return this.ToString("N", CultureInfo.CurrentCulture);
        }

        public string ToString(string format)
        {
            return this.ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            if (string.IsNullOrEmpty(format))
            {
                format = "N";
            }

            if (provider == null)
            {
                provider = CultureInfo.CurrentCulture;
            }

            switch (format)
            {
                case "I":
                    return base.ToString();
                case "N":
                default:
                    var str = AirQualityTranslations.ResourceManager.GetString(this.resourceId, (CultureInfo)provider);
                    return str;
            }

        }
    }
}