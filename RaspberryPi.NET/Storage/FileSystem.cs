using System.IO;

namespace RaspberryPi.Storage
{
    public class FileSystem : IFileSystem
    {
        public FileSystem()
        {
            this.FileStreamFactory = new FileStreamFactory();
            this.File = new File();
        }

        public IFile File { get; }

        public IFileStreamFactory FileStreamFactory { get; }
    }
}