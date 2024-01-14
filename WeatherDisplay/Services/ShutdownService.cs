using System.Threading.Tasks;
using DisplayService.Services;
using Microsoft.Extensions.Logging;

namespace WeatherDisplay.Services
{
    public class ShutdownService : IShutdownService
    {
        private readonly ILogger<ShutdownService> logger;
        private readonly RaspberryPi.Services.IShutdownService shutdownService;
        private readonly IDisplayManager displayManager;

        public ShutdownService(
            ILogger<ShutdownService> logger,
            RaspberryPi.Services.IShutdownService shutdownService,
            IDisplayManager displayManager)
        {
            this.logger = logger;
            this.shutdownService = shutdownService;
            this.displayManager = displayManager;
        }

        public async void Shutdown()
        {
            this.logger.LogDebug("Shutdown");

            this.displayManager.Reset();

            await Task.Delay(3000);

            this.shutdownService.Shutdown();
        }

        public async void Reboot()
        {
            this.logger.LogDebug("Reboot");

            this.displayManager.Reset();

            await Task.Delay(3000);

            this.shutdownService.Reboot();
        }
    }
}
