using System;
using System.Threading;
using System.Threading.Tasks;

namespace WeatherDisplay.Extensions
{
    public static class TaskExtensions
    {
        /// <summary>
        /// Runs the <paramref name="task"/> once.
        /// The task is started only if <paramref name="task"/> is not currently running.
        /// </summary>
        /// <param name="task">The asynchronous task.</param>
        public static async Task RunOnce(this Func<Task> task)
        {
            if (Monitor.TryEnter(task))
            {
                try
                {
                    await task();
                }
                finally
                {
                    Monitor.Exit(task);
                }
            }
        }
    }
}