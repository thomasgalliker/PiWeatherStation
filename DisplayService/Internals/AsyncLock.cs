using System;
using System.Threading;
using System.Threading.Tasks;

namespace DisplayService.Internals
{
    public class AsyncLock
    {
        private readonly AsyncSemaphore semaphore;
        private readonly Task<IDisposable> releaserTask;

        public AsyncLock()
        {
            this.semaphore = new AsyncSemaphore(1);
            this.releaserTask = Task.FromResult<IDisposable>(new Releaser(this));
        }

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