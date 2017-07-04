namespace Eks.Abstraction.System.IO
{
    using global::System.IO;

    public class DirectoryProxy : IDirectoryProxy
    {
        public bool Exists(string path)
        {
            return Directory.Exists(path);
        }

        public string[] GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }
    }
}