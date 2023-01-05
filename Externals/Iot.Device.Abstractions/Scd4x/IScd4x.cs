using UnitsNet;

namespace Iot.Device.Scd4x
{
    public interface IScd4x
    {
        Temperature Temperature { get; }

        RelativeHumidity RelativeHumidity { get; }

        VolumeConcentration Co2 { get; }

        void Reset();
    }
}