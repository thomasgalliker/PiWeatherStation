using Microsoft.AspNetCore.Mvc;
using WeatherDisplay.Api.Services.Configuration;
using WeatherDisplay.Model.Settings;
using WeatherDisplay.Services.Hardware;

namespace WeatherDisplay.Api.Controllers
{
    [ApiController]
    [Route("api/button")]
    public class ButtonsController : ControllerBase
    {
        private readonly IWritableOptions<AppSettings> appSettings;
        private readonly IButtonsAccessService buttonsAccessService;

        public ButtonsController(
            IWritableOptions<AppSettings> appSettings,
            IButtonsAccessService buttonsAccessService)
        {
            this.appSettings = appSettings;
            this.buttonsAccessService = buttonsAccessService;
        }

        [HttpGet("mappings")]
        public IEnumerable<ButtonMapping> GetButtonMappings()
        {
            var buttonMappings = this.appSettings.Value.ButtonMappings;
            return buttonMappings;
        }

        [HttpPost("mapping")]
        public void AddButtonMapping(ButtonMapping buttonMapping)
        {
            var buttonMappings = this.appSettings.Value.ButtonMappings;
            var existingButtonMapping = buttonMappings.SingleOrDefault(m => m.ButtonId == buttonMapping.ButtonId);
            if (existingButtonMapping != null)
            {
                throw new Exception($"ButtonMapping with buttonId={buttonMapping.ButtonId} already exists");
            }

            if (buttonMapping.Default)
            {
                foreach (var item in buttonMappings)
                {
                    item.Default = false;
                }
            }

            buttonMappings.Add(buttonMapping);

            this.appSettings.UpdateProperty(s => s.ButtonMappings, buttonMappings);
        }

        [HttpPut("mapping")]
        public void UpdateButtonMapping(int buttonId, string page, bool? isDefault)
        {
            var buttonMappings = this.appSettings.Value.ButtonMappings;
            var existingButtonMapping = buttonMappings.SingleOrDefault(m => m.ButtonId == buttonId);
            if (existingButtonMapping == null)
            {
                throw new Exception($"ButtonMapping with buttonId={buttonId} does not exist");
            }

            if (isDefault is bool isDefaultValue)
            {
                if (isDefaultValue)
                {
                    foreach (var item in buttonMappings)
                    {
                        item.Default = false;
                    }
                }

                existingButtonMapping.Default = isDefaultValue;
            }

            if (!string.IsNullOrEmpty(page))
            {
                existingButtonMapping.Page = page;
            }

            this.appSettings.UpdateProperty(s => s.ButtonMappings, buttonMappings);
        }

        [HttpDelete("mapping")]
        public void RemoveButtonMapping(int buttonId)
        {
            var buttonMappings = this.appSettings.Value.ButtonMappings;
            var existingButtonMapping = buttonMappings.SingleOrDefault(m => m.ButtonId == buttonId);
            if (existingButtonMapping == null)
            {
                throw new Exception($"ButtonMapping with buttonId={buttonId} does not exist");
            }

            buttonMappings.Remove(existingButtonMapping);

            this.appSettings.UpdateProperty(s => s.ButtonMappings, buttonMappings);
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

        [HttpPost("hold")]
        public async Task Hold([FromBody] params int[] buttonIds)
        {
            foreach (var buttonId in buttonIds)
            {
                await this.buttonsAccessService.HandleButtonHolding(buttonId);
                await Task.Delay(50);
            }
        }
    }
}