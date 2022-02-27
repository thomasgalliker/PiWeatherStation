using DisplayService.Services;
using Microsoft.AspNetCore.Mvc;

namespace WeatherDisplay.Api.Controllers
{
    [ApiController]
    [Route("api/displaymanager")]
    public class DisplayManagerController : ControllerBase
    {
        private readonly IDisplayManager displayManager;

        public DisplayManagerController(IDisplayManager displayManager)
        {
            this.displayManager = displayManager;
        }

        [HttpGet("start")]
        public async Task StartAsync()
        {
            await this.displayManager.StartAsync();
        }
        
        [HttpGet("stop")]
        public void Stop()
        {
            this.displayManager.StopTimers();
        }

        [HttpGet("clear")]
        public async Task ClearAsync()
        {
            await this.displayManager.ClearAsync();
        }

        [HttpGet("reset")]
        public async Task ResetAsync()
        {
            await this.displayManager.ResetAsync();
        }
    }
}