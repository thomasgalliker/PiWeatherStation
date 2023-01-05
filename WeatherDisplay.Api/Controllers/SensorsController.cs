using Iot.Device.Extensions;
using Iot.Device.Model;
using Microsoft.AspNetCore.Mvc;
using WeatherDisplay.Services.Hardware;

namespace WeatherDisplay.Api.Controllers
{
    [ApiController]
    [Route("api/sensor")]
    public class SensorsController : ControllerBase
    {
        private readonly ISensorAccessService sensorsAccessService;

        public SensorsController(ISensorAccessService sensorsAccessService)
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
    }
}