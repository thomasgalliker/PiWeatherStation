namespace RaspberryPi.Storage
{
    public class File : IFile
    {
        public string[] ReadAllLines(string path)
        {
            return System.IO.File.ReadAllLines(path);
        }
        
        public void Delete(string path)
        {
            System.IO.File.Delete(path);
        }

        public bool Exists(string path)
        {
            return System.IO.File.Exists(path);
        }

        public void WriteAllText(string path, string contents)
        {
            System.IO.File.WriteAllText(path, contents);
        }
    }
}