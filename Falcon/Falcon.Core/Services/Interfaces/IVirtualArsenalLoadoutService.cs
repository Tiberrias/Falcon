using System.Collections.Generic;
using Falcon.Core.Model;

namespace Falcon.Core.Services.Interfaces
{
    public interface IVirtualArsenalLoadoutService
    {
        List<Loadout> ImportLoadouts(string configVarsFilepath);

        List<string> GetPossibleConfigVarsFilepaths();
    }
}