using System;
using System.IO;

namespace DisplayService.Utils
{
    public class FileHelper
    {
        public static string RandomizeFilePath(string path)
        {
            var folderName = Path.GetDirectoryName(path);
            var fileName = Path.GetFileName(path);
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            var fileExtension = Path.GetExtension(fileName);
            var randomFileName = Path.Combine(folderName, $"{fileNameWithoutExtension}{GenerateRandomness()}{fileExtension}");
            return randomFileName;
        }

        private static string GenerateRandomness()
        {
            return $"-{DateTime.Now:yyyy-dd-MM-HH-mm-ss-fff}";
        }
    }
}
