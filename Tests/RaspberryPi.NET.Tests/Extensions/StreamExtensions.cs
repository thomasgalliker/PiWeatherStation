using System.IO;

namespace RaspberryPi.Internals.Extensions
{
    internal static class StreamExtensions
    {
        public static string ReadToEnd(this MemoryStream stream)
        {
            stream.Position = 0;
            var streamReader = new StreamReader(stream);
            return streamReader.ReadToEnd();
        }
    }
}
