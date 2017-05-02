using System.Collections.Generic;
using Falcon.Core.Model.Loadouts;

namespace Falcon.Core.Services.Interfaces
{
    public interface IVirtualArsenalLoadoutService
    {
        List<Loadout> ImportLoadouts(string configVarsFilepath);

    }
}