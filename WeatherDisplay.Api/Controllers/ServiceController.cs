using Microsoft.AspNetCore.Mvc;
using RaspberryPi.Services;
using WeatherDisplay.Api.Services;

namespace WeatherDisplay.Api.Controllers
{
    [ApiController]
    [Route("api/system/service")]
    public class ServiceController : ControllerBase
    {
        private readonly IWeatherDisplayServiceConfigurator weatherDisplayServiceConfigurator;

        public ServiceController(IWeatherDisplayServiceConfigurator weatherDisplayServiceConfigurator)
        {
            this.weatherDisplayServiceConfigurator = weatherDisplayServiceConfigurator;
        }

        [HttpGet("start")]
        public void Start()
        {
            this.weatherDisplayServiceConfigurator.StartService();
        }

        [HttpGet("stop")]
        public void Stop()
        {
            this.weatherDisplayServiceConfigurator.StopService();
        }

        [HttpGet("restart")]
        public void Restart()
        {
            this.weatherDisplayServiceConfigurator.RestartService();
        }

        [HttpGet("install")]
        public void Install()
        {
            this.weatherDisplayServiceConfigurator.InstallService();
        }

        [HttpGet("reinstall")]
        public void Reistall()
        {
            this.weatherDisplayServiceConfigurator.ReinstallService();
        }

        [HttpGet("uninstall")]
        public void Uninstall()
        {
            this.weatherDisplayServiceConfigurator.UninstallService();
        }
    }
}