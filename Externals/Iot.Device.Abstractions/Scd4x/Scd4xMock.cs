using System.Threading.Tasks;
using UnitsNet;

namespace Iot.Device.Scd4x
{
    internal class Scd4xMock : IScd4x
    {
        public Temperature Temperature => Temperature.FromDegreesCelsius(20d);

        public RelativeHumidity RelativeHumidity => RelativeHumidity.FromPercent(60d);

        public VolumeConcentration Co2 => VolumeConcentration.FromPartsPerMillion(1500d);

        public Task<Scd41ReadResult> ReadAsync()
        {
            return Task.FromResult(new Scd41ReadResult(this.Temperature, this.RelativeHumidity, this.Co2));
        }

        public void Reset()
        {
        }

        public void Dispose()
        {
        }
    }
}