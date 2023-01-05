using UnitsNet;

namespace Iot.Device.Scd4x
{
    internal class Scd41Mock : IScd4x
    {
        public Temperature Temperature => throw new System.NotImplementedException();

        public RelativeHumidity RelativeHumidity => throw new System.NotImplementedException();

        public VolumeConcentration Co2 => throw new System.NotImplementedException();

        public void Reset()
        {
            throw new System.NotImplementedException();
        }
    }
}