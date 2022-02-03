using System;

namespace DisplayService.Services
{
    public interface IDisplayManager : IDisposable
    {
        void Start();

        void Stop();
    }
}