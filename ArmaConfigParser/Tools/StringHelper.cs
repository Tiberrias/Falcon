using System;
using System.IO;

namespace ArmaConfigParser.Tools
{
    
    public class StringHelper
    {

        public static bool HasBalancedCurlyBrackets(StringReader expression)
        {
            if (expression == null)
                return false;
            int openingBracketsCount = 0;
            int closingBracketsCount = 0;
            while (expression.Peek() != -1)
            {
                char c = (char)expression.Peek();
                if (c == '{')
                    openingBracketsCount++;
                else if (c == '}')
                    closingBracketsCount++;

                if (closingBracketsCount > openingBracketsCount)
                    return false;
                expression.Read();
            }
            if (openingBracketsCount == closingBracketsCount)
                return true;
            return false;
        }

        public static bool HasBalancedCurlyBrackets(string expression)
        {
            if (expression == null)
                return false;
            StringReader reader = new StringReader(expression);
            return HasBalancedCurlyBrackets(reader);
        }

        public static bool ContainsAtTheBeginning(string expression, string stringWithin)
        {
            if (String.IsNullOrEmpty(expression) || String.IsNullOrEmpty(stringWithin))
                return false;
            if (expression.Length < stringWithin.Length)
                return false;
            for (int i = 0; i < stringWithin.Length; i++)
            {
                if (expression[i] != stringWithin[i])
                    return false;
            }
            return true;
        }

    }
}
