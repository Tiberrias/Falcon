using System.Diagnostics.CodeAnalysis;
using System.IO;
using Falcon.Utilities.Wrappers.Interfaces;

namespace Falcon.Utilities.Wrappers
{
    [ExcludeFromCodeCoverage]
    public class FileWrapper : IFileWrapper
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }
    }
}