using System.Diagnostics;
using System.Reflection;

namespace WeatherDisplay.Utils
{
    internal static class FileVersionInfoHelper
    {
        internal static string GetProductVersion(bool displayGitHash = false)
        {
            return GetProductVersion(null, displayGitHash);
        }

        internal static string GetProductVersion(Assembly assembly = null, bool displayGitHash = false)
        {
            if (assembly == null)
            {
                assembly = Assembly.GetExecutingAssembly();
            }

            var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            var productVersion = fvi.ProductVersion;

            if (productVersion.Contains("."))
            {
                var split = productVersion.Split('+');

                if (displayGitHash)
                {
                    return $"{split[0]} ({split[1].Substring(0, 8)})";
                }

                return split[0];
            }

            return productVersion;
        }
    }
}
