using System.IO;
using System.Threading.Tasks;

namespace DisplayService.Extensions
{
    public static class StreamExtensions
    {
        /// <summary>
        /// Copy stream as new stream.
        /// </summary>
        /// <param name="stream">Stream to copy.</param>
        /// <returns>Copy of the original stream.</returns>
        public static async Task<Stream> CopyToAndRewindAsync(this Stream stream)
        {
            var targetStream = new MemoryStream();
            await stream.CopyToAndRewindAsync(targetStream);
            return targetStream;
        }

        /// <summary>
        /// Copy stream as new stream.
        /// </summary>
        /// <param name="stream">Stream to copy.</param>
        /// <param name="targetStream">Copy of the original stream.</param>
        public static async Task CopyToAndRewindAsync(this Stream stream, Stream targetStream)
        {
            stream.Rewind();
            await stream.CopyToAsync(targetStream);
            targetStream.Rewind();
        }

        /// <summary>
        /// Rewind stream to its start position.
        /// </summary>
        /// <param name="stream">Stream to rewind.</param>
        /// <returns>The input stream (rewinded).</returns>
        public static Stream Rewind(this Stream stream)
        {
            if (stream.CanSeek && stream.Position != 0)
            {
                stream.Seek(0L, SeekOrigin.Begin);
            }

            return stream;
        }
    }
}