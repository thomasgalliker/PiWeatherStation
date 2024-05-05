using Microsoft.AspNetCore.Mvc;
using WeatherDisplay.Pages;

namespace WeatherDisplay.Api.Controllers.Pages
{
    [ApiController]
    [Route("api/page")]
    public class PageController : ControllerBase
    {
        public PageController()
        {
        }

        [HttpGet("")]
        public IEnumerable<string> GetPages([FromServices] IEnumerable<IPage> pages)
        {
            var pageType = typeof(IPage);
            var systemPageType = typeof(ISystemPage);
            var pageInfos = pages
                .Where(p => p.GetType() is Type t && pageType.IsAssignableFrom(t) && !systemPageType.IsAssignableFrom(t))
                .Select(t => t.GetType().Name)
                .OrderBy(p => p)
                .ToArray();

            return pageInfos;
        }
    }
}