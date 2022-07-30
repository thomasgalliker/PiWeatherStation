using DisplayService.Services;
using Microsoft.AspNetCore.Mvc;
using WeatherDisplay.Api.Services;

namespace WeatherDisplay.Api.Controllers
{
    [ApiController]
    [Route("api/button")]
    public class WeatherDisplayHardwareController : ControllerBase
    {
        private readonly IWeatherDisplayHardwareCoordinator weatherDisplayHardwareCoordinator;

        public WeatherDisplayHardwareController(IWeatherDisplayHardwareCoordinator weatherDisplayHardwareCoordinator)
        {
            this.weatherDisplayHardwareCoordinator = weatherDisplayHardwareCoordinator;
        }

        [HttpGet("press/{buttonId}")]
        public async Task Press(int buttonId)
        {
            await this.weatherDisplayHardwareCoordinator.HandleButtonPress(buttonId);
        }
    }
}