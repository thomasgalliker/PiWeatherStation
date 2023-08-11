using System.IO;
using System.Reflection;

namespace DisplayService.Resources
{
    public static class BitmapImages
    {
        private static readonly Assembly Assembly = typeof(BitmapImages).Assembly;

        public static Stream GetTestImage1()
        {
            return ResourceLoader.Current.GetEmbeddedResourceStream(Assembly, "TestImage1.png");
        }

        public static Stream GetTestImage2()
        {
            return ResourceLoader.Current.GetEmbeddedResourceStream(Assembly, "TestImage2.png");
        }
    }
}
