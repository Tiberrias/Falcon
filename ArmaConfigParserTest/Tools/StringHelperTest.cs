using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;
using System.Resources;
using ArmaConfigParser;
using ArmaConfigParser.Tools;

namespace ArmaConfigParserTest.Tools
{
    [TestFixture]
    public class StringHelperTest
    {
        
        [SetUp]
        public void TestInit()
        {

        }


        [TestCase("",ExpectedResult=true)]
        [TestCase("{}", ExpectedResult = true)]
        [TestCase("}{", ExpectedResult = false)]
        [TestCase("{{}}{}", ExpectedResult = true)]
        [TestCase("asdasd1{1231{asdasda}{}{}{}{}{sd}sadsd}123", ExpectedResult = true)]
        [TestCase("{213{}", ExpectedResult = false)]
        [TestCase(null,ExpectedResult=false)]
        public bool HasBalancedCurlyBrackets_SmallExamples_ValidOutcome(string expression)
        {
            return StringHelper.HasBalancedCurlyBrackets(expression);
        }

        [Test]
        public void HasBalancedCurlyBrackets_LargeExample_ValidOutcome()
        {
            StringReader testReader = new StringReader(ArmaConfigParserTest.Properties.Resources.ParsingExampleLargeCase);

            bool result = StringHelper.HasBalancedCurlyBrackets(testReader);

            Assert.AreEqual(true, result);
        }

        [TestCase("cookie", "cookie", ExpectedResult = true)]
        [TestCase("not a cookie", "cookie", ExpectedResult = false)]
        [TestCase("cook", "cookie", ExpectedResult = false)]
        [TestCase("cookie dasdad qwe qwe qwe qwe qwe qzxc", "cookie", ExpectedResult = true)]
        public bool ContainsAtTheBeginning_ValidOutcome(string expression, string stringWithin)
        {
            return StringHelper.ContainsAtTheBeginning(expression, stringWithin);
        }
        
    }
}
