using System;
using System.IO;

namespace DisplayService.Services
{
    public abstract class FileNameRandomizer : IFileNameRandomizer
    {
        /// <inheritdoc/>
        public string Next(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            var folderName = Path.GetDirectoryName(path);
            var fileName = Path.GetFileName(path);
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            var fileExtension = Path.GetExtension(fileName);
            var randomFileName = Path.Combine(folderName, $"{fileNameWithoutExtension}{this.GetRandomness()}{fileExtension}");
            return randomFileName;
        }

        /// <summary>
        /// Returns the random portion to be used in the file name or file path.
        /// </summary>
        /// <returns>The random string.</returns>
        public abstract string GetRandomness();
    }
}