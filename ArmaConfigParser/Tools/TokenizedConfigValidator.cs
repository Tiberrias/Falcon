using System.Collections.Generic;
using ArmaConfigParser.Tokens.Model;

namespace ArmaConfigParser.Tools
{
    public class TokenizedConfigValidator : ITokenizedConfigValidator
    {
        List<Token> _tokenizedConfig;

        public bool Validate(List<Token> tokenizedConfig)
        {
            _tokenizedConfig = tokenizedConfig;
            return HasValidStructure();
        }

        private bool HasValidStructure()
        {
            int treeDepth = 0;
            foreach (Token token in _tokenizedConfig)
            {
                if (token is OpeningToken)
                {
                    treeDepth++;
                }
                else if (token is ClosingToken)
                {
                    treeDepth--;
                }
                if (treeDepth < 0)
                    return false;
            }
            return treeDepth == 0;
        }


    }
}
