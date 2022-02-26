using System.IO;

namespace DisplayService.Model
{
    public partial class RenderActions
    {
        public class StreamImage : Image
        {
            /// <summary>
            /// Stream of the image to be displayed.
            /// </summary>
            public Stream Image { get; set; }
        }
    }
}
