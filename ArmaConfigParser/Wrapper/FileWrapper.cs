using System.IO;

namespace ArmaConfigParser.Wrapper
{
    public class FileWrapper : IFileWrapper
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }
    }
}