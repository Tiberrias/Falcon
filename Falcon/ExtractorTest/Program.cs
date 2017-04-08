using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ArmaConfigParser.ConfigReader.Interfaces;
using ArmaConfigParser.Modules;
using ArmaConfigParser.Tokens;
using ArmaConfigParser.Tokens.Model;
using ArmaConfigParser.Tools.Interfaces;
using Ninject;

namespace ExtractorTest
{
    class Program
    {
        static void Main(string[] args)
        {
            StandardKernel kernel = new StandardKernel(new ArmaConfigParserNinjectModule());
            
            string sourcePath = @"C:\Users\Tiberrias\Documents\A3\ConfigParsing\Tiberrias.vars.Arma3Profile";
            string destPath = @"C:\Users\Tiberrias\Documents\A3\ConfigParsing\Tiberrias.vars.Arma3Profile.cpp";
            string toolsPath = @"C:\Gry\Steam\steamapps\common\Arma 3 Tools\CfgConvert\CfgConvert.exe";

            IConfigDebinarizer debinarizer = kernel.Get<IConfigDebinarizer>();
            debinarizer.Initialize(toolsPath);
            debinarizer.DebinarizeConfigFile(sourcePath, destPath);

            string fucker = File.ReadAllText(destPath);

            Tokenizer tokenizer = new Tokenizer();
            tokenizer.Initialize(fucker);
            List<Token> tokens = tokenizer.Tokenize().ToList();
            ITokenizedConfigValidator validator = kernel.Get<ITokenizedConfigValidator>();
            bool result = validator.Validate(tokens);

            Console.ReadKey();

        }
    }
}
