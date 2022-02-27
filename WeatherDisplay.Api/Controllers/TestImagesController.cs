using DisplayService.Model;
using DisplayService.Resources;
using DisplayService.Services;
using Microsoft.AspNetCore.Mvc;

namespace WeatherDisplay.Api.Controllers
{
    [ApiController]
    [Route("api/testimages")]
    public class TestImagesController : ControllerBase
    {
        private readonly IDisplayManager displayManager;

        public TestImagesController(IDisplayManager displayManager)
        {
            this.displayManager = displayManager;
        }

        [HttpGet("1")]
        public async Task TestImage1()
        {
            await this.DisplayTestImage(() => TestImages.GetTestImage1());
        }

        [HttpGet("2")]
        public async Task TestImage2()
        {
            await this.DisplayTestImage(() => TestImages.GetTestImage2());
        }

        private async Task DisplayTestImage(Func<Stream> imageProvider)
        {
            await this.displayManager.ResetAsync();

            this.displayManager.AddRenderAction(
                () => new RenderActions.StreamImage
                {
                    X = 0,
                    Y = 0,
                    Image = imageProvider(),
                });

            await this.displayManager.StartAsync();
        }
    }
}