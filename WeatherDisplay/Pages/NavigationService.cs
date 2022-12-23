using System;
using System.Linq;
using System.Threading.Tasks;
using DisplayService.Internals;
using DisplayService.Services;
using Microsoft.Extensions.Logging;

namespace WeatherDisplay.Pages
{
    public class NavigationService : INavigationService
    {
        private readonly ILogger logger;
        private readonly IDisplayManager displayManager;
        private readonly IServiceProvider serviceProvider;

        private readonly SyncHelper syncHelper = new SyncHelper();
        private object currentPage;

        public NavigationService(
            ILogger<NavigationService> logger,
            IDisplayManager displayManager,
            IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.displayManager = displayManager;
            this.serviceProvider = serviceProvider;
        }

        public string GetCurrentPage()
        {
            return this.currentPage?.GetType().Name;
        }

        public Task NavigateAsync(string name)
        {
            return this.NavigateAsync(name, null);
        }

        public async Task NavigateAsync(string name, INavigationParameters navigationParameters)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(nameof(name));
            }

            var currentPageName = this.GetCurrentPage();
            //if (currentPageName == name)
            //{
            //    this.logger.LogDebug($"NavigateAsync: name={name} --> is already displayed");
            //    return;
            //}

            this.currentPage = await this.syncHelper.RunOnceAsync(async () =>
            {
                this.logger.LogDebug($"NavigateAsync: name={name}, navigationParameters={navigationParameters?.ToString() ?? "null"}");

                var interfaceType = typeof(INavigatedAware);
                var typesToLookup = this.GetType().Assembly
                    .DefinedTypes.Where(t =>
                        t.DeclaringType == null &&
                        t.FullName.EndsWith(name) &&
                        interfaceType.IsAssignableFrom(t))
                    .ToList();

                if (typesToLookup.Count == 0)
                {
                    throw new ArgumentException("Could not find target page", nameof(name));
                }
                else if (typesToLookup.Count > 1)
                {
                    throw new ArgumentException($"Ambiguous name '{name}': Could not distinctly find target page", nameof(name));
                }

                var typeToResolve = typesToLookup.First();

                INavigatedAware selectedPage;

                try
                {
                    selectedPage = this.serviceProvider.GetService(typeToResolve) as INavigatedAware;
                    if (selectedPage == null)
                    {
                        throw new ArgumentException($"Unable to resolve target page of type {typeToResolve.FullName}", nameof(name));
                    }
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex, "NavigateAsync failed with exception");
                    throw new ArgumentException($"Unable to resolve target page of type {typeToResolve.FullName}", nameof(name), ex);
                }

                this.displayManager.RemoveRenderingActions();

                await selectedPage.OnNavigatedToAsync(navigationParameters);

                await this.displayManager.StartAsync();

                return selectedPage;
            });
        }
    }
}
