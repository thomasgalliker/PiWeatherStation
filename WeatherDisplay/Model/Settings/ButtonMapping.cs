using System.Diagnostics;

namespace WeatherDisplay.Model.Settings
{
    [DebuggerDisplay(@"\{ButtonId = {ButtonId}, Page = {Page}\}")]
    public class ButtonMapping
    {
        public string Page { get; set; }

        public int ButtonId { get; set; }

        public int GpioPin { get; set; }

        public bool Default { get; set; }
    }
}