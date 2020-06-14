namespace Kpax.Abstraction.System.IO
{
    using global::System.Collections.Generic;
    using global::System.IO;

    public class FileFacade : IFileFacade
    {
        public byte[] ReadAllBytes(string path)
        {
            return File.ReadAllBytes(path);
        }

        public string[] ReadAllLines(string path)
        {
            return File.ReadAllLines(path);
        }

        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        public IEnumerable<string> ReadLines(string path)
        {
            return File.ReadLines(path);
        }

        public bool IsFile(string path)
        {
            FileAttributes attr = File.GetAttributes(path);

            return (attr & FileAttributes.Directory) == 0;
        }

        public IFileInfo GetFileInfo(string path)
        {
            return new FileInfoFacade(path);
        }
    }
}