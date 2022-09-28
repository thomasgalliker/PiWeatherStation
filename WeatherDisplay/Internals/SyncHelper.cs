using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WeatherDisplay.Internals
{
    internal class SyncHelper
    {
        private readonly ConcurrentQueue<TaskCompletionSource<object>> requestQueue = new ConcurrentQueue<TaskCompletionSource<object>>();

        private const int NotRunning = 0;
        private const int Running = 1;
        private int currentState;

        public bool IsRunning => Interlocked.CompareExchange(ref this.currentState, NotRunning, NotRunning) != NotRunning;

        public async Task RunOnceAsync(Func<Task> task)
        {
            if (Interlocked.CompareExchange(ref this.currentState, Running, NotRunning) == NotRunning)
            {
                // The given task is only executed if we pass this atomic CompareExchange call,
                // which switches the current state flag from 'not running' to 'running'.

                var id = $"{Guid.NewGuid():N}".Substring(0, 5).ToUpperInvariant();
                Debug.WriteLine($"Task {id} started");

                try
                {
                    await task();
                }
                finally
                {
                    Debug.WriteLine($"Task {id} finished");
                    Interlocked.Exchange(ref this.currentState, NotRunning);
                }
            }

            // All other method calls which can't make it into the critical section
            // are just returned immediately.
        }

        public async Task<T> RunOnceAsync<T>(Func<Task<T>> task)
        {
            if (Interlocked.CompareExchange(ref this.currentState, Running, NotRunning) == NotRunning)
            {
                var id = $"{Guid.NewGuid():N}".Substring(0, 5).ToUpperInvariant();
                Debug.WriteLine($"Task {id} started");

                try
                {
                    var result = await task();

                    // As soon as we have a result value,
                    // we signal the subsequent requests with the result.
                    while (this.requestQueue.TryDequeue(out var item))
                    {
                        item.SetResult(result);
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    // In case of an exception,
                    // we signal the subsequent requests with this exception.
                    while (this.requestQueue.TryDequeue(out var item))
                    {
                        item.SetException(ex);
                    }
                }
                finally
                {
                    Debug.WriteLine($"Task {id} finished");
                    Interlocked.Exchange(ref this.currentState, NotRunning);
                }
            }

            // All other method calls which can't make it into the critical section
            // are waiting for the result to be returned.
            {
                var taskCompletionSource = new TaskCompletionSource<object>();
                this.requestQueue.Enqueue(taskCompletionSource);

                var result = (T)await taskCompletionSource.Task;
                return result;
            }
        }
    }
}
