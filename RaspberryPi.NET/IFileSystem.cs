namespace RaspberryPi
{
    public interface IFileSystem
    {
        void Delete(string path);
        
        bool Exists(string path);
        
        void WriteAllText(string path, string contents);
    }
}