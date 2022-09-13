using System.IO;
using Microsoft.Extensions.Logging;

namespace DisplayService.Services
{
    public class NullDisplayService : IDisplay
    {
        private readonly ILogger<NullDisplayService> logger;

        public NullDisplayService(ILogger<NullDisplayService> logger)
        {
            this.logger = logger;
        }

        public int Width => 800;

        public int Height => 480;

        public void Clear()
        {
            this.logger.LogDebug("Clear");
        }

        public void DisplayImage(Stream bitmapStream)
        {
            this.logger.LogDebug("DisplayImage");
        }

        public void Dispose()
        {
            this.logger.LogDebug("Dispose");
        }
    }
}
