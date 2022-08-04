using System.Device.Gpio;
using System.Gpio.Devices;
using System.Gpio.Devices.Buttons;
using WeatherDisplay.Compilations;

namespace WeatherDisplay.Api.Services
{
    public class WeatherDisplayHardwareCoordinator : IWeatherDisplayHardwareCoordinator, IDisposable
    {
        private static readonly IDictionary<int, string> ButtonMappings = new Dictionary<int, string>
        {
            { 1, "MainWeatherDisplayCompilation" },
            { 2, "TemperatureWeatherDisplayCompilation" },
            { 3, "WaterTemperatureDisplayCompilation" },
        };

        private readonly ILogger logger;
        private readonly IGpioController gpioController;
        private readonly IDisplayCompilationService displayCompilationService;
        private readonly GpioButton button1;
        private bool disposed;

        public WeatherDisplayHardwareCoordinator(
            ILogger<WeatherDisplayHardwareCoordinator> logger,
            IGpioController gpioController,
            IDisplayCompilationService displayCompilationService)
        {
            this.logger = logger;
            this.gpioController = gpioController;
            this.displayCompilationService = displayCompilationService;

            this.logger.LogDebug($"Initialize a new GPIO button");
            this.button1 = new GpioButton(5, this.gpioController, true, PinMode.InputPullUp, debounceTime: TimeSpan.FromMilliseconds(10));
            this.button1.Press += this.OnButton1Pressed;
        }

        public async Task HandleButtonPress(int buttonId)
        {
            this.logger.LogDebug($"HandleButtonPress: buttonId={buttonId}");

            if (ButtonMappings.TryGetValue(buttonId, out var displayCompilationName))
            {
                await this.displayCompilationService.SelectDisplayCompilationAsync(displayCompilationName);
            }
            else
            {
                throw new NotSupportedException($"Button with buttonId={buttonId} does not exist.");
            }
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
