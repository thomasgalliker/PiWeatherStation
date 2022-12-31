using Microsoft.AspNetCore.Mvc;
using WeatherDisplay.Api.Services.Hardware;

namespace WeatherDisplay.Api.Controllers
{
    [ApiController]
    [Route("api/button")]
    public class ButtonsController : ControllerBase
    {
        private readonly IButtonsAccessService buttonsAccessService;

        public ButtonsController(IButtonsAccessService buttonsAccessService)
        {
            this.buttonsAccessService = buttonsAccessService;
        }

        [HttpGet("press/{buttonId}")]
        public async Task Press(int buttonId)
        {
            await this.buttonsAccessService.HandleButtonPress(buttonId);
        }

        [HttpGet("hold/{buttonId}")]
        public async Task Hold(int buttonId)
        {
            await this.buttonsAccessService.HandleButtonHolding(buttonId);
        }
    }
}