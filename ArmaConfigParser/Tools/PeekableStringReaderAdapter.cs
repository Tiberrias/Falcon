using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ArmaConfigParser.Tools
{
    public class PeekableStringReaderAdapter
    {
        //stackoverflow.com/questions/842465/reading-a-line-from-a-streamreader-without-consuming

        private readonly StringReader _underlyingReader;
        private readonly Queue<int> _bufferedCharsIntegers;

        public PeekableStringReaderAdapter(StringReader underlying)
        {
            _underlyingReader = underlying;
            _bufferedCharsIntegers = new Queue<int>();
        }

        public int PeekWithBuffering()
        {
            int readCharInteger = _underlyingReader.Read();
            if (readCharInteger == -1)
                return -1;
            _bufferedCharsIntegers.Enqueue(readCharInteger);
            return readCharInteger;
        }

        public int PeekBufferTip()
        {
            return _underlyingReader.Peek();
        }

        public int Peek()
        {
            if (_bufferedCharsIntegers.Count > 0)
                return _bufferedCharsIntegers.Peek();
            return _underlyingReader.Peek();
        }
        
        public int Read()
        {
            if (_bufferedCharsIntegers.Count > 0)
                return _bufferedCharsIntegers.Dequeue();
            return _underlyingReader.Read();
        }

        public void ConsumeBuffer()
        {
            _bufferedCharsIntegers.Clear();
        }

        public string PeekWithBufferingUntilEndOfString()
        {
            StringBuilder fetchedString = new StringBuilder();
            int peekedValue = PeekWithBuffering();
            while (peekedValue != -1)
            {
                var peekedChar = (char)peekedValue;
                if ('"' == peekedChar)
                {
                    int forwardPeekedValue = PeekBufferTip();
                    if (forwardPeekedValue != -1 && (char)forwardPeekedValue == '"')
                    {
                        PeekWithBuffering();
                        fetchedString.Append('"');
                    }
                    else
                    {
                        break;
                    }
                }
                fetchedString.Append(peekedChar);
                peekedValue = PeekWithBuffering();
            }
            return fetchedString.ToString();
        }

        public string PeekWithBufferingUntil(char breakCharacter)
        {
            StringBuilder fetchedString = new StringBuilder();
            int peekedValue = PeekWithBuffering();
            char peekedChar;
            while(peekedValue != -1)
            {
                peekedChar = (char)peekedValue;
                if (breakCharacter == peekedChar)
                {
                    break;
                }
                fetchedString.Append(peekedChar);
                peekedValue = PeekWithBuffering();
            }
            return fetchedString.ToString();
        }

        public string PeekWithBufferingUntil(char[] breakCharacterArray)
        {
            StringBuilder fetchedString = new StringBuilder();
            int peekedValue = PeekWithBuffering();
            char peekedChar;
            while (peekedValue != -1)
            {
                peekedChar = (char)peekedValue;
                if (peekedChar == '"')
                {
                     fetchedString.Append(PeekWithBufferingUntilEndOfString());
                }
                else if (breakCharacterArray.Contains(peekedChar))
                {
                    break;
                }
                else
                {
                    fetchedString.Append(peekedChar);
                }
                peekedValue = PeekWithBuffering();
            }
            return fetchedString.ToString();
        }

    }
}
