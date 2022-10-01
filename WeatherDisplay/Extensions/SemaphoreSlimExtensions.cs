using System;
using System.Threading;
using System.Threading.Tasks;

namespace WeatherDisplay.Extensions
{
    public static class SemaphoreSlimExtensions
    {
        /// <summary>
        /// Executes a task within the context of a a SemaphoreSlim.
        /// The task is started only if no <paramref name="action"/> is currently running.
        /// </summary>
        /// <param name="semaphoreSlim">The semaphore instance.</param>
        /// <param name="action">The function to execute as a task.</param>
        public static async Task ExecuteOnceAsync(this SemaphoreSlim semaphoreSlim, Func<Task> action)
        {
            if (semaphoreSlim.CurrentCount == 0)
            {
                return;
            }

            try
            {
                await semaphoreSlim.WaitAsync();
                await action();
            }
            finally
            {
                try
                {
                    semaphoreSlim.Release();
                }
                catch (SemaphoreFullException)
                {
                    // Ignored
                }
            }
        }
    }
}