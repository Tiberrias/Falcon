using System;
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
            return FetchListOfObjects(configTokens);
        }

        private List<ConfigObject> FetchListOfObjects(List<Token> configSection)
        {
            var resultObjects = new List<ConfigObject>();
            var currentTokenTreeDepth = 0;
            var currentToken = -1;
            var firstTokenToFetch = 0;
            var numberOfTokensToFetch = 1;
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
            
            var sectionBeginningToken = configSection.First();
            var sectionEndingToken = configSection.Last();
            
            if (sectionBeginningToken is VariableToken)
            {
                resultObject = new ConfigVariable
                {
                    Name = (sectionBeginningToken as VariableToken).VariableName,
                    Value = (sectionBeginningToken as VariableToken).Value
                };
                return resultObject;
            }

            var numberOfObjectDefiningTokensToSkip = 1;

            if (sectionBeginningToken is ClassOpeningToken)
            {
                if ((sectionBeginningToken as ClassOpeningToken).ClassName == "data")
                {
                    resultObject = new DataClass()
                    {
                        DataType = ConfigDataTypeHelper.GetType(
                            configSection.First(token => token is StandaloneStringToken) as StandaloneStringToken)
                    };
                    numberOfObjectDefiningTokensToSkip = 6;
                }
                else if ((sectionBeginningToken as ClassOpeningToken).ClassName == "Item")
                {
                    resultObject = new ItemClass();
                }
                else
                {
                    resultObject = new GeneralClass();
                }
            }
            

            var currentTokenTreeDepth = 0;
            var currentToken = -1;
            var firstTokenToFetch = 0;
            var numberOfTokensToFetch = 1;
            var isGettingTokensForInnerFetch = false;

            foreach (var token in configSection.Skip(numberOfObjectDefiningTokensToSkip).Take(configSection.Count - numberOfObjectDefiningTokensToSkip - 1))
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
            }
            return resultObject;
        }
    }
}