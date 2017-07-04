namespace Eks.Abstraction.System.IO
{
    public interface IDirectoryProxy
    {
        bool Exists(string path);

        string[] GetFiles(string path);
    }
}