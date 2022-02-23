using DisplayService.Services;
using Microsoft.AspNetCore.Mvc;

namespace WeatherDisplay.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DisplayManagerController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IDisplayManager displayManager;

        public DisplayManagerController(ILogger<DisplayManagerController> logger, IDisplayManager displayManager)
        {
            this.logger = logger;
            this.displayManager = displayManager;
        }

        [HttpGet("start")]
        public async Task StartAsync()
        {
            await this.displayManager.StartAsync();
        }

        [HttpGet("clear")]
        public void Clear()
        {
            this.displayManager.Clear();
        }
    }
}