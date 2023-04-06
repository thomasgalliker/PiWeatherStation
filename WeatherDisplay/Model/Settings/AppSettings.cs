using System.Collections.Generic;
using System.Globalization;

namespace WeatherDisplay.Model.Settings
{
    public class AppSettings : IAppSettings
    {
        private CultureInfo cultureInfo;

        public AppSettings()
        {
            this.Displays = new List<DisplaySetting>();
            this.ButtonMappings = new List<ButtonMapping>();
        }

        public string Title { get; set; }

        public CultureInfo CultureInfo
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

        public bool RunSetup { get; set; }

        public bool IsDebug { get; set; }

        public AccessPointSettings AccessPoint { get; set; }

        public ICollection<DisplaySetting> Displays { get; set; }

        public ICollection<ButtonMapping> ButtonMappings { get; set; }
    }
}