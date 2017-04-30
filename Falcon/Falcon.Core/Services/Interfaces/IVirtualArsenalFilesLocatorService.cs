using System.Collections.Generic;

namespace Falcon.Core.Services.Interfaces
{
    public interface IVirtualArsenalFilesLocatorService
    {
        List<string> GetPossibleConfigVarsFilepaths();
    }
}