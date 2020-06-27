namespace Kpax.Abstraction.System.IO
{
    using global::System.IO;

    public class FileInfoFacade : IFileInfo
    {
        private readonly FileInfo fileInfo;

        public FileInfoFacade(string filePath) => this.fileInfo = new FileInfo(filePath);

        public FileInfoFacade(FileInfo fileInfo) => this.fileInfo = fileInfo;

        public bool IsReadOnly
        {
            get => this.fileInfo.IsReadOnly;
            set => this.fileInfo.IsReadOnly = value;
        }

        public bool Exists => this.fileInfo.Exists;

        public string DirectoryName => this.fileInfo.DirectoryName;

        public DirectoryInfo Directory => this.fileInfo.Directory;

        public long Length => this.fileInfo.Length;

        public string Name => this.fileInfo.Name;

        public StreamWriter AppendText()
        {
            return this.fileInfo.AppendText();
        }

        public IFileInfo CopyTo(string destFileName, bool overwrite)
        {
            return new FileInfoFacade(this.fileInfo.CopyTo(destFileName, overwrite));
        }

        public IFileInfo CopyTo(string destFileName)
        {
            return new FileInfoFacade(this.fileInfo.CopyTo(destFileName));
        }

        public FileStream Create()
        {
            return this.fileInfo.Create();
        }

        public StreamWriter CreateText()
        {
            return this.fileInfo.CreateText();
        }

        public void Delete()
        {
            this.fileInfo.Delete();
        }

        public void MoveTo(string destFileName)
        {
            this.fileInfo.MoveTo(destFileName);
        }

        public FileStream Open(FileMode mode)
        {
            return this.fileInfo.Open(mode);
        }

        public FileStream Open(FileMode mode, FileAccess access)
        {
            return this.fileInfo.Open(mode, access);
        }

        public FileStream Open(FileMode mode, FileAccess access, FileShare share)
        {
            return this.fileInfo.Open(mode, access, share);
        }

        public FileStream OpenRead()
        {
            return this.fileInfo.OpenRead();
        }

        public StreamReader OpenText()
        {
            return this.fileInfo.OpenText();
        }

        public FileStream OpenWrite()
        {
            return this.fileInfo.OpenWrite();
        }

        public override string ToString()
        {
            return this.fileInfo.ToString();
        }
    }
}