using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ArmaConfigParser.ConfigModel;
using ArmaConfigParser.Converters.Interfaces;
using ArmaConfigParser.Helpers;
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
                    numberOfTokensToFetch = 1;
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
            if (configSection.Count == 0)
            {
                return null;
            }

            ConfigObject resultObject = null;
            
            var sectionBeginningToken = configSection.First();

            if (sectionBeginningToken is VariableToken)
            {
                resultObject = new ConfigVariable
                {
                    Name = (sectionBeginningToken as VariableToken).VariableName,
                    Value = (sectionBeginningToken as VariableToken).Value
                };
                return resultObject;
            }

            if (sectionBeginningToken is ClassOpeningToken)
            {
                if ((sectionBeginningToken as ClassOpeningToken).ClassName == "data")
                {
                    var dataClassType = ConfigDataTypeHelper.GetType(
                        configSection.OfType<StandaloneStringToken>().First());

                    var dataObject = (FetchObject(configSection.Skip(6).Take(configSection.Count - 7).ToList()));
                    dynamic dataValue = ExtractDataValue(dataClassType, dataObject);
                    
                    resultObject = new DataClass()
                    {
                        DataType = dataClassType,
                        Value = dataValue
                    };
                }
                else if (Regex.Match((sectionBeginningToken as ClassOpeningToken).ClassName, @"^Item\d+$").Success)
                {
                    var fetchedObjects = FetchListOfObjects(configSection.Skip(1).Take(configSection.Count - 2).ToList());
                    var fetchedReadOnlyProperty =
                    (fetchedObjects.FirstOrDefault(configObject => (configObject as ConfigVariable)?.Name == "readOnly")
                        as
                        ConfigVariable)?.Value;
                    resultObject = new ItemClass
                    {
                        Name =
                        (fetchedObjects.FirstOrDefault(configObject => (configObject as ConfigVariable)?.Name == "name") as
                            ConfigVariable)?.Value.ToString(),
                        Data = fetchedObjects.OfType<DataClass>().First(),
                        ReadOnly = fetchedReadOnlyProperty == null ? default(bool?) : (int)fetchedReadOnlyProperty == 1
                    };
                    
                }
                else
                {
                    resultObject = new GeneralClass()
                    {
                        ClassName = (sectionBeginningToken as ClassOpeningToken).ClassName,
                        Content = FetchListOfObjects(configSection.Skip(1).Take(configSection.Count-2).ToList())
                    };
                }
            }

            return resultObject;
        }

        private static dynamic ExtractDataValue(ConfigDataType dataType, ConfigObject data)
        {
            dynamic dataValue;
            switch (dataType)
            {
                case ConfigDataType.Bool:
                    dataValue = System.Convert.ToBoolean((data as ConfigVariable).Value);
                    break;
                case ConfigDataType.Array:
                    if (data == null)
                    {
                        dataValue = null;
                    }
                    else
                    {
                        dataValue = (data as GeneralClass).Content;
                    }
                    break;
                case ConfigDataType.String:
                    dataValue = (data as ConfigVariable).Value.ToString();
                    break;
                case ConfigDataType.Scalar:
                    dataValue = (data as ConfigVariable).Value;
                    break;
                case ConfigDataType.Nothing:
                    dataValue = null;
                    break;
                default:
                    throw new Exception();
            }
            return dataValue;
        }
    }
}