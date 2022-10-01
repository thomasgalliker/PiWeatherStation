using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace WeatherDisplay.Internals
{
    internal class AsyncLock
    {
        private readonly AsyncSemaphore semaphore;
        private readonly Task<IDisposable> releaserTask;

        public AsyncLock()
        {
            this.semaphore = new AsyncSemaphore(1);
            this.releaserTask = Task.FromResult<IDisposable>(new Releaser(this));
        }

        //public void RunOnce(Action action)
        //{
        //    if (Monitor.TryEnter(action))
        //    {
        //        try
        //        {
        //            action();
        //        }
        //        finally
        //        {
        //            Monitor.Exit(action);
        //        }
        //    }
        //    else
        //    {
        //        Debug.WriteLine($"TryEnter not successful");
        //    }
        //}
        
        //public async Task RunOnceAsync(Func<Task> task)
        //{
        //    if (Monitor.TryEnter(task))
        //    {
        //        try
        //        {
        //            await task();
        //        }
        //        finally
        //        {
        //            Monitor.Exit(task);
        //        }
        //    }
        //    else
        //    {
        //    }
        //}

        public Task<IDisposable> LockAsync()
        {
            var wait = this.semaphore.WaitAsync();
            return wait.IsCompleted
                ? this.releaserTask
                : wait.ContinueWith((_, state) => (IDisposable)new Releaser((AsyncLock)state), this, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
        }

        private struct Releaser : IDisposable
        {
            private readonly AsyncLock asyncLock;

            internal Releaser(AsyncLock asyncLock)
            {
                this.asyncLock = asyncLock;
            }

            public void Dispose()
            {
                this.asyncLock?.semaphore.Release();
            }
        }
    }

    internal class AsyncDisposable : IDisposable
    {
        private readonly TaskCompletionSource<bool> tcs;

        public AsyncDisposable()
        {
            this.tcs = new TaskCompletionSource<bool>();
        }

        public Task Task => this.tcs.Task;

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}