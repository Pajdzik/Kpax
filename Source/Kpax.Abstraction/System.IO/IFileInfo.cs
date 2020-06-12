namespace Kpax.Abstraction.System.IO
{
    using global::System.IO;

    public interface IFileInfo
    {
        bool IsReadOnly { get; set; }

        bool Exists { get; }

        string DirectoryName { get; }

        DirectoryInfo Directory { get; }

        long Length { get; }

        string Name { get; }

        StreamWriter AppendText();

        IFileInfo CopyTo(string destFileName, bool overwrite);

        IFileInfo CopyTo(string destFileName);

        FileStream Create();

        StreamWriter CreateText();

        void Delete();

        void MoveTo(string destFileName);

        FileStream Open(FileMode mode);

        FileStream Open(FileMode mode, FileAccess access);

        FileStream Open(FileMode mode, FileAccess access, FileShare share);

        FileStream OpenRead();

        StreamReader OpenText();

        FileStream OpenWrite();

        string ToString();
    }
}