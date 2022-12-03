using System.Collections.Generic;

namespace WeatherDisplay.Model.MeteoSwiss
{
    public class MeteoSwissWeatherDisplayCompilationOptions
    {
        public MeteoSwissWeatherDisplayCompilationOptions()
        {
            this.Places = new List<MeteoSwissPlace>();
        }

        public ICollection<MeteoSwissPlace> Places { get; set; }
    }
}