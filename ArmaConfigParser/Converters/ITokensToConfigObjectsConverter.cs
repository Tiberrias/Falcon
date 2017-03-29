using System.Collections.Generic;
using ArmaConfigParser.ConfigModel;
using ArmaConfigParser.Tokens.Model;

namespace ArmaConfigParser.Converters
{
    public interface ITokensToConfigObjectsConverter
    {
        List<ConfigObject> Convert(List<Token> configTokens);
    }
}