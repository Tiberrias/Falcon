using System;
using System.Collections.Generic;
using System.Linq;
using ArmaConfigParser.Tokens.Model;
using ArmaConfigParser.Tools.Interfaces;
using Falcon.Core.Services.Interfaces;

namespace Falcon.Core.Services
{
    //TODO: redesign system to have this service work on more useful constructs than tokens

    public class ArsenalEquipmentExtractionService : IArsenalEquipmentExtractionService
    {
        private readonly ITokenizedConfigValidator _tokenizedConfigValidator;

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
                    currentTokenTreeDepth == 2 &&
                    IsInventoryDataBeginningVariableToken(token))
                {
                    foundVirtualArsenalPartOfConfig = true;
                    resulTokens.Add(new ClassOpeningToken("VA"));
                }

                if (foundVirtualArsenalPartOfConfig)
                {
                    resulTokens.Add(token);

                    if (currentTokenTreeDepth < 2)
                    {
                        break;
                    }
                }

                isTokenAfterClassOpeningToken = token is ClassOpeningToken;
            }

            if (!_tokenizedConfigValidator.Validate(resulTokens))
            {
               throw new Exception("Failed to extract equipment data. Invalid token list.");
            }

            return resulTokens;
        }

        public List<List<Token>> SplitVirtualArsenalTokensIntoLoadoutsTokens(List<Token> virtualArsenalTokens)
        {
            throw new NotImplementedException();
        }

        private static bool IsInventoryDataBeginningVariableToken(Token token)
        {
            return token is VariableToken &&
                   (token as VariableToken).VariableName == "name" &&
                   (token as VariableToken).Value as string == "bis_fnc_saveinventory_data";
        }
    }
}