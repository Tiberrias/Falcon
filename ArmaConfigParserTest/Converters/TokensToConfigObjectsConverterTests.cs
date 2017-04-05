using System.Collections.Generic;
using ArmaConfigParser.ConfigModel;
using ArmaConfigParser.Converters;
using ArmaConfigParser.Tokens.Model;
using FluentAssertions;
using NUnit.Framework;

namespace ArmaConfigParserTest.Converters
{
    [TestFixture]
    public class TokensToConfigObjectsConverterTests
    {
        private TokensToConfigObjectsConverter _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new TokensToConfigObjectsConverter();
        }

        [Test]
        public void Convert_LooseVariables_ConvertsAsExpected()
        {
            //Arrange
            var expectedConfigObjects = new List<ConfigObject>
            {
                new ConfigVariable {Name = "version", Value = 2},
                new ConfigVariable {Name = "something", Value = "some value"},
            };
            var inputTokens = new List<Token>
            {
                new VariableToken("version", 2),
                new VariableToken("something", "some value")
            };

            //Act
            var result = _sut.Convert(inputTokens);

            //Assert
            result.ShouldAllBeEquivalentTo(expectedConfigObjects,
                options =>
                    options.IncludingNestedObjects().IncludingFields().IncludingProperties().RespectingRuntimeTypes());
        }

        [Test]
        public void Convert_GeneralClassWithVariables_ConvertsAsExpected()
        {
            //Arrange
            var expectedConfigObjects = new List<ConfigObject>
            {
                new GeneralClass
                {
                    Content = new List<ConfigObject>
                    {
                        new ConfigVariable {Name = "items", Value = 1},
                        new ConfigVariable {Name = "something", Value = "some value"},
                    }
                }
            };
            var inputTokens = new List<Token>
            {
                new ClassOpeningToken("ProfileVariables"),
                new VariableToken("items", 1),
                new VariableToken("something", "some value"),
                new ClosingToken()
            };

            //Act
            var result = _sut.Convert(inputTokens);

            //Assert
            result.ShouldAllBeEquivalentTo(expectedConfigObjects,
                options =>
                    options.IncludingNestedObjects().IncludingFields().IncludingProperties().RespectingRuntimeTypes());
        }
    }
}