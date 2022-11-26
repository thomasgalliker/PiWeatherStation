using System.Device.Gpio;
using System.Gpio.Devices;
using System.Gpio.Devices.Buttons;
using WeatherDisplay.Compilations;
using WeatherDisplay.Model;

namespace WeatherDisplay.Api.Services
{
    public class WeatherDisplayHardwareCoordinator : IWeatherDisplayHardwareCoordinator, IDisposable
    {
        private readonly ILogger logger;
        private readonly IAppSettings appSettings;
        private readonly IGpioController gpioController;
        private readonly IDisplayCompilationService displayCompilationService;
        private readonly GpioButton button1;
        private bool disposed;

        public WeatherDisplayHardwareCoordinator(
            ILogger<WeatherDisplayHardwareCoordinator> logger,
            IAppSettings appSettings,
            IGpioController gpioController,
            IDisplayCompilationService displayCompilationService)
        {
            this.logger = logger;
            this.appSettings = appSettings;
            this.gpioController = gpioController;
            this.displayCompilationService = displayCompilationService;

            this.logger.LogDebug($"Initialize a new GPIO button");
            this.button1 = new GpioButton(5, this.gpioController, true, PinMode.InputPullUp, debounceTime: TimeSpan.FromMilliseconds(10));
            this.button1.Press += this.OnButton1Pressed;
        }

        public async Task HandleButtonPress(int buttonId)
        {
            this.logger.LogDebug($"HandleButtonPress: buttonId={buttonId}");

            var buttonMappings = this.appSettings.ButtonMappings.Where(b => b.ButtonId == buttonId);
            var buttonMappingsCount = buttonMappings.Count();
            if (buttonMappingsCount == 0)
            {
                throw new NotSupportedException($"Button with buttonId={buttonId} is currently not supported.");
            }

            if (buttonMappingsCount > 1)
            {
                throw new NotSupportedException(
                    $"Button with buttonId={buttonId} has multiple assignments:{Environment.NewLine}" +
                    $"{string.Join(Environment.NewLine, buttonMappings.Select(b => $"- {b.Name}"))}");
            }

            var buttonMapping = buttonMappings.Single();
            await this.displayCompilationService.SelectDisplayCompilationAsync(buttonMapping.Name);
        }

        private async void OnButton1Pressed(object sender, EventArgs e)
        {
            await this.HandleButtonPress(buttonId: 1);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.button1.Press -= this.OnButton1Pressed;
                    this.button1.Dispose();
                }

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
