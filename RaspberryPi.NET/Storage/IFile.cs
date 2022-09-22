using System.IO;

namespace RaspberryPi.Storage
{
    public interface IFile
    {
        void Delete(string path);

        bool Exists(string path);
        string[] ReadAllLines(string path);
        void WriteAllText(string path, string contents);
    }
}