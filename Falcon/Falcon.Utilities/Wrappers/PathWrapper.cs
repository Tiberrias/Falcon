using System.Diagnostics.CodeAnalysis;
using System.IO;
using Falcon.Utilities.Wrappers.Interfaces;

namespace Falcon.Utilities.Wrappers
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