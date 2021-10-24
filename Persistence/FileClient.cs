using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Persistence
{

    public class FileClient : IFileClient
    {
        public void Append<T>(string filename, T item)
        {
            var jsonItem = JsonSerializer.Serialize(item);

            File.AppendAllLines(filename, new[] { jsonItem });
        }

        public void DeleteFileContent(string filename)
        {
            File.WriteAllLines(filename, Array.Empty<string>());
        }

        public IEnumerable<T> ReadAll<T>(string filename)
        {
            var jsonItems = File.ReadAllLines(filename);

            return jsonItems.Select(jsonItem => JsonSerializer.Deserialize<T>(jsonItem));
        }

        public void WriteAll<T>(string filename, IEnumerable<T> items)
        {

            var jsonItems = items.Select(item => JsonSerializer.Serialize(item));

            File.WriteAllLines(filename, jsonItems);
        }
    }
}
