using DisplayService.Services;

namespace WeatherDisplay.Services
{
    public class ShutdownService : IShutdownService
    {
        private readonly RaspberryPi.Services.IShutdownService shutdownService;
        private readonly IDisplayManager displayManager;

        public ShutdownService(
            RaspberryPi.Services.IShutdownService shutdownService,
            IDisplayManager displayManager)
        {
            this.shutdownService = shutdownService;
            this.displayManager = displayManager;
        }

        public void Shutdown()
        {
            this.displayManager.Reset();

            this.shutdownService.Shutdown();
        }

        public void Reboot()
        {
            this.displayManager.Reset();

            this.shutdownService.Reboot();
        }
    }
}
