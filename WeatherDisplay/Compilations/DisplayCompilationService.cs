using System;
using System.Linq;
using System.Threading.Tasks;
using DisplayService.Internals;
using DisplayService.Services;
using Microsoft.Extensions.Logging;

namespace WeatherDisplay.Compilations
{
    // TODO: Rename to "NavigationService" as its purpose is to connect user input with display compilations
    public class DisplayCompilationService : IDisplayCompilationService
    {
        private readonly ILogger logger;
        private readonly IDisplayManager displayManager;
        private readonly IServiceProvider serviceProvider;

        private readonly SyncHelper syncHelper = new SyncHelper();

        public DisplayCompilationService(
            ILogger<DisplayCompilationService> logger,
            IDisplayManager displayManager,
            IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.displayManager = displayManager;
            this.serviceProvider = serviceProvider;
        }

        public async Task SelectDisplayCompilationAsync(string name)
        {
            await this.syncHelper.RunOnceAsync(async () =>
            {
                this.logger.LogDebug($"SelectDisplayCompilationAsync: name={name}");

                var displayCompilationType = typeof(IDisplayCompilation);
                var typesToLookup = this.GetType().Assembly
                    .DefinedTypes.Where(t => 
                        t.DeclaringType == null &&
                        t.FullName.EndsWith(name) &&
                        displayCompilationType.IsAssignableFrom(t))
                    .ToList();

                if (typesToLookup.Count == 0)
                {
                    throw new ArgumentException("Could not find display compilation", nameof(name));
                }
                else if (typesToLookup.Count > 1)
                {
                    throw new ArgumentException($"Ambiguous name '{name}': Could not find display compilation", nameof(name));
                }

                var typeToResolve = typesToLookup.First();

                IDisplayCompilation selectedDisplayCompilation;

                try
                {
                    selectedDisplayCompilation = this.serviceProvider.GetService(typeToResolve) as IDisplayCompilation;
                    if (selectedDisplayCompilation == null)
                    {
                        throw new ArgumentException("Could not find display compilation", nameof(name));
                    }
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex, "SelectDisplayCompilationAsync failed with exception");
                    throw new ArgumentException("Could not find display compilation", nameof(name));
                }

                this.displayManager.RemoveRenderingActions();

                selectedDisplayCompilation.AddRenderActions();

                await this.displayManager.StartAsync();
            });
        }
    }
}
