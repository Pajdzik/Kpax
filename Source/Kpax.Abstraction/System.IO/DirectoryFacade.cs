﻿namespace Kpax.Abstraction.System.IO
{
    using global::System.IO;

    public class DirectoryFacade : IDirectoryFacade
    {
        public bool Exists(string path)
        {
            return Directory.Exists(path);
        }

        public string[] GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }

        public bool IsDirectory(string path)
        {
            FileAttributes attr = File.GetAttributes(path);

            return (attr & FileAttributes.Directory) == FileAttributes.Directory;
        }
    }
}