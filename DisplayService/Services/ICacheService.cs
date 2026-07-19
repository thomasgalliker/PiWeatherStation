using System.IO;

namespace DisplayService.Services
{
    public interface ICacheService
    {
        Stream LoadFromCache();

        Task SaveToCache(Stream bitmapStream);
    }
}