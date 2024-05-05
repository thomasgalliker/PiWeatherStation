using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace WeatherDisplay.Model.Settings
{
    [DebuggerDisplay(@"\{RunSetup = {RunSetup}, IsDebug = {IsDebug}\}")]
    public class AppSettings : IAppSettings
    {
        private CultureInfo cultureInfo;

        public static readonly AppSettings Default = new AppSettings
        {
            RunSetup = true,
            IsDebug = false,
            ButtonMappings = new[]
            {
                new ButtonMapping
                {
                    ButtonId = 1,
                    GpioPin = 6,
                    Page = App.Pages.OpenWeatherMapPage,
                },
                new ButtonMapping
                {
                    ButtonId = 2,
                    GpioPin = 5,
                    Page = App.Pages.TemperatureDiagramPage,
                },
                new ButtonMapping
                {
                    ButtonId = 3,
                    GpioPin = 16,
                    Page = App.Pages.MeteoSwissWeatherPage,
                    Default = true,
                },
                new ButtonMapping
                {
                    ButtonId = 4,
                    GpioPin = 26,
                    Page = App.Pages.MeteoSwissWeatherStationPage,
                }
            }
        };

        public AppSettings()
        {
            this.ButtonMappings = new List<ButtonMapping>();
        }

        public virtual CultureInfo CultureInfo
        {
            get => this.cultureInfo;
            set
            {
                if (value != null)
                {
                    this.cultureInfo = value;
                    CultureInfo.CurrentCulture = value;
                    CultureInfo.CurrentUICulture = value;
                }
            }
        }

        public virtual bool RunSetup { get; set; }

        public virtual bool IsDebug { get; set; }

        public virtual AccessPointSettings AccessPoint { get; set; }

        public virtual ICollection<ButtonMapping> ButtonMappings { get; set; }
    }
}