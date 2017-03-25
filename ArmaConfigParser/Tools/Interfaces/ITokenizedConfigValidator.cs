using System.Collections.Generic;
using ArmaConfigParser.Tokens.Model;

namespace ArmaConfigParser.Tools.Interfaces
{
    public interface ITokenizedConfigValidator
    {
        bool Validate(List<Token> tokenizedConfig);
    }
}