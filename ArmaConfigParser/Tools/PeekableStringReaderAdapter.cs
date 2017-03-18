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

        public int PeekBuffered()
        {
            int ReadCharInteger = _underlyingReader.Read();
            if (ReadCharInteger == -1)
                return -1;
            _bufferedCharsIntegers.Enqueue(ReadCharInteger);
            return ReadCharInteger;
        }

        public int PeekUnBuffered()
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

        public string PeekBufferedUntilEndOfString()
        {
            StringBuilder fetchedString = new StringBuilder();
            int peekedValue = PeekBuffered();
            char peekedChar;
            while (peekedValue != -1)
            {
                peekedChar = (char)peekedValue;
                if ('"' == peekedChar)
                {
                    int forwardPeekedValue = PeekUnBuffered();
                    if (forwardPeekedValue != -1 && (char)forwardPeekedValue == '"')
                    {
                        PeekBuffered();
                        fetchedString.Append('"');
                    }
                    else
                    {
                        break;
                    }
                }
                fetchedString.Append(peekedChar);
                peekedValue = PeekBuffered();
            }
            return fetchedString.ToString();
        }

        public string PeekBufferedUntil(char breakCharacter)
        {
            StringBuilder fetchedString = new StringBuilder();
            int peekedValue = PeekBuffered();
            char peekedChar;
            while(peekedValue != -1)
            {
                peekedChar = (char)peekedValue;
                if (breakCharacter == peekedChar)
                {
                    break;
                }
                fetchedString.Append(peekedChar);
                peekedValue = PeekBuffered();
            }
            return fetchedString.ToString();
        }

        public string PeekBufferedUntil(char[] breakCharacterArray)
        {
            StringBuilder fetchedString = new StringBuilder();
            int peekedValue = PeekBuffered();
            char peekedChar;
            while (peekedValue != -1)
            {
                peekedChar = (char)peekedValue;
                if (peekedChar == '"')
                {
                     fetchedString.Append(PeekBufferedUntilEndOfString());
                }
                else if (breakCharacterArray.Contains(peekedChar))
                {
                    break;
                }
                else
                {
                    fetchedString.Append(peekedChar);
                }
                peekedValue = PeekBuffered();
            }
            return fetchedString.ToString();
        }

    }
}
