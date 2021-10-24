using System.Collections.Generic;

namespace Persistence
{
    public interface IFileClient
    {
        IEnumerable<T> ReadAll<T>(string filename);

        void Append<T>(string filename, T item);

        void WriteAll<T>(string filename, IEnumerable<T> items);

        void DeleteFileContent(string filename);
    }
}
