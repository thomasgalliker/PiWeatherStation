using System.Diagnostics;

namespace WeatherDisplay.Pages.MeteoSwiss
{
    [DebuggerDisplay("{this.Name}")]
    public class MeteoSwissPlace
    {
        public string Name { get; set; }

        public int Plz { get; set; }
    }
}