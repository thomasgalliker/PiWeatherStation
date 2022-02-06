using System;
using System.IO;

namespace DisplayService.Services
{
    public class CacheService : ICacheService
    {
        private const string cacheFileName = "IoTDisplayScreen.png";
        private readonly string cacheFolder;

        public CacheService(string cacheFolder = ".")
        {
            this.cacheFolder = cacheFolder;
        }

        public string CacheFile
        {
            get
            {
                return Path.GetFullPath(Path.Combine(this.cacheFolder, cacheFileName));
            }
        }

        public void SaveToCache(Stream bitmapStream)
        {
            var cacheFile = this.CacheFile;

            try
            {
                using (var fileStream = new FileStream(cacheFile, FileMode.Create, FileAccess.Write))
                {
                    bitmapStream.CopyTo(fileStream);
                }

                Console.WriteLine($"SaveToCache finished successfully (cacheFile: {cacheFile})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SaveToCache failed with exception: {ex.Message}. (cacheFile: {cacheFile})");
            }
        }

        public Stream LoadFromCache()
        {
            var cacheFile = this.CacheFile;

            try
            {
                if (File.Exists(cacheFile))
                {
                    var memoryStream = new MemoryStream();
                    using (var fileStream = new FileStream(cacheFile, FileMode.Open, FileAccess.Read))
                    {
                        fileStream.CopyTo(memoryStream);
                    }

                    Console.WriteLine($"LoadFromCache finished successfully (cacheFile: {cacheFile})");
                    return memoryStream;
                }
                else
                {
                    Console.WriteLine($"LoadFromCache couldn't find cacheFile: {cacheFile}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"LoadFromCache failed with exception: {ex.Message}. (cacheFile: {cacheFile})");
            }

            return null;
        }
    }
}
