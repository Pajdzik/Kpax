namespace Eks.Abstraction.System.IO
{
    using global::System.Collections.Generic;
    using global::System;

    public interface IFileFacade
    {
        byte[] ReadAllBytes(string path);

        string[] ReadAllLines(string path);

        string ReadAllText(string path);

        IEnumerable<string> ReadLines(string path);
    }
}