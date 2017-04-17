namespace ArmaConfigParser.Tokens.Model
{
    public class ClassOpeningToken : OpeningToken
    {
        private readonly string _className;

        public string ClassName => _className;

        public ClassOpeningToken(string classname)
        {
            _className = classname;
        }
    }
}