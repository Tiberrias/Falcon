using System.Collections.Generic;
using ArmaConfigParser.Tokens.Model;

namespace Falcon.Core.Services.Interfaces
{
    public interface IArsenalEquipmentExtractionService
    {
        List<Token> ExtractEntireVirtualArsenalTokens(List<Token> varsConfigTokens);
    }
}