using System.Collections.Generic;
using ArmaConfigParser.Tokens.Model;
using ArmaConfigParser.Tools.Interfaces;
using Falcon.Core.Services;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace FalconTests.Falcon.Core.Services
{
    [TestFixture]
    public class ArsenalEquipmentExtractionServiceTests
    {
        private ArsenalEquipmentExtractionService _arsenalEquipmentExtractionService;
        private Mock<ITokenizedConfigValidator> _tokenizedConfigValidator;

        [SetUp]
        public void SetUp()
        {
            _tokenizedConfigValidator = new Mock<ITokenizedConfigValidator>();
            _arsenalEquipmentExtractionService = new ArsenalEquipmentExtractionService(_tokenizedConfigValidator.Object);
        }

        [Test]
        public void ExtractEntireVirtualArsenalTokens_ExampleData_ExtractsProperTokens()
        {
            //Arrange
            _tokenizedConfigValidator.Setup(x => x.Validate(It.IsAny<List<Token>>())).Returns(true);
            var inputTokens = getExampleGoodInputTokens();
            var expectedTokens = getExpectedTokens();

            //Act
            var resultTokens = _arsenalEquipmentExtractionService.ExtractEntireVirtualArsenalTokens(inputTokens);

            //Assert
            resultTokens.ShouldBeEquivalentTo(expectedTokens);
        }

        private List<Token> getExampleGoodInputTokens()
        {
            return new List<Token>
            {
                new VariableToken("version", 2),
                new ClassOpeningToken("ProfileVariables"),
                new VariableToken("items", 581),
                new VariableToken("slots", 63),
                new ClassOpeningToken("Item0"),
                new VariableToken("something", "something"),
                new ClosingToken(),
                new ClassOpeningToken("Item548"),
                new VariableToken("name", "bis_fnc_saveinventory_data"),
                new ClassOpeningToken("data"),
                new ClassOpeningToken("type"),
                new TypeOpeningToken(),
                new StandaloneStringToken("ARRAY"),
                new ClosingToken(),
                new ClosingToken(),
                new ClassOpeningToken("value"),
                new VariableToken("items", 482),
                new ClassOpeningToken("Item0"),
                new ClassOpeningToken("data"),
                new ClassOpeningToken("type"),
                new TypeOpeningToken(),
                new StandaloneStringToken("STRING"),
                new ClosingToken(),
                new ClosingToken(),
                new VariableToken("value", "pusty"),
                new ClosingToken(),
                new ClosingToken(),
                new ClosingToken(),
                new ClosingToken(),
                new ClosingToken(),
                new ClassOpeningToken("Item549"),
                new VariableToken("something", "something"),
                new ClosingToken(),
                new ClosingToken()
            };
        }

        private List<Token> getExpectedTokens()
        {
            return new List<Token>
            {
                new ClassOpeningToken("VA"),
                new VariableToken("name", "bis_fnc_saveinventory_data"),
                new ClassOpeningToken("data"),
                new ClassOpeningToken("type"),
                new TypeOpeningToken(),
                new StandaloneStringToken("ARRAY"),
                new ClosingToken(),
                new ClosingToken(),
                new ClassOpeningToken("value"),
                new VariableToken("items", 482),
                new ClassOpeningToken("Item0"),
                new ClassOpeningToken("data"),
                new ClassOpeningToken("type"),
                new TypeOpeningToken(),
                new StandaloneStringToken("STRING"),
                new ClosingToken(),
                new ClosingToken(),
                new VariableToken("value", "pusty"),
                new ClosingToken(),
                new ClosingToken(),
                new ClosingToken(),
                new ClosingToken(),
                new ClosingToken(),
            };
        }
    }
}