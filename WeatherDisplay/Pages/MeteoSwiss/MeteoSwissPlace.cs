using System.Diagnostics;

namespace WeatherDisplay.Pages.MeteoSwiss
{
    [DebuggerDisplay(@"\{Name = {Name}, Plz = {Plz}\}")]
    public class MeteoSwissPlace
    {
        public string Name { get; set; }

        public int Plz { get; set; }

        public string WeatherStationCode { get; set; }

        public bool IsCurrentPlace { get; set; }
    }
}