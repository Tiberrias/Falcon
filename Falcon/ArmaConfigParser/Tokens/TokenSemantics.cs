namespace ArmaConfigParser.Tokens
{
    public static class TokenSemantics
    {
        public static string ClassOpeningTokenStringDefinition = "class ";
        public static string TypeOpeningTokenStringDefinition = "type[]=";

        public static char[] ClassnameOrVariableSearchDelimiters = { '{', ';' };
    }
}
