using System;
using System.Collections.Generic;
using ArmaConfigParser.ConfigModel;

namespace Falcon.Core.Services.Interfaces
{
    public interface IArsenalEquipmentExtractionService
    {
        DataClass GetInventoryData(List<ConfigObject> profileVars);
    }
}