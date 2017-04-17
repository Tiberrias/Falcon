using System;
using ArmaConfigParser.ConfigModel;
using ArmaConfigParser.Tokens.Model;

namespace ArmaConfigParser.Helpers
{
    public static class ConfigDataTypeHelper
    {
        public static ConfigDataType GetType(StandaloneStringToken typeDefiningToken)
        {
            if (!(typeDefiningToken.Value is string))
            {
                throw new ArgumentException(@"Type defining token must be a string", nameof(typeDefiningToken));
            }

            var typeString = typeDefiningToken.Value as string;

            switch (typeString)
            {
                case "BOOL":
                    return ConfigDataType.Bool;
                case "SCALAR":
                    return ConfigDataType.Scalar;
                case "ARRAY":
                    return ConfigDataType.Array;
                case "STRING":
                    return ConfigDataType.String;
                case "NOTHING":
                case "ANY":
                    return ConfigDataType.Nothing;
                default:
                    throw new ArgumentException(@"Unknown ConfigDataType", nameof(typeDefiningToken));
            }
        }
    }
}