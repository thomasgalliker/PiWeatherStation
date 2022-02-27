using DisplayService.Extensions;
using DisplayService.Model;
using DisplayService.Resources;
using DisplayService.Services;
using Microsoft.AspNetCore.Mvc;

namespace WeatherDisplay.Api.Controllers
{
    [ApiController]
    [Route("api/images")]
    public class ImagesController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IDisplayManager displayManager;

        public ImagesController(
            ILogger<ImagesController> logger,
            IDisplayManager displayManager)
        {
            this.logger = logger;
            this.displayManager = displayManager;
        }

        [HttpGet("testimage1")]
        public async Task TestImage1()
        {
            await this.DisplayTestImage(() => TestImages.GetTestImage1());
        }

        [HttpGet("testimage2")]
        public async Task TestImage2()
        {
            await this.DisplayTestImage(() => TestImages.GetTestImage2());
        }

        [HttpPost("sendimage")]
        public async Task SendImageAsync(IFormFile formFile)
        {
            if (formFile == null || formFile.Length == 0)
            {
                throw new ArgumentNullException(nameof(formFile));
            }

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await formFile.CopyToAsync(memoryStream);
                    memoryStream.Rewind();

                    await this.DisplayTestImage(() => memoryStream);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"SendImageAsync with file {formFile.FileName} failed with exception");
            }
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