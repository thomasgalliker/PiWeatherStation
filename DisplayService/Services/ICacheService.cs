using System.IO;

namespace DisplayService.Services
{
    public interface ICacheService
    {
        string CacheFile { get; }

        Stream LoadFromCache();

        void SaveToCache(Stream bitmapStream);
    }
}