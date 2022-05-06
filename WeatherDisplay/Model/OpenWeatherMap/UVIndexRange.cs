using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WeatherDisplay.Resources.Strings;

namespace WeatherDisplay.Model.OpenWeatherMap
{
    public class UVIndexRange : Range<double>, IFormattable
    {
        public static readonly UVIndexRange Low = new UVIndexRange(nameof(Low), min: 0, max: 3, minInclusive: true, maxInclusive: false);
        public static readonly UVIndexRange Moderate = new UVIndexRange(nameof(Moderate), min: 3, max: 6, minInclusive: true, maxInclusive: false);
        public static readonly UVIndexRange High = new UVIndexRange(nameof(High), min: 6, max: 8, minInclusive: true, maxInclusive: false);
        public static readonly UVIndexRange VeryHigh = new UVIndexRange(nameof(VeryHigh), min: 8, max: 11, minInclusive: true, maxInclusive: false);
        public static readonly UVIndexRange Extreme = new UVIndexRange(nameof(Extreme), min: 11, max: double.MaxValue, minInclusive: true, maxInclusive: true);

        public static readonly IEnumerable<UVIndexRange> All = new List<UVIndexRange>
        {
            Low, Moderate, High, VeryHigh, Extreme
        };

        public static double MinValue = All.Min(x => x.Min);
        public static double MaxValue = All.Max(x => x.Max);

        private readonly string resourceId;

        private UVIndexRange(string resourceId, double min, double max, bool minInclusive, bool maxInclusive)
            : base(min, max, minInclusive, maxInclusive)
        {
            this.resourceId = resourceId;
        }

        public static UVIndexRange FromValue(double value)
        {
            foreach (var humidityRange in All)
            {
                if (humidityRange.InRange(value))
                {
                    return humidityRange;
                }
            }

            throw new ArgumentOutOfRangeException(nameof(value), $"Value must be between {MinValue} and {MaxValue}");
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
                    var str = UVIndexRanges.ResourceManager.GetString(this.resourceId, (CultureInfo)provider);
                    return str;
            }

        }
    }
}