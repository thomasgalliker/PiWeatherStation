﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WeatherDisplay.Resources.Strings {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Translations {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Translations() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WeatherDisplay.Resources.Strings.Translations", typeof(Translations).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Air Quality.
        /// </summary>
        public static string AirQualityLabelText {
            get {
                return ResourceManager.GetString("AirQualityLabelText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CO2.
        /// </summary>
        public static string CarbonDioxideAbbreviation {
            get {
                return ResourceManager.GetString("CarbonDioxideAbbreviation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Source: MeteoSwiss.
        /// </summary>
        public static string MeteoSwissWeatherPage_SourceName {
            get {
                return ResourceManager.GetString("MeteoSwissWeatherPage_SourceName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error: Missing places configuration..
        /// </summary>
        public static string OpenWeatherMapPage_ErrorMissingPlacesConfigurationLine1 {
            get {
                return ResourceManager.GetString("OpenWeatherMapPage_ErrorMissingPlacesConfigurationLine1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to appsettings.User.json &gt;&gt; OpenWeatherMapPageOptions &gt;&gt; Places.
        /// </summary>
        public static string OpenWeatherMapPage_ErrorMissingPlacesConfigurationLine2 {
            get {
                return ResourceManager.GetString("OpenWeatherMapPage_ErrorMissingPlacesConfigurationLine2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Source: OpenWeatherMap.
        /// </summary>
        public static string OpenWeatherMapPage_SourceName {
            get {
                return ResourceManager.GetString("OpenWeatherMapPage_SourceName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}, at {1}.
        /// </summary>
        public static string PlaceAndDateTime {
            get {
                return ResourceManager.GetString("PlaceAndDateTime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to RH.
        /// </summary>
        public static string RelativeHumiditySuffix {
            get {
                return ResourceManager.GetString("RelativeHumiditySuffix", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UV.
        /// </summary>
        public static string UltraViolettAbbreviation {
            get {
                return ResourceManager.GetString("UltraViolettAbbreviation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Source: wiewarm.ch.
        /// </summary>
        public static string WaterTemperaturePage_SourceName {
            get {
                return ResourceManager.GetString("WaterTemperaturePage_SourceName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Password.
        /// </summary>
        public static string WifiPSKLabelText {
            get {
                return ResourceManager.GetString("WifiPSKLabelText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Connect to following wifi network:.
        /// </summary>
        public static string WifiSetupIntroLabelText {
            get {
                return ResourceManager.GetString("WifiSetupIntroLabelText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SSID.
        /// </summary>
        public static string WifiSSIDLabelText {
            get {
                return ResourceManager.GetString("WifiSSIDLabelText", resourceCulture);
            }
        }
    }
}
