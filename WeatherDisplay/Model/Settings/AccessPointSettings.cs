using System.Diagnostics;

namespace WeatherDisplay.Model.Settings
{
    [DebuggerDisplay("AccessPointSettings: {this.SSID}")]
    public class AccessPointSettings
    {
        public string SSID { get; set; }

        public string PSK { get; set; }
    }
}
