using System;
using System.Collections.Generic;
using System.Linq;
using ArmaConfigParser.ConfigReader.Interfaces;
using ArmaConfigParser.Wrapper.Interfaces;
using Falcon.Core.Configuration;
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
        private readonly IDirectoryWrapper _directoryWrapper;
        private readonly IConfigurationService _configurationService;

        public VirtualArsenalLoadoutService(IConfigExtractionService configExtractionService, IArsenalEquipmentExtractionService arsenalEquipmentExtractionService, IDataClassToLoadoutListConverter classToLoadoutListConverter, IDirectoryWrapper directoryWrapper, IConfigurationService configurationService)
        {
            _configExtractionService = configExtractionService;
            _arsenalEquipmentExtractionService = arsenalEquipmentExtractionService;
            _classToLoadoutListConverter = classToLoadoutListConverter;
            _directoryWrapper = directoryWrapper;
            _configurationService = configurationService;
        }

        public List<Loadout> ImportLoadouts(string configVarsFilepath)
        {
            var configObjects = _configExtractionService.Extract(configVarsFilepath);
            var arsenalData = _arsenalEquipmentExtractionService.GetInventoryData(configObjects);
            var loadouts = _classToLoadoutListConverter.Convert(arsenalData);

            return loadouts;
        }

        public List<string> GetPossibleConfigVarsFilepaths()
        {
            var files =  _directoryWrapper.GetFiles(Environment.ExpandEnvironmentVariables(_configurationService.DefaultProfileVarsFileSearchLocation));

            var possibleConfigVarsFiles = files.Where(x => x.EndsWith(".vars.Arma3Profile")).ToList();
            return possibleConfigVarsFiles;
        }
    }
}