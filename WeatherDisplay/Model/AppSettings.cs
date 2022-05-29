using System;
using System.Collections.Generic;
using System.Globalization;

namespace WeatherDisplay.Model
{
    public class AppSettings : IAppSettings
    {
        private CultureInfo cultureInfo;

        public AppSettings()
        {
            this.Displays = new List<DisplaySetting>();
            this.Places = new List<Place>();
        }

        public string Title { get; set; }

        public CultureInfo CultureInfo
        {
            get => this.cultureInfo;
            set
            {
                this.cultureInfo = value;
                CultureInfo.CurrentCulture = value;
                CultureInfo.CurrentUICulture = value;
            }
        }

        public bool IsDebug { get; set; }

        public List<DisplaySetting> Displays { get; set; }

        public List<Place> Places { get; set; }

        public string StateFolder { get; set; }

        public string BackgroundColor { get; set; }

        public string ForegroundColor { get; set; }

        public class DisplaySetting
        {
            public string DriverType { get; set; }

            public string Driver { get; set; }

            public int Width { get; set; } = 800;

            public int Height { get; set; } = 480;

            public int Rotation { get; set; }
        }
    }
}