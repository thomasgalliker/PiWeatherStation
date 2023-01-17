using System;

namespace WeatherDisplay.Services.Astronomy
{
    public class PlanetaryKIndexForecast
    {
        public DateTime TimeTag { get; set; }

        public decimal KpIndex { get; set; }

        public string Observed { get; set; }

        public string NoaaScale { get; set; }
    }
}