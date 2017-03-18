using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmaConfigParser.Tokens
{
    public abstract class Token
    {
    }

    public abstract class EnclosingToken : Token
    {
    }

    public abstract class OpeningToken : EnclosingToken
    {
    }

    public class ClassOpeningToken : OpeningToken
    {
        private readonly string _className;
        public string ClassName
        {
            get { return _className; }
        }
        public ClassOpeningToken(string classname)
        {
            _className = classname;
        }
    }

    public class TypeOpeningToken : OpeningToken
    {
    }

    public class ClosingToken : EnclosingToken
    {
    }

    public abstract class DataToken : Token
    {
        private object _value;
        public object Value
        {
            get { return _value; }
        }
        public DataToken(object value)
        {
            _value = value;
        }
    }

    public class StandaloneStringToken : DataToken
    {
        public StandaloneStringToken(string value) :
            base(value)
        {
        }
    }

    public class VariableToken : DataToken
    {
        private string _variableName;
        public string VariableName
        {
            get { return _variableName; }
        }

        public VariableToken(string variableName, object value) :
            base(value)
        {
            _variableName = variableName;
        }
    }

}
