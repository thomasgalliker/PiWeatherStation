using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DisplayService.Services;
using Microsoft.Extensions.Logging;
using DisplayService.Internals;

namespace WeatherDisplay.Compilations
{
    // TODO: Rename to "NavigationService" as its purpose is to connect user input with display compilations
    public class DisplayCompilationService : IDisplayCompilationService
    {
        private readonly ILogger logger;
        private readonly IDisplayManager displayManager;
        private readonly IEnumerable<IDisplayCompilation> displayCompilations;

        private readonly SyncHelper syncHelper = new SyncHelper();

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
            await this.syncHelper.RunOnceAsync(async () =>
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
            });
        }
    }
}
