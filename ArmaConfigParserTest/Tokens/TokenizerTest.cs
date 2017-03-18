using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ArmaConfigParser;
using ArmaConfigParser.Tokens;


namespace ArmaConfigParserTest.Tokens
{
    [TestFixture]
    public class TokenizerTest
    {
        [TestCase("", ExpectedResult = 0)]
        [TestCase("class item { };", ExpectedResult = 2)]
        [TestCase(@"type[]= {  ""STRING"" };", ExpectedResult = 3)]
        [TestCase(@"   ""STRING"" ", ExpectedResult = 1)]
        public int Tokenize_SmallDatas_ProperNumberOfTokenizedTokens(string text)
        {
            Tokenizer tokenizer = new Tokenizer(text);

            return (tokenizer.Tokenize() as List<Token>).Count;
        }

        [Test]
        public void Tokenize_InvalidData_ThrowsException()
        {
            Tokenizer tokenizer = new Tokenizer("}");
            
            Assert.Throws<ArgumentException>(() => tokenizer.Tokenize());
        }

        [Test]
        public void Tokenize_ClassItemString_TokenizesAsSpecificTokens()
        {
            Tokenizer tokenizer = new Tokenizer(@"class item { ""STRING""    };");
            
            List<Token> resultTokens = tokenizer.Tokenize().ToList();
            
            Assert.AreEqual(3, resultTokens.Count);
            Assert.IsInstanceOf(typeof(ClassOpeningToken), resultTokens[0]);
            Assert.AreEqual("item", (resultTokens[0] as ClassOpeningToken).ClassName);
            Assert.IsInstanceOf(typeof(StandaloneStringToken), resultTokens[1]);
            Assert.AreEqual("STRING", (resultTokens[1] as StandaloneStringToken).Value);
            Assert.IsInstanceOf(typeof(ClosingToken), resultTokens[2]);
        }

        [Test]
        public void Tokenize_ClassItem_TokenizesAsSpecificTokens()
        {
            Tokenizer tokenizer = new Tokenizer("class item { };");
            
            List<Token> resultTokens = tokenizer.Tokenize().ToList();
            
            Assert.AreEqual(2, resultTokens.Count);
            Assert.IsInstanceOf(typeof(ClassOpeningToken), resultTokens[0]);
            Assert.AreEqual("item", (resultTokens[0] as ClassOpeningToken).ClassName);
            Assert.IsInstanceOf(typeof(ClosingToken), resultTokens[1]);
        }

        [Test]
        public void Tokenize_SingleStringVariable_TokenizedProperly()
        {
            Tokenizer tokenizer = new Tokenizer(@"value=""[""""Open"""",true] spawn BIS_fnc_arsenal;"";");

            List<Token> resultTokens = tokenizer.Tokenize().ToList();
            
            Assert.AreEqual(1, resultTokens.Count);
            Assert.IsInstanceOf(typeof(VariableToken), resultTokens[0]);
            Assert.AreEqual("value", (resultTokens[0] as VariableToken).VariableName);
            Assert.AreEqual(@"[""""Open"""",true] spawn BIS_fnc_arsenal;", (resultTokens[0] as VariableToken).Value);
        }

        [Test]
        public void Tokenize_LargeExample_TokenizedProperly()
        {
            Tokenizer tokenizer = new Tokenizer(ArmaConfigParserTest.Properties.Resources.ParsingExampleLargeCase);
            
            List<Token> tokens = tokenizer.Tokenize().ToList();
            
            Assert.AreEqual(432, tokens.Count);
            Assert.IsInstanceOf(typeof(ClassOpeningToken), tokens[0]);
            Assert.IsInstanceOf(typeof(VariableToken), tokens[1]);
            Assert.IsInstanceOf(typeof(ClassOpeningToken), tokens[2]);
            Assert.IsInstanceOf(typeof(ClassOpeningToken), tokens[3]);
            Assert.IsInstanceOf(typeof(ClassOpeningToken), tokens[4]);
            Assert.IsInstanceOf(typeof(TypeOpeningToken), tokens[5]);
            Assert.IsInstanceOf(typeof(ClosingToken), tokens[tokens.Count - 1]);
        }
    }
}
