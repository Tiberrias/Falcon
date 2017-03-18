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
    public class TokenTest
    {
        [Test]
        public void ClassOpeningToken_HasClassname()
        {
            ClassOpeningToken cot = new ClassOpeningToken("data");

            Assert.AreEqual(cot.ClassName, "data");
        }

        [Test]
        public void StandaloneStringToken_HasProperValue()
        {
            StandaloneStringToken sst = new StandaloneStringToken("test value");

            Assert.IsInstanceOf(Type.GetType("System.String"), sst.Value);
            Assert.AreEqual(sst.Value, "test value");
        }

        [Test]
        public void VariableToken_HasProperValues()
        {
            VariableToken vt = new VariableToken("IntVariable", 12);

            Assert.IsInstanceOf(Type.GetType("System.Int32"), vt.Value);
            Assert.AreEqual(12, vt.Value);
            Assert.AreEqual("IntVariable", vt.VariableName);
        }

    }
}
