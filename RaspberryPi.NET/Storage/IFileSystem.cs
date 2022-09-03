namespace RaspberryPi.Storage
{
    public interface IFileSystem
    {
        IFile File { get; }

        IFileStreamFactory FileStreamFactory { get; }
    }
}