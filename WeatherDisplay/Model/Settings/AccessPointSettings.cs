using System.Diagnostics;

namespace WeatherDisplay.Model.Settings
{
    [DebuggerDisplay(@"\{SSID = {SSID}\}")]
    public class AccessPointSettings
    {
        public string SSID { get; set; }

        public string PSK { get; set; }
    }
}
