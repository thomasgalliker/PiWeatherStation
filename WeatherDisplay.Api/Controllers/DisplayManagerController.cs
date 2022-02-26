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
        public void Clear()
        {
            this.displayManager.Clear();
        }
    }
}