namespace Eks.Abstraction.System.IO
{
    using global::System.IO;

    public class PathFacade : IPathFacade
    {
        public string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }
    }
}