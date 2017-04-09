using System;
using System.Collections.Generic;
using System.Linq;
using ArmaConfigParser.ConfigModel;
using Falcon.Core.Services.Interfaces;

namespace Falcon.Core.Services
{
    public class ArsenalEquipmentExtractionService : IArsenalEquipmentExtractionService
    {
        private const string InventoryDataString = "bis_fnc_saveinventory_data";
        private const string ProfileVariablesString = "ProfileVariables";

        public DataClass GetInventoryData(List<ConfigObject> profileVars)
        {
            var innerData =
                profileVars?.OfType<GeneralClass>().FirstOrDefault(item => item.ClassName == ProfileVariablesString);
            var inventoryClass =
                innerData?.Content.OfType<ItemClass>().FirstOrDefault(item => item.Name == InventoryDataString);
            if (inventoryClass != null)
            {
                return inventoryClass.Data;
            }
            throw new ArgumentException("No inventory data found in config variables.");
        }
    }
}