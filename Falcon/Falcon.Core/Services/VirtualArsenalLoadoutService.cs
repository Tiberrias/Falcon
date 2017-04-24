using System;
using System.Collections.Generic;
using System.Linq;
using ArmaConfigParser.ConfigReader.Interfaces;
using Falcon.Core.Configuration;
using Falcon.Core.Converters.Interfaces;
using Falcon.Core.Model;
using Falcon.Core.Services.Interfaces;
using Falcon.Utilities.Wrappers.Interfaces;

namespace Falcon.Core.Services
{
    public class VirtualArsenalLoadoutService : IVirtualArsenalLoadoutService
    {
        private readonly IConfigExtractionService _configExtractionService;
        private readonly IArsenalEquipmentExtractionService _arsenalEquipmentExtractionService;
        private readonly IDataClassToLoadoutListConverter _classToLoadoutListConverter;
        private readonly IDirectoryWrapper _directoryWrapper;
        private readonly IEnvironmentWrapper _environmentWrapper;
        private readonly IConfigurationService _configurationService;

        public VirtualArsenalLoadoutService(IConfigExtractionService configExtractionService,
            IArsenalEquipmentExtractionService arsenalEquipmentExtractionService,
            IDataClassToLoadoutListConverter classToLoadoutListConverter,
            IDirectoryWrapper directoryWrapper,
            IEnvironmentWrapper environmentWrapper,
            IConfigurationService configurationService)
        {
            _configExtractionService = configExtractionService;
            _arsenalEquipmentExtractionService = arsenalEquipmentExtractionService;
            _classToLoadoutListConverter = classToLoadoutListConverter;
            _directoryWrapper = directoryWrapper;
            _environmentWrapper = environmentWrapper;
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
            var fullProfileVarsFileSearchLocation =
                _environmentWrapper.ExpandEnvironmentVariables(
                    _configurationService.DefaultProfileVarsFileSearchLocation);
            var files =  _directoryWrapper.GetFiles(fullProfileVarsFileSearchLocation);

            var possibleConfigVarsFiles = files.Where(x => x.EndsWith(".vars.Arma3Profile")).ToList();
            return possibleConfigVarsFiles;
        }
    }
}