using Iot.Device.Extensions;
using Iot.Device.Model;
using Microsoft.AspNetCore.Mvc;
using WeatherDisplay.Services.Hardware;

namespace WeatherDisplay.Api.Controllers
{
    [ApiController]
    [Route("api/sensor")]
    public class SensorController : ControllerBase
    {
        private readonly ISensorAccessService sensorsAccessService;

        public SensorController(ISensorAccessService sensorsAccessService)
        {
            this.sensorsAccessService = sensorsAccessService;
        }

        [HttpGet("bme680")]
        public async Task<SensorData> Bme680()
        {
            var bme680 = this.sensorsAccessService.Bme680;
            if (bme680 == null)
            {
                return null;
            }

            var readResult = await bme680.ReadAsync();
            var sensorData = readResult.ToSensorData();
            return sensorData;
        }

        [HttpGet("scd41")]
        public async Task<SensorData> Scd41()
        {
            var scd41 = this.sensorsAccessService.Scd41;
            if (scd41 == null)
            {
                return null;
            }

            var readResult = await scd41.ReadAsync();
            var sensorData = readResult.ToSensorData();
            return sensorData;
        }
    }
}