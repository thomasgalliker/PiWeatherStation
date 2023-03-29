using System.Collections.Generic;
using WeatherDisplay.Model.Settings;

namespace WeatherDisplay.Model
{
    public class TemperatureDiagramPageOptions
    {
        public TemperatureDiagramPageOptions()
        {
            this.Places = new List<Place>();
        }

        public ICollection<Place> Places { get; set; }
    }
}