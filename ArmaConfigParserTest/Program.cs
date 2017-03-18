using System;
using System.Collections.Generic;
using System.Linq;
using ArmaConfigParser.Tokens;
using System.IO;
using ArmaConfigParser.Tokens.Model;
using ArmaConfigParser.Tools;

namespace ArmaConfigParserTest
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string testPath = @"C:\Users\Tiberrias\Documents\A3\Tiberrias.vars.Arma3Profile.cpp";
            //string testPath = @"C:\Users\Tiberrias\Documents\A3\scrap.cpp";
            string fucker = File.ReadAllText(testPath);

            Tokenizer tokenizer = new Tokenizer(fucker);
            List<Token> tokens = tokenizer.Tokenize().ToList();
            TokenizedConfigValidator validator = new TokenizedConfigValidator();
            bool result = validator.Validate(tokens);
            
            Console.ReadKey();
        }
    }
}