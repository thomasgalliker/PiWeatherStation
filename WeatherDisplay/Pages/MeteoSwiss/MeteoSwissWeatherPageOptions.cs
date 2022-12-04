using System.Collections.Generic;

namespace WeatherDisplay.Pages.MeteoSwiss
{
    public class MeteoSwissWeatherPageOptions
    {
        public MeteoSwissWeatherPageOptions()
        {
            this.Places = new List<MeteoSwissPlace>();
        }

        public ICollection<MeteoSwissPlace> Places { get; set; }
    }
}