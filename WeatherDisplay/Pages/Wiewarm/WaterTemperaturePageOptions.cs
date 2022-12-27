using System.Collections.Generic;

namespace WeatherDisplay.Pages.Wiewarm
{
    public class WaterTemperaturePageOptions
    {
        public WaterTemperaturePageOptions()
        {
            this.Places = new List<string>();
        }

        public ICollection<string> Places { get; set; }
    }
}