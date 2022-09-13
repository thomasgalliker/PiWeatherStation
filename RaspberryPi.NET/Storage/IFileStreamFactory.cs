using System.IO;

namespace RaspberryPi.Storage
{
    /// <inheritdoc cref="FileStream" />
    public interface IFileStreamFactory
    {
        /// <inheritdoc cref="FileStream(string,FileMode)" />
        Stream Create(string path, FileMode mode);

        /// <inheritdoc cref="FileStream(string,FileMode,FileAccess)" />
        Stream Create(string path, FileMode mode, FileAccess access);

        /// <inheritdoc cref="StreamReader(string,FileMode,FileAccess)" />
        StreamReader CreateStreamReader(string path, FileMode mode, FileAccess access);

        /// <inheritdoc cref="StreamWriter(string,FileMode,FileAccess)" />
        StreamWriter CreateStreamWriter(string path, FileMode mode, FileAccess access);

        /// <inheritdoc cref="FileStream(string,FileMode,FileAccess,FileShare)" />
        Stream Create(string path, FileMode mode, FileAccess access, FileShare share);

        /// <inheritdoc cref="FileStream(string,FileMode,FileAccess,FileShare,int)" />
        Stream Create(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize);

        /// <inheritdoc cref="FileStream(string,FileMode,FileAccess,FileShare,int,FileOptions)" />
        Stream Create(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, FileOptions options);

        /// <inheritdoc cref="FileStream(string,FileMode,FileAccess,FileShare,int,bool)" />
        Stream Create(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, bool useAsync);
    }
}