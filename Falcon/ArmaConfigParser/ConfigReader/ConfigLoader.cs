using ArmaConfigParser.ConfigReader.Interfaces;
using ArmaConfigParser.Configuration;
using Falcon.Utilities.Wrappers.Interfaces;

namespace ArmaConfigParser.ConfigReader
{
    public class ConfigLoader : IConfigLoader
    {
        private readonly IConfigurationService _configurationService;
        private readonly IConfigDebinarizer _configDebinarizer;
        private readonly IPathWrapper _pathWrapper;
        private readonly IFileWrapper _fileWrapper;

        public ConfigLoader(IConfigurationService configurationService,
            IConfigDebinarizer configDebinarizer,
            IPathWrapper pathWrapper,
            IFileWrapper fileWrapper)
        {
            _configurationService = configurationService;
            _configDebinarizer = configDebinarizer;
            _pathWrapper = pathWrapper;
            _fileWrapper = fileWrapper;
        }

        public string Load(string configFilePath)
        {
            _configDebinarizer.Initialize(_configurationService.Arma3ToolsCfgConvertExecutablePath);
            var debinarizedFilePath = _pathWrapper.GetTempFileName();
            _configDebinarizer.DebinarizeConfigFile(configFilePath, debinarizedFilePath);

            return _fileWrapper.ReadAllText(debinarizedFilePath);
        }
    }
}