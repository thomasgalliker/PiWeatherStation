using System.Globalization;
using System.Reflection;

namespace WeatherDisplay.Api.Properties
{
    [AttributeUsage(AttributeTargets.Assembly)]
    internal class BuildDateTimeAttribute : Attribute
    {
        public string BuildTime { get; set; }

        public BuildDateTimeAttribute(string buildTime)
        {
            this.BuildTime = buildTime;
        }
    }

    internal static class AssemblyExtensions
    {
        internal static DateTime? GetAssemblyBuildDateTime()
        {
            var assembly = Assembly.GetExecutingAssembly();
            return assembly.GetBuildTime();
        }

        internal static DateTime? GetBuildTime(this Assembly assembly)
        {
            var attribute = Attribute.GetCustomAttribute(assembly, typeof(BuildDateTimeAttribute));
            if (attribute is BuildDateTimeAttribute buildDateTimeAttribute &&
                DateTime.TryParseExact(buildDateTimeAttribute.BuildTime, "O", null, DateTimeStyles.RoundtripKind, out var dateTime))
            {
                return dateTime;
            }
            else
            {
                return null;
            }
        }
    }
}