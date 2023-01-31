using UnitsNet;

namespace Iot.Device.Scd4x
{
    public class Scd41ReadResult
    {
        public Scd41ReadResult(Temperature temperature, RelativeHumidity relativeHumidity, VolumeConcentration co2)
        {
            this.Temperature = temperature;
            this.RelativeHumidity = relativeHumidity;
            this.Co2 = co2;
        }

        public Temperature Temperature { get; }

        public RelativeHumidity RelativeHumidity { get; }

        public VolumeConcentration Co2 { get; }
    }
}