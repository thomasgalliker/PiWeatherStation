using System;
using System.IO;

namespace DisplayService.Services
{
    public interface IDisplay : IDisposable
    {
        int Width { get; }

        int Height { get; }

        void Clear();

        void DisplayImage(Stream bitmapStream); // TODO: Implement partial update
    }
}
