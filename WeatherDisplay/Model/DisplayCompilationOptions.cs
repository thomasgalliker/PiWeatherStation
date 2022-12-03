using System.Diagnostics;

namespace WeatherDisplay.Model
{
    [DebuggerDisplay("ButtonMapping: {this.Name}, Button {this.ButtonId}")]
    public class ButtonMapping
    {
        public string DisplayCompilation { get; set; }
        
        public int ButtonId { get; set; }
    }
}