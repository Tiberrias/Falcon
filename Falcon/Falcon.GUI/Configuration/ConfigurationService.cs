using System;
using System.Configuration;
using ArmaConfigParser.Configuration;

namespace Falcon.GUI.Configuration
{
    public class ConfigurationService : IConfigurationService, Core.Configuration.IConfigurationService
    {
        public int ConfigFileDebinarizationTimeout
            => Int32.Parse(ConfigurationManager.AppSettings[nameof(ConfigFileDebinarizationTimeout)]);

        public string Arma3ToolsCfgConvertExecutablePath
            => ConfigurationManager.AppSettings[nameof(Arma3ToolsCfgConvertExecutablePath)];

        public string DefaultProfileVarsFileSearchLocation
            => ConfigurationManager.AppSettings[nameof(DefaultProfileVarsFileSearchLocation)];

        public string OtherProfilesVarsFileSearchLocation
            => ConfigurationManager.AppSettings[nameof(OtherProfilesVarsFileSearchLocation)];

        public string CustomProfileVarsFileSearchLocation
            => ConfigurationManager.AppSettings[nameof(CustomProfileVarsFileSearchLocation)];
    }
}