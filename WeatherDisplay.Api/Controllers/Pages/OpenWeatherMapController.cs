using Microsoft.AspNetCore.Mvc;
using WeatherDisplay.Api.Services.Configuration;
using WeatherDisplay.Model.Settings;
using WeatherDisplay.Pages.OpenWeatherMap;

namespace WeatherDisplay.Api.Controllers.Pages
{
    [ApiController]
    [Route("api/page/openweathermap")]
    public class OpenWeatherMapController : ControllerBase
    {
        private readonly IWritableOptions<OpenWeatherMapPageOptions> openWeatherMapPageOptions;

        public OpenWeatherMapController(
            IWritableOptions<OpenWeatherMapPageOptions> openWeatherMapPageOptions)
        {
            this.openWeatherMapPageOptions = openWeatherMapPageOptions;
        }

        [HttpGet("places")]
        public IEnumerable<Place> GetPlaces()
        {
            var places = this.openWeatherMapPageOptions.Value.Places;
            return places;
        }

        [HttpPost("place")]
        public void AddPlace(Place place)
        {
            // TODO: Input validation!

            this.openWeatherMapPageOptions.Update((o) =>
            {
                if (o.Places.SingleOrDefault(p => string.Equals(p.Name, place.Name, StringComparison.InvariantCultureIgnoreCase)) is not null)
                {
                    throw new Exception($"Place with Name={place.Name} already exists");
                }

                if (place.IsCurrentPlace)
                {
                    foreach (var item in o.Places)
                    {
                        item.IsCurrentPlace = false;
                    }
                }

                o.Places.Add(place);

                return o;
            });
        }

        [HttpPut("place")]
        public void UpdatePlace(Place place)
        {
            // TODO: Input validation!

            this.openWeatherMapPageOptions.Update((o) =>
            {
                if (place.IsCurrentPlace)
                {
                    foreach (var item in o.Places)
                    {
                        item.IsCurrentPlace = false;
                    }
                }

                if (o.Places.SingleOrDefault(p => string.Equals(p.Name, place.Name, StringComparison.InvariantCultureIgnoreCase) ||
                                                  (p.Latitude == place.Latitude && p.Longitude == p.Longitude)) is Place existingPlace)
                {
                    o.Places.Remove(existingPlace);
                    o.Places.Add(place);
                }
                else
                {
                    throw new Exception($"Couldn't find place with Name={place.Name}");
                }

                return o;
            });
        }

        [HttpPut("place/current")]
        public void SetCurrentPlace(string name)
        {
            this.openWeatherMapPageOptions.Update((o) =>
            {
                if (o.Places.SingleOrDefault(p => string.Equals(p.Name, name, StringComparison.InvariantCultureIgnoreCase)) is Place existingPlace)
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
        public void RemovePlace(string name)
        {
            this.openWeatherMapPageOptions.Update((o) =>
            {
                if (o.Places.SingleOrDefault(p => string.Equals(p.Name, name, StringComparison.InvariantCultureIgnoreCase)) is Place existingPlace)
                {
                    o.Places.Remove(existingPlace);
                }

                return o;
            });
        }
    }
}