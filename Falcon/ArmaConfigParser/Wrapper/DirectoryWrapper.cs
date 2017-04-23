using System.IO;
using ArmaConfigParser.Wrapper.Interfaces;

namespace ArmaConfigParser.Wrapper
{
    public class DirectoryWrapper : IDirectoryWrapper
    {
        public string[] GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }
    }
}