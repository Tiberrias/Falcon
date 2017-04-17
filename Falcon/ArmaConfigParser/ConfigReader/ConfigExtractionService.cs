using System.Collections.Generic;
using System.Linq;
using ArmaConfigParser.ConfigModel;
using ArmaConfigParser.ConfigReader.Interfaces;
using ArmaConfigParser.Converters.Interfaces;
using ArmaConfigParser.Tokens;

namespace ArmaConfigParser.ConfigReader
{
    public class ConfigExtractionService : IConfigExtractionService
    {
        private readonly IConfigLoader _configLoader;
        private readonly ITokenizer _tokenizer;
        private readonly ITokensToConfigObjectsConverter _converter;

        public ConfigExtractionService(IConfigLoader configLoader, ITokenizer tokenizer, ITokensToConfigObjectsConverter converter)
        {
            _configLoader = configLoader;
            _tokenizer = tokenizer;
            _converter = converter;
        }

        public List<ConfigObject> Extract(string configFilePath)
        {
            var debinarizedConfig = _configLoader.Load(configFilePath);

            _tokenizer.Initialize(debinarizedConfig);
            var tokenizedConfig = _tokenizer.Tokenize().ToList();

            var result = _converter.Convert(tokenizedConfig);
            return result;
        }
    }
}