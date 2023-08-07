using System.IO;
using DisplayService.Internals;
using Microsoft.Extensions.Logging;

namespace DisplayService.Devices
{
    public class NullDisplay : IDisplay
    {
        private readonly ILogger logger;

        private readonly SyncHelper syncHelper = new SyncHelper();

        public NullDisplay(ILogger<NullDisplay> logger)
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
            this.syncHelper.RunOnce(() =>
            {
                this.logger.LogDebug("DisplayImage");
            });
        }

        public void Dispose()
        {
            this.logger.LogDebug("Dispose");
        }
    }
}
