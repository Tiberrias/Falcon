using System.Diagnostics.CodeAnalysis;
using System.IO;
using ArmaConfigParser.Wrapper.Interfaces;

namespace ArmaConfigParser.Wrapper
{
    [ExcludeFromCodeCoverage]
    public class PathWrapper : IPathWrapper
    {
        public string GetTempFileName()
        {
            return Path.GetTempFileName();
        }
    }
}