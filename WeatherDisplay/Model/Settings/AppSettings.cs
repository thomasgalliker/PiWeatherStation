using System.Collections.Generic;
using System.Globalization;

namespace WeatherDisplay.Model.Settings
{
    public class AppSettings : IAppSettings
    {
        private CultureInfo cultureInfo;

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