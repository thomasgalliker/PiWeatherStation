using DisplayService.Services.Scheduling;
using Microsoft.Extensions.DependencyInjection;
using NCrontab.Scheduler.Internals;

namespace NCrontab.Scheduler.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddScheduler(this IServiceCollection services)
        {
            // Register services
            services.AddSingleton<IDateTime, SystemDateTime>();
            services.AddSingleton<IScheduler, Scheduler>();
        }
    }
}