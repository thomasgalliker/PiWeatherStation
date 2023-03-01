using System;
using System.Diagnostics;

namespace WeatherDisplay.Services.Astronomy
{
    /// <summary>
    /// The planetary K-index forecast for a three-hour interval.
    /// </summary>
    /// <remarks>
    /// https://www.swpc.noaa.gov/sites/default/files/images/u2/TheK-index.pdf
    /// </remarks>
    [DebuggerDisplay("{TimeTag}, KpIndex: {KpIndex},  Observed: {Observed},  NoaaScale: {NoaaScale}")]
    public class PlanetaryKIndexForecast
    {
        public DateTime TimeTag { get; set; }

        /// <summary>
        /// The K-index is a code that is related to the maximum fluctuations of horizontal components
        /// observed on a magnetometer relative to a quiet day, during a three-hour interval.
        /// </summary>
        public decimal KpIndex { get; set; }

        public string Observed { get; set; }

        public string NoaaScale { get; set; }
    }
}