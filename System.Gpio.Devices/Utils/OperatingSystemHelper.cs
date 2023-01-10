using System.Runtime.InteropServices;

namespace System.Device.Utils
{
    internal static class OperatingSystemHelper
    {
        internal static OSPlatform GetOperatingSystem()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return OSPlatform.OSX;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return OSPlatform.Linux;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return OSPlatform.Windows;
            }

            throw new NotSupportedException($"Operating system \"{RuntimeInformation.OSDescription}\" is not supported");
        }
    }
}
