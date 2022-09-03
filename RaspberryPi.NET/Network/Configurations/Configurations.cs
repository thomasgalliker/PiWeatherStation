using System.IO;
using System.Reflection;
using RaspberryPi.Internals.ResourceLoader;

namespace RaspberryPi.Network
{
    internal static class Configurations
    {
        private static readonly Assembly CurrentAssembly = typeof(Configurations).Assembly;

        public static Stream GetDnsmasqTemplateStream()
        {
            return ResourceLoader.Current.GetEmbeddedResourceStream(CurrentAssembly, "dnsmasq.conf");
        }

        public static Stream GetHostapdTemplateStream()
        {
            return ResourceLoader.Current.GetEmbeddedResourceStream(CurrentAssembly, "hostapd.conf");
        }
    }
}
