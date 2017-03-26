using System.Collections.Generic;
using ArmaConfigParser.Tokens.Model;
using ArmaConfigParser.Tools;
using ArmaConfigParser.Tools.Interfaces;
using Falcon.Core.Services.Interfaces;

namespace Falcon.Core.Services
{
    class ArsenalEquipmentExtractionService : IArsenalEquipmentExtractionService
    {
        private ITokenizedConfigValidator _tokenizedConfigValidator;

        public ArsenalEquipmentExtractionService(ITokenizedConfigValidator tokenizedConfigValidator)
        {
            _tokenizedConfigValidator = tokenizedConfigValidator;
        }

        public List<Token> ExtractEntireVirtualArsenalTokens(List<Token> varsConfigTokens)
        {
            List<Token> resulTokens = new List<Token>();
            var foundVirtualArsenalPartOfConfig = false;
            var isTokenAfterClassOpeningToken = false;
            var currentTokenTreeDepth = 0;

            foreach (var token in varsConfigTokens)
            {
                if (token is OpeningToken)
                {
                    currentTokenTreeDepth++;
                }
                else if (token is EnclosingToken)
                {
                    currentTokenTreeDepth--;
                }

                if (!foundVirtualArsenalPartOfConfig &&
                    isTokenAfterClassOpeningToken &&
                    currentTokenTreeDepth == 1 &&
                    (token is VariableToken) &&
                    (token as VariableToken).VariableName == "name" &&
                    (token as VariableToken).Value as string == "bis_fnc_saveinventory_data")
                {
                    foundVirtualArsenalPartOfConfig = true;
                    resulTokens.Add(new ClassOpeningToken("VA"));
                }

                if (foundVirtualArsenalPartOfConfig)
                {
                    resulTokens.Add(token);

                    if (currentTokenTreeDepth < 0)
                    {
                        
                    }
                }

                isTokenAfterClassOpeningToken = (token is ClassOpeningToken);
            }

            if (!_tokenizedConfigValidator.Validate(resulTokens))
            {
               
            }

            return resulTokens;
        }
    }
}