using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DisplayService.Services;
using Microsoft.Extensions.Logging;

namespace WeatherDisplay.Compilations
{
    public class DisplayCompilationService : IDisplayCompilationService
    {
        private readonly ILogger logger;
        private readonly IDisplayManager displayManager;
        private readonly IEnumerable<IDisplayCompilation> displayCompilations;

        public DisplayCompilationService(
            ILogger<DisplayCompilationService> logger, 
            IDisplayManager displayManager,
            IEnumerable<IDisplayCompilation> displayCompilations)
        {
            this.logger = logger;
            this.displayManager = displayManager;
            this.displayCompilations = displayCompilations;
        }

        public async Task SelectDisplayCompilationAsync(string name)
        {
            this.logger.LogDebug($"SelectDisplayCompilationAsync: name={name}");

            var selectedDisplayCompilation = this.displayCompilations.SingleOrDefault(c => c.Name == name);
            if (selectedDisplayCompilation == null)
            {
                throw new ArgumentException("Could not find display compilation", nameof(name));
            }

            this.displayManager.RemoveRenderingActions();

            selectedDisplayCompilation.AddRenderActions();

            await this.displayManager.StartAsync();
        }
    }
}
