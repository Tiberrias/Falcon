using NUnit.Framework;
using System.IO;
using ArmaConfigParser.Tools;

namespace ArmaConfigParserTest.Tools
{
    [TestFixture]
    class PeekableStringReaderAdapterTests
    {
        private PeekableStringReaderAdapter _peekableStringReaderAdapter;
        private StringReader _stringReader;
        
        [TearDown]
        public void TearDown()
        {
            _stringReader?.Dispose();
        }

        [Test]
        public void PeekableStreamReaderPeekingReading()
        {
            string testText = "abcdefg";
            _stringReader = new StringReader(testText);
            _peekableStringReaderAdapter = new PeekableStringReaderAdapter(_stringReader);


            char readChar, PeekedChar;
            readChar = (char)_peekableStringReaderAdapter.Read();
            Assert.AreEqual('a', readChar);
            PeekedChar = (char)_peekableStringReaderAdapter.Peek();
            Assert.AreEqual(PeekedChar,(char)_peekableStringReaderAdapter.Peek());
            readChar = (char)_peekableStringReaderAdapter.Read();
            Assert.AreEqual(PeekedChar, readChar);
        }

        [Test]
        public void PeekableStreamReaderBufferPeeking()
        {
            string testText = "abcdefg";
            _stringReader = new StringReader(testText);
            _peekableStringReaderAdapter = new PeekableStringReaderAdapter(_stringReader);

            char readChar, PeekedChar;
            PeekedChar = (char)_peekableStringReaderAdapter.PeekBuffered();
            Assert.AreEqual('a', PeekedChar);
            PeekedChar = (char)_peekableStringReaderAdapter.PeekBuffered();
            Assert.AreEqual('b', PeekedChar);
            PeekedChar = (char)_peekableStringReaderAdapter.PeekBuffered();
            Assert.AreEqual('c', PeekedChar);
            PeekedChar = (char)_peekableStringReaderAdapter.Peek();
            Assert.AreEqual('a', PeekedChar);
            readChar = (char)_peekableStringReaderAdapter.Read();
            Assert.AreEqual(PeekedChar, readChar);
        }

        [TestCase("abcdefghijkl{asda",'{',ExpectedResult="abcdefghijkl")]
        [TestCase("asd", '{', ExpectedResult = "asd")]
        public string PeekableStreamReaderPeekingUntilChar(string text, char breakingCharacter)
        {
            _stringReader = new StringReader(text);
            _peekableStringReaderAdapter = new PeekableStringReaderAdapter(_stringReader);

            return _peekableStringReaderAdapter.PeekBufferedUntil(breakingCharacter);
        }

        [TestCase("abcdefghijkl{xxxxasda", new[]{'x','{'}, ExpectedResult = "abcdefghijkl")]
        [TestCase("asd", new[] {'{'}, ExpectedResult = "asd")]
        public string PeekableStreamReaderPeekingUntilChar(string text, char[] breakingCharactersArray)
        {
            _stringReader = new StringReader(text);
            _peekableStringReaderAdapter = new PeekableStringReaderAdapter(_stringReader);

            return _peekableStringReaderAdapter.PeekBufferedUntil(breakingCharactersArray);
        }

    }
}
