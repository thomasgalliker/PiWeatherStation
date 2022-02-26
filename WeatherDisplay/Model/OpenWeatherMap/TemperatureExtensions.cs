namespace WeatherDisplay.Model.OpenWeatherMap
{
    public static class TemperatureExtensions
    {
        public static double ToKelvin(this Temperature temperature)
        {
            return Temperature.ToKelvin(temperature.Value, temperature.Unit);
        }

        public static double FromKelvinTo(this Temperature temperature)
        {
            return Temperature.FromKelvinTo(temperature.Value, temperature.Unit);
        }

        public static Temperature ConvertTo(this Temperature temperature, TemperatureUnit targetTemperatureUnit)
        {
            var targetTemperatureValue = FromTo(temperature.Value, temperature.Unit, targetTemperatureUnit);
            return new Temperature(targetTemperatureValue, targetTemperatureUnit);
        }

        public static double FromTo(double srcValue, TemperatureUnit srcUnit, TemperatureUnit destUnit)
        {
            if (srcUnit == destUnit)
            {
                return srcValue;
            }
            else
            {
                return Temperature.FromKelvinTo(Temperature.ToKelvin(srcValue, srcUnit), destUnit);
            }
        }
    }

}