using System.Collections.Generic;

namespace WeatherDisplay.Model
{
    public class WaterTemperatureDisplayCompilationOptions
    {
        public WaterTemperatureDisplayCompilationOptions()
        {

            this.Places = new List<Place>();
        }

        public ICollection<Place> Places { get; set; }
    }
}