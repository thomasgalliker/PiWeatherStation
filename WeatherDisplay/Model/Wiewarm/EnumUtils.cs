using System.ComponentModel;

namespace WeatherDisplay.Model.Wiewarm
{
    internal static class EnumUtils
    {
        internal static string GetDescription<T>(T enumValue, string @default = null)
        {
            var fi = enumValue.GetType().GetField(enumValue.ToString());
            if (fi != null)
            {
                var attrs = fi.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return @default;
        }
    }
}