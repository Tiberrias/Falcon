﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ArmaConfigParser.ConfigReader;
using ArmaConfigParser.Modules;
using ArmaConfigParser.Tokens;
using ArmaConfigParser.Tokens.Model;
using ArmaConfigParser.Tools;
using Ninject;

namespace ExtractorTest
{
    class Program
    {
        static void Main(string[] args)
        {
            StandardKernel kernel = new StandardKernel(new ArmaConfigParserNinjectModule());

            string testPath = @"C:\Users\Tiberrias\Documents\A3\Tiberrias.vars.Arma3Profile.cpp";
            string sourcePath = @"C:\Users\Tiberrias\Documents\A3\ConfigParsing\Tiberrias.vars.Arma3Profile";
            string destPath = @"C:\Users\Tiberrias\Documents\A3\ConfigParsing\Tiberrias.vars.Arma3Profile.cpp";
            string toolsPath = @"C:\Gry\Steam\steamapps\common\Arma 3 Tools\CfgConvert\CfgConvert.exe";
            //string testPath = @"C:\Users\Tiberrias\Documents\A3\scrap.cpp";

            IConfigDebinarizer debinarizer = kernel.Get<IConfigDebinarizer>();
            debinarizer.Initialize(toolsPath);
            debinarizer.DebinarizeConfigFile(sourcePath, destPath);

            string fucker = File.ReadAllText(destPath);

            Tokenizer tokenizer = new Tokenizer(fucker);
            List<Token> tokens = tokenizer.Tokenize().ToList();
            ITokenizedConfigValidator validator = kernel.Get<ITokenizedConfigValidator>();
            bool result = validator.Validate(tokens);

            Console.ReadKey();

        }
    }
}
