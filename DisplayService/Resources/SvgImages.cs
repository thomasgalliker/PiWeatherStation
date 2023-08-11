using System.IO;
using System.Reflection;

namespace DisplayService.Resources
{
    public static class SvgImages
    {
        private static readonly Assembly Assembly = typeof(SvgImages).Assembly;

        public static Stream GetSvgImage1()
        {
            return ResourceLoader.Current.GetEmbeddedResourceStream(Assembly, "SvgImage1.svg");
        }
    }
}
