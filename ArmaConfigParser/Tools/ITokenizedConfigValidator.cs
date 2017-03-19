using System.Collections.Generic;
using ArmaConfigParser.Tokens.Model;

namespace ArmaConfigParser.Tools
{
    public interface ITokenizedConfigValidator
    {
        bool Validate(List<Token> tokenizedConfig);
    }
}