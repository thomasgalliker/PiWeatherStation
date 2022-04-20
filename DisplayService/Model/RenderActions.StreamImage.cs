using System.Diagnostics;
using System.IO;

namespace DisplayService.Model
{
    public partial class RenderActions
    {
        [DebuggerDisplay("StreamImage: X={X}, Y={Y}")]
        public class StreamImage : Image
        {
            /// <summary>
            /// Stream of the image to be displayed.
            /// </summary>
            public Stream Image { get; set; }
        }
    }
}
