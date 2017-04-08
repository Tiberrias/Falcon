using System.Collections.Generic;
using ArmaConfigParser.Tokens.Model;

namespace ArmaConfigParser.Tokens
{
    public interface ITokenizer
    {
        void Initialize(string text);

        IEnumerable<Token> Tokenize();
    }
}