using ArmaConfigParser.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmaConfigParser.Tools
{
    public class TokenizedConfigValidator
    {
        List<Token> _tokenizedConfig;

        public bool Validate(List<Token> tokenizedConfig)
        {
            _tokenizedConfig = tokenizedConfig;
            return HasValidStructure();
        }

        private bool HasValidStructure()
        {
            int TreeDepth = 0;
            foreach (Token token in _tokenizedConfig)
            {
                if (token is OpeningToken)
                {
                    TreeDepth++;
                }
                else if (token is ClosingToken)
                {
                    TreeDepth--;
                }
                if (TreeDepth < 0)
                    return false;
            }
            return TreeDepth == 0;
        }


    }
}
