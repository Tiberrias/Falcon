using System.Collections.Generic;
using ArmaConfigParser.ConfigModel;

namespace ArmaConfigParser.ConfigReader.Interfaces
{
    public interface IConfigExtractionService
    {
        List<ConfigObject> Extract(string configFilePath);
    }
}