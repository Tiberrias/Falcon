using System.Collections.Generic;
using ArmaConfigParser.ConfigModel;
using Falcon.Core.Model.Loadouts;

namespace Falcon.Core.Converters.Interfaces
{
    public interface IDataClassToLoadoutListConverter
    {
        List<Loadout> Convert(DataClass inventoryData);
    }
}