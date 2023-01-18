using System;
using System.Device.Buttons;
using System.Device.Gpio;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WeatherDisplay.Extensions;
using WeatherDisplay.Model;
using WeatherDisplay.Pages.SystemInfo;
using WeatherDisplay.Services.Navigation;

namespace WeatherDisplay.Services.Hardware
{
    public class ButtonsAccessService : IButtonsAccessService, IDisposable
    {
        private static readonly TimeSpan ButtonDebounceTime = TimeSpan.FromMilliseconds(10);
        private static readonly PinMode ButtonPinMode = PinMode.InputPullUp;

        private readonly ILogger logger;
        private readonly IAppSettings appSettings;
        private readonly IGpioController gpioController;
        private readonly INavigationService navigationService;

        private GpioButton button1;
        private GpioButton button2;
        private GpioButton button3;
        private GpioButton button4;

        private bool initialized;
        private bool disposed;

        public ButtonsAccessService(
            ILogger<ButtonsAccessService> logger,
            IAppSettings appSettings,
            IGpioController gpioController,
            INavigationService navigationService)
        {
            this.logger = logger;
            this.appSettings = appSettings;
            this.gpioController = gpioController;
            this.navigationService = navigationService;
        }

        public void Initialize()
        {
            if (this.initialized)
            {
                this.logger.LogDebug($"Initialize: Already initialized");
                return;
            }

            this.logger.LogDebug($"Initialize");

            var buttonMappings = this.appSettings.ButtonMappings;

            var buttonMapping1 = buttonMappings.SingleOrDefault(b => b.ButtonId == 1);
            if (buttonMapping1 != null)
            {
                this.button1 = new GpioButton(buttonMapping1.GpioPin, this.gpioController, shouldDispose: false, ButtonPinMode, ButtonDebounceTime);
                this.button1.Press += this.OnButton1Pressed;
            }

            var buttonMapping2 = buttonMappings.SingleOrDefault(b => b.ButtonId == 2);
            if (buttonMapping2 != null)
            {
                this.button2 = new GpioButton(buttonMapping2.GpioPin, this.gpioController, shouldDispose: false, ButtonPinMode, ButtonDebounceTime);
                this.button2.Press += this.OnButton2Pressed;
            }

            var buttonMapping3 = buttonMappings.SingleOrDefault(b => b.ButtonId == 3);
            if (buttonMapping3 != null)
            {
                this.button3 = new GpioButton(buttonMapping3.GpioPin, this.gpioController, shouldDispose: false, ButtonPinMode, ButtonDebounceTime);
                this.button3.Press += this.OnButton3Pressed;
            }

            var buttonMapping4 = buttonMappings.SingleOrDefault(b => b.ButtonId == 4);
            if (buttonMapping4 != null)
            {
                this.button4 = new GpioButton(buttonMapping4.GpioPin, this.gpioController, shouldDispose: false, ButtonPinMode, ButtonDebounceTime);
                this.button4.Press += this.OnButton4Pressed;
                this.button4.Holding += this.OnButton4Holding;
            }

            this.initialized = true;
        }

        public async Task HandleButtonPress(int buttonId)
        {
            this.logger.LogDebug($"HandleButtonPress: buttonId={buttonId}");

            try
            {

                var currentPage = this.navigationService.GetCurrentPage();
                if (App.Pages.IsSystemPage(currentPage))
                {
                    if (buttonId == 4)
                    {
                        var nextPage = App.Pages.GetNextSystemPage(currentPage);
                        await this.navigationService.NavigateAsync(nextPage);
                    }
                }
                else
                {
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
                            $"{string.Join(Environment.NewLine, buttonMappings.Select(b => $"- {b.Page}"))}");
                    }

                    var buttonMapping = buttonMappings.Single();
                    await this.navigationService.NavigateAsync(buttonMapping.Page);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HandleButtonPress for buttonId={buttonId} failed with exception");

                //var navigationParameters = new ErrorPage.NavigationParameters { Exception = ex };
                //await this.navigationService.NavigateAsync(App.Pages.ErrorPage, navigationParameters);
            }
        }

        public async Task HandleButtonHolding(int buttonId)
        {
            this.logger.LogDebug($"HandleButtonHold: buttonId={buttonId}");

            try
            {
                if (buttonId == 4)
                {
                    var currentPage = this.navigationService.GetCurrentPage();
                    if (App.Pages.IsSystemPage(currentPage))
                    {
                        var defaultButton = this.appSettings.ButtonMappings.GetDefaultButtonMapping();
                        await this.navigationService.NavigateAsync(defaultButton.Page);
                    }
                    else
                    {
                        await this.navigationService.NavigateAsync(App.Pages.SetupPage);
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HandleButtonHolding for buttonId={buttonId} failed with exception");

                //var navigationParameters = new ErrorPage.NavigationParameters { Exception = ex };
                //await this.navigationService.NavigateAsync(App.Pages.ErrorPage, navigationParameters);
            }
        }

        private async void OnButton1Pressed(object sender, EventArgs e)
        {
            await this.HandleButtonPress(buttonId: 1);
        }

        private async void OnButton4Holding(object sender, EventArgs e)
        {
            await this.HandleButtonHolding(buttonId: 4);
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
                    this.button4.Holding -= this.OnButton4Holding;
                    this.button4.Dispose();

                    this.initialized = false;
                }

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
