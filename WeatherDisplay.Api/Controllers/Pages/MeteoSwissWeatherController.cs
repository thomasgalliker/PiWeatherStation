using MeteoSwissApi;
using MeteoSwissApi.Models;
using Microsoft.AspNetCore.Mvc;
using WeatherDisplay.Api.Services.Configuration;
using WeatherDisplay.Pages.MeteoSwiss;

namespace WeatherDisplay.Api.Controllers.Pages
{
    [ApiController]
    [Route("api/page/meteoswissweather")]
    public class MeteoSwissWeatherController : ControllerBase
    {
        private readonly IWritableOptions<MeteoSwissWeatherPageOptions> meteoSwissWeatherPageOptions;
        private readonly ISwissMetNetService swissMetNetService;

        public MeteoSwissWeatherController(
            IWritableOptions<MeteoSwissWeatherPageOptions> meteoSwissWeatherPageOptions,
            ISwissMetNetService swissMetNetService)
        {
            this.meteoSwissWeatherPageOptions = meteoSwissWeatherPageOptions;
            this.swissMetNetService = swissMetNetService;
        }

        [HttpGet("weatherstations")]
        public async Task<IEnumerable<WeatherStation>> GetWeatherStations()
        {
            var weatherStations = await this.swissMetNetService.GetWeatherStationsAsync();
            return weatherStations.OrderBy(s => s.StationCode);
        }

        [HttpGet("places")]
        public IEnumerable<MeteoSwissPlace> GetPlaces()
        {
            var places = this.meteoSwissWeatherPageOptions.Value.Places;
            return places;
        }

        [HttpPost("place")]
        public void AddPlace(MeteoSwissPlace meteoSwissPlace)
        {
            // TODO: Input validation!

            this.meteoSwissWeatherPageOptions.Update((o) =>
            {
                if (o.Places.SingleOrDefault(p => p.Plz == meteoSwissPlace.Plz) is MeteoSwissPlace)
                {
                    throw new Exception($"Place with Plz={meteoSwissPlace.Plz} already exists");
                }

                if (meteoSwissPlace.IsCurrentPlace)
                {
                    foreach (var item in o.Places)
                    {
                        item.IsCurrentPlace = false;
                    }
                }

                o.Places.Add(meteoSwissPlace);

                return o;
            });
        }

        [HttpPut("place")]
        public void UpdatePlace(MeteoSwissPlace meteoSwissPlace)
        {
            // TODO: Input validation!

            this.meteoSwissWeatherPageOptions.Update((o) =>
            {
                if (meteoSwissPlace.IsCurrentPlace)
                {
                    foreach (var item in o.Places)
                    {
                        item.IsCurrentPlace = false;
                    }
                }

                if (o.Places.SingleOrDefault(p => p.Plz == meteoSwissPlace.Plz) is MeteoSwissPlace existingPlace)
                {
                    o.Places.Remove(existingPlace);
                    o.Places.Add(meteoSwissPlace);
                }
                else
                {
                    throw new Exception($"Couldn't find place with Plz={meteoSwissPlace.Plz}");
                }

                return o;
            });
        }

        [HttpPut("place/current")]
        public void SetCurrentPlace(int plz)
        {
            this.meteoSwissWeatherPageOptions.Update((o) =>
            {
                if (o.Places.SingleOrDefault(p => p.Plz == plz) is MeteoSwissPlace existingPlace)
                {
                    foreach (var place in o.Places)
                    {
                        place.IsCurrentPlace = false;
                    }

                    existingPlace.IsCurrentPlace = true;
                }

                return o;
            });
        }

        [HttpDelete("place")]
        public void RemovePlace(int plz)
        {
            this.meteoSwissWeatherPageOptions.Update((o) =>
            {
                if (o.Places.SingleOrDefault(p => p.Plz == plz) is MeteoSwissPlace existingPlace)
                {
                    o.Places.Remove(existingPlace);
                }

                return o;
            });
        }
    }
}