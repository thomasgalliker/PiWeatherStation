using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace DisplayService.Internals
{
    public class SyncHelper
    {
        private readonly ConcurrentQueue<TaskCompletionSource<object>> requestQueue = new ConcurrentQueue<TaskCompletionSource<object>>();

        private const long NotRunning = 0;
        private const long Running = 1;
        private long currentState;

        /// <summary>
        /// Indicates if the current instance of <seealso cref="SyncHelper"/> is currently running.
        /// </summary>
        public bool IsRunning => Interlocked.Read(ref this.currentState) == Running;

        /// <summary>
        /// Runs the given <paramref name="action"/> only once at a time.
        /// </summary>
        /// <param name="action">The synchronous action.</param>
        public void RunOnce(Action action)
        {
            if (Interlocked.CompareExchange(ref this.currentState, Running, NotRunning) == NotRunning)
            {
                // The given task is only executed if we pass this atomic CompareExchange call,
                // which switches the current state flag from 'not running' to 'running'.

                var id = $"{Guid.NewGuid():N}".Substring(0, 5).ToUpperInvariant();
                Debug.WriteLine($"RunOnce: Task {id} started");

                try
                {
                    action();
                }
                finally
                {
                    Debug.WriteLine($"RunOnce: Task {id} finished");
                    Interlocked.Exchange(ref this.currentState, NotRunning);
                }
            }

            // All other method calls which can't make it into the critical section
            // are just returned immediately.
        }

        /// <summary>
        /// Runs the given <paramref name="task"/> only once at a time.
        /// </summary>
        /// <param name="task">The asynchronous task.</param>
        public async Task RunOnceAsync(Func<Task> task)
        {
            if (Interlocked.CompareExchange(ref this.currentState, Running, NotRunning) == NotRunning)
            {
                // The given task is only executed if we pass this atomic CompareExchange call,
                // which switches the current state flag from 'not running' to 'running'.

                var id = $"{Guid.NewGuid():N}".Substring(0, 5).ToUpperInvariant();
                Debug.WriteLine($"RunOnceAsync: Task {id} started");

                try
                {
                    await task();
                }
                finally
                {
                    Debug.WriteLine($"RunOnceAsync: Task {id} finished");
                    Interlocked.Exchange(ref this.currentState, NotRunning);
                }
            }

            // All other method calls which can't make it into the critical section
            // are just returned immediately.
        }

        /// <summary> 
        /// Runs the given <paramref name="function"/> only once at a time.
        /// </summary>
        /// <param name="function">The synchronous function which returns a result of type <typeparamref name="T"/>.</param>
        public T RunOnce<T>(Func<T> function)
        {
            if (Interlocked.CompareExchange(ref this.currentState, Running, NotRunning) == NotRunning)
            {
                var id = $"{Guid.NewGuid():N}".Substring(0, 5).ToUpperInvariant();
                Debug.WriteLine($"RunOnce: Task {id} started");

                try
                {
                    var result = function();

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
                    Debug.WriteLine($"RunOnce: Task {id} finished");
                    Interlocked.Exchange(ref this.currentState, NotRunning);
                }
            }

            // All other method calls which can't make it into the critical section
            // are waiting for the result to be returned.
            {
                var taskCompletionSource = new TaskCompletionSource<object>();
                this.requestQueue.Enqueue(taskCompletionSource);

                var result = (T)taskCompletionSource.Task.Result;
                return result;
            }
        }

        /// <summary>
        /// Runs the given <paramref name="task"/> only once at a time.
        /// </summary>
        /// <param name="task">The asynchronous task which returns a result of type <typeparamref name="T"/>.</param>
        public async Task<T> RunOnceAsync<T>(Func<Task<T>> task)
        {
            if (Interlocked.CompareExchange(ref this.currentState, Running, NotRunning) == NotRunning)
            {
                var id = $"{Guid.NewGuid():N}".Substring(0, 5).ToUpperInvariant();
                Debug.WriteLine($"RunOnceAsync: Task {id} started");

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
                    Debug.WriteLine($"RunOnceAsync: Task {id} finished");
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