using System.IO;
using ArmaConfigParser.Wrapper.Interfaces;

namespace ArmaConfigParser.Wrapper
{
    class DirectoryWrapper : IDirectoryWrapper
    {
        public string[] GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }
    }
}