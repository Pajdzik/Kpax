namespace Kpax.Abstraction.System.IO
{
    public interface IDirectoryFacade
    {
        bool Exists(string path);

        string[] GetFiles(string path);
    }
}