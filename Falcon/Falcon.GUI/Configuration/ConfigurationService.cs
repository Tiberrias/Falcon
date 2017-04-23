using System;
using System.Configuration;
using ArmaConfigParser.Configuration;

namespace Falcon.GUI.Configuration
{
    public class ConfigurationService : IConfigurationService
    {
        public int ConfigFileDebinarizationTimeout => Int32.Parse(ConfigurationManager.AppSettings[nameof(ConfigFileDebinarizationTimeout)]);

        public string Arma3ToolsCfgConvertExecutablePath => ConfigurationManager.AppSettings["ArmaToolsCfgConvertPath"];
    }
}