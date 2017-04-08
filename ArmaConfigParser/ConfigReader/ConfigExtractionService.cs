using System.Collections.Generic;
using ArmaConfigParser.ConfigModel;
using ArmaConfigParser.ConfigReader.Interfaces;

namespace ArmaConfigParser.ConfigReader
{
    public class ConfigExtractionService : IConfigExtractionService
    {
        private IConfigLoader _configLoader;

        public ConfigExtractionService(IConfigLoader configLoader)
        {
            _configLoader = configLoader;
            
        }

        public List<ConfigObject> Extract(string configFilePath)
        {
            var debinarizedConfig = _configLoader.Load(configFilePath);
            return null;
        }
    }
}