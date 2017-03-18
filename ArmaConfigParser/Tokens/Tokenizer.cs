using ArmaConfigParser.Tools;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ArmaConfigParser.Tokens
{
    public class Tokenizer
    {
        
        private PeekableStringReaderAdapter Reader;
        private string Text;
        private List<Token> _resultTokens;

        public Tokenizer(string text)
        {
            Text = text;
            StringReader underlyingReader = new StringReader(text);
            Reader = new PeekableStringReaderAdapter(underlyingReader);
        }

        public Tokenizer(StringReader stringReader)
        {
            Text = null;
            if (stringReader != null)
            {
                StringReader underlyingReader = stringReader;
                Reader = new PeekableStringReaderAdapter(underlyingReader);
            }
        }

        public bool TextIsValidForTokenize()
        {
            return StringHelper.HasBalancedCurlyBrackets(Text);
        }

        public IEnumerable<Token> Tokenize()
        {
            if (!TextIsValidForTokenize())
                throw new ArgumentException("Invalid config syntax");

            _resultTokens = new List<Token>();
            string PeekedString;

            while (Reader.Peek() != -1)
            {
                SkipWhitespaces();
                int peekValue = Reader.Peek();
                if (peekValue == -1)
                    break;
                char nextCharacter = (char)peekValue;

                switch (nextCharacter)
                {
                    case 'c':
                        PeekedString = Reader.PeekBufferedUntil(TokenSemantics.ClassnameOrVariableSearchDelimiter);
                        _resultTokens.Add(ParseIfClassOpeningToken(PeekedString));
                        break;
                    case '}':
                        _resultTokens.Add(ParseIfClosingToken());
                        break;
                    case 't':
                        PeekedString = Reader.PeekBufferedUntil(TokenSemantics.ClassnameOrVariableSearchDelimiter);
                        _resultTokens.Add(ParseIfTypeOpeningToken(PeekedString));
                        break;
                    case '"':
                        _resultTokens.Add(ParseIfStandaloneStringToken());
                        break;
                    default:
                        PeekedString = Reader.PeekBufferedUntil(TokenSemantics.ClassnameOrVariableSearchDelimiter);
                        _resultTokens.Add(ParseIfVariableToken(PeekedString));
                        break;
                }
                Reader.Read();
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
            if(variableName.Contains('"'))
                throw new ArgumentException("Invalid config syntax: invalid variable definition");
            object value = FetchVariableFromString(variableValue);
            Reader.ConsumeBuffer();
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
                int intVariable;
                if (Int32.TryParse(variableValue, NumberStyles.Any, CultureInfo.InvariantCulture, out intVariable))
                    return intVariable;
                double doubleVariable;
                if (Double.TryParse(variableValue, NumberStyles.Any, CultureInfo.InvariantCulture, out doubleVariable))
                    return doubleVariable;
                else
                    return variableValue;

        }

        private Token ParseIfStandaloneStringToken()
        {
            Reader.Read();
            string PeekedString = Reader.PeekBufferedUntilEndOfString();
            if(PeekedString == null)
                throw new ArgumentException("Invalid config syntax");
            Reader.ConsumeBuffer();
            return new StandaloneStringToken(PeekedString);
        }

        private Token ParseIfTypeOpeningToken(string searchedArea)
        {
            Token parsedToken;
            if (StringHelper.ContainsAtTheBeginning(searchedArea, TokenSemantics.TypeOpeningTokenStringDefinition))
            {
                Reader.ConsumeBuffer();
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
            char firstCharacter = (char)Reader.Read();
            int secondCharacterCode = Reader.Read();
            if(secondCharacterCode == -1)
                throw new ArgumentException("Invalid config syntax: unexpected end");
            if((char)secondCharacterCode != ';')
                throw new ArgumentException("Invalid config syntax: missing semicolon");
            Reader.ConsumeBuffer();
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
                Reader.ConsumeBuffer();
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
            while (Char.IsWhiteSpace((char)Reader.Peek()))
            {
                Reader.Read();
            }
        }
    }
}
