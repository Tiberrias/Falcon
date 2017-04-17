namespace ArmaConfigParser.Configuration
{
    public interface IConfigurationService
    {
        int ConfigFileDebinarizationTimeout { get; }

        string Arma3ToolsCfgConvertExecutablePath { get; }
    }
}