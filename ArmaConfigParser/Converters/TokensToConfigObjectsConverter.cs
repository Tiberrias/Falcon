using System.Collections.Generic;
using System.Linq;
using ArmaConfigParser.ConfigModel;
using ArmaConfigParser.Tokens.Model;

namespace ArmaConfigParser.Converters
{
    public class TokensToConfigObjectsConverter : ITokensToConfigObjectsConverter
    {
        public List<ConfigObject> Convert(List<Token> configTokens)
        {
            var resultObjects = new List<ConfigObject>();
            var currentTokenTreeDepth = 0;
            var currentToken = -1;
            var firstTokenToFetch = 0;
            var numberOfTokensToFetch = 0;
            var isGettingTokensForInnerFetch = false;

            foreach (var token in configTokens)
            {
                currentToken++;
                if (token is OpeningToken)
                {
                    currentTokenTreeDepth++;
                }
                else if (token is EnclosingToken)
                {
                    currentTokenTreeDepth--;
                }

                if (currentTokenTreeDepth > 0)
                {
                    if (!isGettingTokensForInnerFetch)
                    {
                        firstTokenToFetch = currentToken;
                        isGettingTokensForInnerFetch = true;
                    }
                    numberOfTokensToFetch++;
                    continue;
                }
                if (isGettingTokensForInnerFetch)
                {
                    var fetchedObject =
                        FetchObject(configTokens.Skip(firstTokenToFetch).Take(numberOfTokensToFetch).ToList());
                    resultObjects.Add(fetchedObject);
                    isGettingTokensForInnerFetch = false;
                    numberOfTokensToFetch = 0;
                }
                else
                {
                    if (token is VariableToken)
                    {
                        resultObjects.Add(new ConfigVariable
                        {
                            Name = (token as VariableToken).VariableName,
                            Value = (token as VariableToken).Value
                        });
                    }
                }
            }

            return resultObjects;
        }

        private ConfigObject FetchObject(List<Token> configSection)
        {
            ConfigObject resultObject = null;
            var currentTokenTreeDepth = 0;
            var currentToken = -1;
            var firstTokenToFetch = 0;
            var numberOfTokensToFetch = 0;
            var isGettingTokensForInnerFetch = false;

            foreach (var token in configSection)
            {
                currentToken++;
                if (token is OpeningToken)
                {
                    currentTokenTreeDepth++;
                }
                else if (token is EnclosingToken)
                {
                    currentTokenTreeDepth--;
                }

                if (currentTokenTreeDepth > 0)
                {
                    if (!isGettingTokensForInnerFetch)
                    {
                        firstTokenToFetch = currentToken;
                        isGettingTokensForInnerFetch = true;
                    }
                    numberOfTokensToFetch++;
                    continue;
                }
                if (isGettingTokensForInnerFetch)
                {
                    var fetchedObject =
                        FetchObject(configSection.Skip(firstTokenToFetch).Take(numberOfTokensToFetch).ToList());
                    resultObject = fetchedObject;
                    isGettingTokensForInnerFetch = false;
                    numberOfTokensToFetch = 0;
                }
                else
                {
                    if (token is VariableToken)
                    {
                        resultObject = new ConfigVariable
                        {
                            Name = (token as VariableToken).VariableName,
                            Value = (token as VariableToken).Value
                        };
                    }
                }
            }
            return resultObject;
        }
    }
}