using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmaConfigParser.Tokens
{
    public static class TokenSemantics
    {
        public static string ClassOpeningTokenStringDefinition = "class ";
        public static string TypeOpeningTokenStringDefinition = "type[]=";

        public static char[] ClassnameOrVariableSearchDelimiter = new char[] { '{', ';' };
    }
}
