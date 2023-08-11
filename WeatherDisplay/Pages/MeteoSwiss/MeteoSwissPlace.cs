using System.Diagnostics;

namespace WeatherDisplay.Pages.MeteoSwiss
{
    [DebuggerDisplay("{this.Name}")]
    public class MeteoSwissPlace
    {
        public string Name { get; set; }

        public int Plz { get; set; }

        public string WeatherStation { get; set; }

        public bool IsCurrentPlace { get; set; }
    }
}