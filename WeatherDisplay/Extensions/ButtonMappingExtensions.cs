using System.Collections.Generic;
using System.Linq;
using WeatherDisplay.Model.Settings;

namespace WeatherDisplay.Extensions
{
    public static class ButtonMappingExtensions
    {
        public static ButtonMapping GetDefaultButtonMapping(this IEnumerable<ButtonMapping> buttonMappings)
        {
            return buttonMappings
                .OrderBy(m => m.ButtonId)
                .FirstOrDefault(m => m.Default) ?? buttonMappings.First();
        }
    }
}
