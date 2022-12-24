using System.Diagnostics;

namespace WeatherDisplay.Model
{
    [DebuggerDisplay("ButtonMapping: Page {this.Page} <-> Button {this.ButtonId}")]
    public class ButtonMapping
    {
        public string Page { get; set; }
        
        public int ButtonId { get; set; }
        
        public int GpioPin { get; set; }

        public bool Default { get; set; }
    }
}