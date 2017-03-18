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
        public void ReadPeekRead_ReturnsProperCharacters()
        {
            //Arrange
            string testText = "abcdefg";
            _stringReader = new StringReader(testText);
            _peekableStringReaderAdapter = new PeekableStringReaderAdapter(_stringReader);
            
            //Act/Assert
            var readChar = (char)_peekableStringReaderAdapter.Read();
            Assert.AreEqual('a', readChar);
            var peekedChar = (char)_peekableStringReaderAdapter.Peek();
            Assert.AreEqual(peekedChar,(char)_peekableStringReaderAdapter.Peek());
            readChar = (char)_peekableStringReaderAdapter.Read();
            Assert.AreEqual(peekedChar, readChar);
        }

        [Test]
        public void PeekWithBuffering_PeeksProperly()
        {
            //Arrange
            string testText = "abcdefg";
            _stringReader = new StringReader(testText);
            _peekableStringReaderAdapter = new PeekableStringReaderAdapter(_stringReader);

            //Act/Assert
            var peekedChar = (char)_peekableStringReaderAdapter.PeekWithBuffering();
            Assert.AreEqual('a', peekedChar);
            peekedChar = (char)_peekableStringReaderAdapter.PeekWithBuffering();
            Assert.AreEqual('b', peekedChar);
            peekedChar = (char)_peekableStringReaderAdapter.PeekWithBuffering();
            Assert.AreEqual('c', peekedChar);
            peekedChar = (char)_peekableStringReaderAdapter.Peek();
            Assert.AreEqual('a', peekedChar);
            var readChar = (char)_peekableStringReaderAdapter.Read();
            Assert.AreEqual(peekedChar, readChar);
        }

        [TestCase("abcdefghijkl{asda",'{',ExpectedResult="abcdefghijkl")]
        [TestCase("asd", '{', ExpectedResult = "asd")]
        public string PeekWithBufferingUntil_PeeksAllCharactersUntilGivenWithoutConsuming(string text, char breakingCharacter)
        {
            //Arrange
            _stringReader = new StringReader(text);
            _peekableStringReaderAdapter = new PeekableStringReaderAdapter(_stringReader);

            //Act
            var peekedCharacters = _peekableStringReaderAdapter.PeekWithBufferingUntil(breakingCharacter);

            //Assert
            Assert.AreEqual(peekedCharacters[0], (char)_peekableStringReaderAdapter.Peek());
            return peekedCharacters;
        }

        [TestCase("abcdefghijkl{xxxxasda", new[]{'x','{'}, ExpectedResult = "abcdefghijkl")]
        [TestCase("asd", new[] {'{'}, ExpectedResult = "asd")]
        public string PeekWithBufferingUntil_PeeksAllCharactersUntilAnyGivenCharWithoutConsuming(string text, char[] breakingCharactersArray)
        {
            //Arrange
            _stringReader = new StringReader(text);
            _peekableStringReaderAdapter = new PeekableStringReaderAdapter(_stringReader);

            //Act
            var peekedCharacters = _peekableStringReaderAdapter.PeekWithBufferingUntil(breakingCharactersArray);

            //Assert
            Assert.AreEqual(peekedCharacters[0], (char)_peekableStringReaderAdapter.Peek());
            return peekedCharacters;
        }
        
        [Test]
        public void ConsumeBuffer_ClearsBuffer()
        {
            // Arrange
            string testText = "#consumed part/not consumed part";
            _stringReader = new StringReader(testText);
            _peekableStringReaderAdapter = new PeekableStringReaderAdapter(_stringReader);

            var peekedPart = _peekableStringReaderAdapter.PeekWithBufferingUntil('/');
            Assert.AreEqual(peekedPart[0], _peekableStringReaderAdapter.Peek());

            //Act
            _peekableStringReaderAdapter.ConsumeBuffer();

            //Assert
            Assert.AreNotEqual(peekedPart[0], _peekableStringReaderAdapter.Peek());
        }
    }
}
