using Microsoft.AspNetCore.Mvc;
using RaspberryPi.Process;

namespace WeatherDisplay.Api.Controllers
{
    [ApiController]
    [Route("api/log")]
    public class LogController : ControllerBase
    {
        private readonly IProcessRunner processRunner;

        public LogController(IProcessRunner processRunner)
        {
            this.processRunner = processRunner;
        }

        [HttpGet("")]
        public string GetLogAsync(int lines = 200)
        {
            var result = this.processRunner.ExecuteCommand($"journalctl -u weatherdisplay.api.service -n {lines} -o short-iso-precise --no-pager");
            return result.OutputData;
        }
    }
}