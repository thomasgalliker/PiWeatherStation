using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DisplayService.Extensions;
using Microsoft.Extensions.Logging;

namespace DisplayService.Services
{
    public class CacheService : ICacheService
    {
        private const string CacheFileName = "IoTDisplayScreen.png";
        private readonly DirectoryInfo cacheFolder;
        private readonly int maxArchiveFiles;
        private readonly ILogger logger;
        private readonly IFileNameRandomizer fileNameRandomizer;

        public CacheService(
            ILogger<CacheService> logger,
            IDateTime dateTime)
        {
            this.logger = logger;
            this.fileNameRandomizer = new DateTimeFileNameRandomizer(dateTime);

            this.cacheFolder = new DirectoryInfo(Path.GetFullPath("./Cache")); // TODO Get this path from a configuration
            if (!this.cacheFolder.Exists)
            {
                this.cacheFolder.Create();
            }
            this.maxArchiveFiles = 10; // TODO Get this path from a configuration
        }

        private string CacheFile => Path.Combine(this.cacheFolder.FullName, CacheFileName);

        public async Task SaveToCache(Stream bitmapStream)
        {
            this.DeleteRollingCacheFiles();

            var cacheFile = this.fileNameRandomizer.Next(this.CacheFile);

            try
            {
                using (var fileStream = new FileStream(cacheFile, FileMode.Create, FileAccess.Write))
                {
                    await bitmapStream.CopyToAndRewindAsync(fileStream);
                }

                var cacheFileInfo = new FileInfo(cacheFile);
                this.logger.LogInformation($"SaveToCache finished successfully (FullName={cacheFileInfo.FullName}, Length={cacheFileInfo.Length})");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"SaveToCache failed with exception: {ex.Message}. (cacheFile: {cacheFile})");
            }
        }

        private void DeleteRollingCacheFiles()
        {
            var cacheFileNameWithoutExtension = Path.GetFileNameWithoutExtension(CacheFileName);
            var cacheFileExtension = Path.GetExtension(CacheFileName);
            var searchPattern = $"{cacheFileNameWithoutExtension}*{cacheFileExtension}";

            this.logger.LogInformation($"DeleteRollingCacheFiles (searchPattern: {searchPattern}, maxArchiveFiles={this.maxArchiveFiles})");

            try
            {
                var cacheFiles = this.cacheFolder.EnumerateFiles(searchPattern).ToList();
                var oldCacheFiles = cacheFiles.OrderByDescending(f => f.CreationTimeUtc).Skip(this.maxArchiveFiles - 1).ToList();
                foreach (var item in oldCacheFiles)
                {
                    File.Delete(item.FullName);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"DeleteRollingCacheFiles failed with exception: {ex.Message}. (searchPattern: {searchPattern}, maxArchiveFiles={this.maxArchiveFiles})");
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

                    this.logger.LogInformation($"LoadFromCache finished successfully (cacheFile: {cacheFile})");
                    return memoryStream;
                }
                else
                {
                    this.logger.LogInformation($"LoadFromCache couldn't find cacheFile: {cacheFile}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"LoadFromCache failed with exception: {ex.Message}. (cacheFile: {cacheFile})");
            }

            return null;
        }
    }
}
