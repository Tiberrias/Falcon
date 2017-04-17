namespace ArmaConfigParser.Tokens.Model
{
    public abstract class DataToken : Token
    {
        private readonly object _value;

        public object Value => _value;

        public DataToken(object value)
        {
            _value = value;
        }
    }
}