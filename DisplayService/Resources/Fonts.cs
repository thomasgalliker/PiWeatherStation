using System.IO;
using System.Reflection;

namespace DisplayService.Resources
{
    public static class Fonts
    {
        private static readonly Assembly Assembly = typeof(Fonts).Assembly;

        public static Stream GetFont(string filename)
        {
            return ResourceLoader.Current.GetEmbeddedResourceStream(Assembly, filename);
        }
    }
}
