using System.IO;
using System.Reflection;
using RaspberryPi.Internals.ResourceLoader;

namespace RaspberryPi.NET.Tests.TestData
{
    public static class Files
    {
        private static readonly Assembly Assembly = typeof(Files).Assembly;

        public static Stream GetWPASupplicantConfStream()
        {
            return ResourceLoader.Current.GetEmbeddedResourceStream(Assembly, "wpa_supplicant.conf");
        }

        public static Stream GetDhcpdConfStream()
        {
            return ResourceLoader.Current.GetEmbeddedResourceStream(Assembly, "dhcpcd.conf");
        }
    }
}
