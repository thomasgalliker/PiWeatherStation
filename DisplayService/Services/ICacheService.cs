using System.IO;
using System.Threading.Tasks;

namespace DisplayService.Services
{
    public interface ICacheService
    {
        Stream LoadFromCache();

        Task SaveToCache(Stream bitmapStream);
    }
}