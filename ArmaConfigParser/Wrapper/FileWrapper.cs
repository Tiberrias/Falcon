using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace ArmaConfigParser.Wrapper
{
    [ExcludeFromCodeCoverage]
    public class FileWrapper : IFileWrapper
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }
    }
}