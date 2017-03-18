using System.Collections.Generic;
using ArmaConfigParser.Tokens.Model;

namespace ArmaConfigParser.Tokens
{
    public interface ITokenizer
    {
        IEnumerable<Token> Tokenize();
    }
}