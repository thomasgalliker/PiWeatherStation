using System.Device.Gpio;
using System.Gpio.Devices;
using System.Gpio.Devices.Buttons;
using WeatherDisplay.Compilations;
using WeatherDisplay.Model;

namespace WeatherDisplay.Api.Services
{
    public class WeatherDisplayHardwareCoordinator : IWeatherDisplayHardwareCoordinator, IDisposable
    {
        private static readonly TimeSpan ButtonDebounceTime = TimeSpan.FromMilliseconds(10);
        private static readonly PinMode ButtonPinMode = PinMode.InputPullUp;

        private readonly ILogger logger;
        private readonly IAppSettings appSettings;
        private readonly IGpioController gpioController;
        private readonly IDisplayCompilationService displayCompilationService;
        private readonly GpioButton button1;
        private readonly GpioButton button2;
        private readonly GpioButton button3;
        private readonly GpioButton button4;

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

            this.logger.LogDebug($"Initialize GPIO buttons");

            this.button1 = new GpioButton(3, this.gpioController, shouldDispose: true, ButtonPinMode, ButtonDebounceTime);
            this.button1.Press += this.OnButton1Pressed;

            this.button2 = new GpioButton(5, this.gpioController, shouldDispose: true, ButtonPinMode, ButtonDebounceTime);
            this.button2.Press += this.OnButton2Pressed;

            this.button3 = new GpioButton(7, this.gpioController, shouldDispose: true, ButtonPinMode, ButtonDebounceTime);
            this.button3.Press += this.OnButton3Pressed;

            this.button4 = new GpioButton(11, this.gpioController, shouldDispose: true, ButtonPinMode, ButtonDebounceTime);
            this.button4.Press += this.OnButton4Pressed;
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
                    $"{string.Join(Environment.NewLine, buttonMappings.Select(b => $"- {b.DisplayCompilation}"))}");
            }

            var buttonMapping = buttonMappings.Single();
            await this.displayCompilationService.SelectDisplayCompilationAsync(buttonMapping.DisplayCompilation);
        }

        private async void OnButton1Pressed(object sender, EventArgs e)
        {
            await this.HandleButtonPress(buttonId: 1);
        }
        
        private async void OnButton2Pressed(object sender, EventArgs e)
        {
            await this.HandleButtonPress(buttonId: 2);
        }
        
        private async void OnButton3Pressed(object sender, EventArgs e)
        {
            await this.HandleButtonPress(buttonId: 3);
        }
        
        private async void OnButton4Pressed(object sender, EventArgs e)
        {
            await this.HandleButtonPress(buttonId: 4);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.button1.Press -= this.OnButton1Pressed;
                    this.button1.Dispose();

                    this.button2.Press -= this.OnButton2Pressed;
                    this.button2.Dispose();

                    this.button3.Press -= this.OnButton3Pressed;
                    this.button3.Dispose();

                    this.button4.Press -= this.OnButton4Pressed;
                    this.button4.Dispose();
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
