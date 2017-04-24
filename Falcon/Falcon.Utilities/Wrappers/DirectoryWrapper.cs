using System.IO;
using Falcon.Utilities.Wrappers.Interfaces;

namespace Falcon.Utilities.Wrappers
{
    public class DirectoryWrapper : IDirectoryWrapper
    {
        public string[] GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }
    }
}