using System;
using System.Threading.Tasks;
using UnitsNet;

namespace Iot.Device.Scd4x
{
    public interface IScd4x : IDisposable
    {
        Temperature Temperature { get; }

        RelativeHumidity RelativeHumidity { get; }

        VolumeConcentration Co2 { get; }

        Task<Scd41ReadResult> ReadAsync();

        void Reset();
    }
}