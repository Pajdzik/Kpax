namespace Kpax.Abstraction.System.IO
{
    using global::System.Collections.Generic;

    public interface IFileFacade
    {
        byte[] ReadAllBytes(string path);

        string[] ReadAllLines(string path);

        string ReadAllText(string path);

        IEnumerable<string> ReadLines(string path);

        bool IsFile(string path);

        IFileInfo GetFileInfo(string path);
    }
}