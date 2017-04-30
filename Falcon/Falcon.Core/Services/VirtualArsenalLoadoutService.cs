using System.Collections.Generic;
using ArmaConfigParser.ConfigReader.Interfaces;
using Falcon.Core.Converters.Interfaces;
using Falcon.Core.Model;
using Falcon.Core.Services.Interfaces;

namespace Falcon.Core.Services
{
    public class VirtualArsenalLoadoutService : IVirtualArsenalLoadoutService
    {
        private readonly IConfigExtractionService _configExtractionService;
        private readonly IArsenalEquipmentExtractionService _arsenalEquipmentExtractionService;
        private readonly IDataClassToLoadoutListConverter _classToLoadoutListConverter;


        public VirtualArsenalLoadoutService(IConfigExtractionService configExtractionService,
            IArsenalEquipmentExtractionService arsenalEquipmentExtractionService,
            IDataClassToLoadoutListConverter classToLoadoutListConverter)
        {
            _configExtractionService = configExtractionService;
            _arsenalEquipmentExtractionService = arsenalEquipmentExtractionService;
            _classToLoadoutListConverter = classToLoadoutListConverter;
        }

        public List<Loadout> ImportLoadouts(string configVarsFilepath)
        {
            var configObjects = _configExtractionService.Extract(configVarsFilepath);
            var arsenalData = _arsenalEquipmentExtractionService.GetInventoryData(configObjects);
            var loadouts = _classToLoadoutListConverter.Convert(arsenalData);

            return loadouts;
        }
    }
}