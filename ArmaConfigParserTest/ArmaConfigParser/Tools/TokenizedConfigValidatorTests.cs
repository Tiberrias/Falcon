using System.Collections.Generic;
using ArmaConfigParser.Tokens.Model;
using ArmaConfigParser.Tools;
using NUnit.Framework;

namespace Falcon.Tests.ArmaConfigParser.Tools
{
    [TestFixture]
    public class TokenizedConfigValidatorTests
    {
        private TokenizedConfigValidator _tokenizedConfigValidator;

        [SetUp]
        public void SetUp()
        {
            _tokenizedConfigValidator = new TokenizedConfigValidator();
        }

        [Test]
        public void Validate_ProperConfig_IsValid()
        {
            List<Token> tokenizedConfig = new List<Token>()
            {
                new ClassOpeningToken("data"),
                new ClassOpeningToken("type"),
                new TypeOpeningToken(),
                new StandaloneStringToken("String"),
                new ClosingToken(),
                new ClosingToken(),
                new VariableToken("value","SomeValue"),
                new ClosingToken()
            };

            var result = _tokenizedConfigValidator.Validate(tokenizedConfig);

            Assert.IsTrue(result);
        }

        [Test]
        public void Validate_MixedEnclosings_NotValid()
        {
            List<Token> tokenizedConfig = new List<Token>()
            {
                new ClassOpeningToken("data"),
                new ClosingToken(),
                new ClosingToken(),
                new ClassOpeningToken("type"),
                new TypeOpeningToken(),
                new StandaloneStringToken("String"),
                new VariableToken("value","SomeValue"),
                new ClosingToken()
            };

            var result = _tokenizedConfigValidator.Validate(tokenizedConfig);

            Assert.IsFalse(result);
        }

        [Test]
        public void Validate_MissingEnclosings_NotValid()
        {
            List<Token> tokenizedConfig = new List<Token>()
            {
                new ClassOpeningToken("data"),
                new ClassOpeningToken("type"),
                new TypeOpeningToken(),
                new StandaloneStringToken("String"),
                new ClosingToken(),
                new ClosingToken(),
                new VariableToken("value","SomeValue")
            };

            var result = _tokenizedConfigValidator.Validate(tokenizedConfig);

            Assert.IsFalse(result);
        }


    }
}
