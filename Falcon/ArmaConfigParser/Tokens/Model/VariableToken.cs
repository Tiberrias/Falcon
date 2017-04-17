namespace ArmaConfigParser.Tokens.Model
{
    public class VariableToken : DataToken
    {
        private string _variableName;

        public string VariableName => _variableName;

        public VariableToken(string variableName, object value) :
            base(value)
        {
            _variableName = variableName;
        }
    }
}