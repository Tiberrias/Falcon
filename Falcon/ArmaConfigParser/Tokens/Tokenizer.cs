using ArmaConfigParser.Tools;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using ArmaConfigParser.Tokens.Model;

namespace ArmaConfigParser.Tokens
{
    public class Tokenizer : ITokenizer
    {
        private PeekableStringReaderAdapter _reader;
        private List<Token> _resultTokens;

        public void Initialize(string text)
        {
            StringReader underlyingReader = new StringReader(text);
            _reader = new PeekableStringReaderAdapter(underlyingReader);
        }

        public IEnumerable<Token> Tokenize()
        {
            _resultTokens = new List<Token>();

            while (_reader.Peek() != -1)
            {
                SkipWhitespaces();
                int peekValue = _reader.Peek();
                if (peekValue == -1)
                    break;
                char nextCharacter = (char)peekValue;

                string peekedString;
                switch (nextCharacter)
                {
                    case 'c':
                        peekedString = _reader.PeekWithBufferingUntil(TokenSemantics.ClassnameOrVariableSearchDelimiters);
                        _resultTokens.Add(ParseIfClassOpeningToken(peekedString));
                        break;
                    case '}':
                        _resultTokens.Add(ParseIfClosingToken());
                        break;
                    case 't':
                        peekedString = _reader.PeekWithBufferingUntil(TokenSemantics.ClassnameOrVariableSearchDelimiters);
                        _resultTokens.Add(ParseIfTypeOpeningToken(peekedString));
                        break;
                    case '"':
                        _resultTokens.Add(ParseIfStandaloneStringToken());
                        break;
                    default:
                        peekedString = _reader.PeekWithBufferingUntil(TokenSemantics.ClassnameOrVariableSearchDelimiters);
                        _resultTokens.Add(ParseIfVariableToken(peekedString));
                        break;
                }
                _reader.Read();
            }
            return _resultTokens;
        }

        private Token ParseIfVariableToken(string searchedArea)
        {
            if (!searchedArea.Contains('='))
                throw new ArgumentException("Invalid config syntax: invalid variable definition");
            int valueAssignmentPosition = searchedArea.IndexOf('=');
            string variableName = searchedArea.Remove(valueAssignmentPosition);
            string variableValue = searchedArea.Remove(0, valueAssignmentPosition + 1);
            if (variableName.Contains('"'))
                throw new ArgumentException("Invalid config syntax: invalid variable definition");
            object value = FetchVariableFromString(variableValue);
            _reader.ConsumeBuffer();
            return new VariableToken(variableName, value);
        }

        private object FetchVariableFromString(string variableValue)
        {
            if (variableValue == null)
                throw new ArgumentException("Invalid config syntax: null variable value");
            if (variableValue == String.Empty)
                return variableValue;
            if (variableValue[0] == '"')
            {
                return variableValue.Trim('"');
            }
            if (Int32.TryParse(variableValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var intVariable))
            {
                return intVariable;
            }
            if (Double.TryParse(variableValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var doubleVariable))
            {
                return doubleVariable;
            }
            return variableValue;
        }

        private Token ParseIfStandaloneStringToken()
        {
            _reader.Read();
            string peekedString = _reader.PeekWithBufferingUntilEndOfString();
            if (peekedString == null)
                throw new ArgumentException("Invalid config syntax");
            _reader.ConsumeBuffer();
            return new StandaloneStringToken(peekedString);
        }

        private Token ParseIfTypeOpeningToken(string searchedArea)
        {
            Token parsedToken;
            if (StringHelper.ContainsAtTheBeginning(searchedArea, TokenSemantics.TypeOpeningTokenStringDefinition))
            {
                _reader.ConsumeBuffer();
                parsedToken = new TypeOpeningToken();
            }
            else
            {
                parsedToken = ParseIfVariableToken(searchedArea);
            }
            return parsedToken;
        }

        private Token ParseIfClosingToken()
        {
            _reader.Read();
            int secondCharacterCode = _reader.Read();
            if (secondCharacterCode == -1)
                throw new ArgumentException("Invalid config syntax: unexpected end");
            if ((char)secondCharacterCode != ';')
                throw new ArgumentException("Invalid config syntax: missing semicolon");
            _reader.ConsumeBuffer();
            return new ClosingToken();
        }

        private Token ParseIfClassOpeningToken(string searchedArea)
        {
            Token parsedToken;
            if (StringHelper.ContainsAtTheBeginning(searchedArea, TokenSemantics.ClassOpeningTokenStringDefinition))
            {
                string className = searchedArea.Substring(TokenSemantics.ClassOpeningTokenStringDefinition.Length).Trim();
                if (String.IsNullOrWhiteSpace(className))
                    throw new ArgumentException("Invalid config syntax: empty classname");
                _reader.ConsumeBuffer();
                parsedToken = new ClassOpeningToken(className);
            }
            else
            {
                parsedToken = ParseIfVariableToken(searchedArea);
            }
            return parsedToken;
        }
        
        private void SkipWhitespaces()
        {
            while (Char.IsWhiteSpace((char)_reader.Peek()))
            {
                _reader.Read();
            }
        }

    }
}
