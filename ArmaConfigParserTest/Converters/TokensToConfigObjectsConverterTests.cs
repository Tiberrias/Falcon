using System.Collections.Generic;
using ArmaConfigParser.ConfigModel;
using ArmaConfigParser.Converters;
using ArmaConfigParser.Tokens.Model;
using FluentAssertions;
using NUnit.Framework;

namespace ArmaConfigParserTest.Converters
{
    [TestFixture]
    public class TokensToConfigObjectsConverterTests
    {
        private TokensToConfigObjectsConverter _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new TokensToConfigObjectsConverter();
        }

        [Test]
        public void Convert_LooseVariables_ConvertsAsExpected()
        {
            //Arrange
            var expectedConfigObjects = new List<ConfigObject>
            {
                new ConfigVariable {Name = "version", Value = 2},
                new ConfigVariable {Name = "something", Value = "some value"},
            };
            var inputTokens = new List<Token>
            {
                new VariableToken("version", 2),
                new VariableToken("something", "some value")
            };

            //Act
            var result = _sut.Convert(inputTokens);

            //Assert
            result.ShouldAllBeEquivalentTo(expectedConfigObjects,
                options =>
                    options.IncludingNestedObjects().IncludingFields().IncludingProperties().RespectingRuntimeTypes());
        }

        [Test]
        public void Convert_GeneralClassWithVariables_ConvertsAsExpected()
        {
            //Arrange
            var expectedConfigObjects = new List<ConfigObject>
            {
                new GeneralClass
                {
                    ClassName = "ProfileVariables",
                    Content = new List<ConfigObject>
                    {
                        new ConfigVariable {Name = "items", Value = 1},
                        new ConfigVariable {Name = "something", Value = "some value"},
                    }
                }
            };
            var inputTokens = new List<Token>
            {
                new ClassOpeningToken("ProfileVariables"),
                new VariableToken("items", 1),
                new VariableToken("something", "some value"),
                new ClosingToken()
            };

            //Act
            var result = _sut.Convert(inputTokens);

            //Assert
            result.ShouldAllBeEquivalentTo(expectedConfigObjects,
                options =>
                    options.IncludingNestedObjects().IncludingFields().IncludingProperties().RespectingRuntimeTypes());
        }

        [Test]
        public void Convert_GeneralClassWithItemClass_ConvertsAsExpected()
        {
            //Arrange
            var expectedConfigObjects = new List<ConfigObject>
            {
                new GeneralClass
                {
                    ClassName = "ProfileVariables",
                    Content = new List<ConfigObject>
                    {
                        new ItemClass()
                        {
                            Name = "Some_config_part",
                            Data = new DataClass
                            {
                                DataType = ConfigDataType.Scalar,
                                Value = 1.1
                            },
                            ReadOnly = false
                        }
                    }
                }
            };
            var inputTokens = new List<Token>
            {
                new ClassOpeningToken("ProfileVariables"),
                new ClassOpeningToken("Item0"),
                new VariableToken("name", "Some_config_part"),
                new ClassOpeningToken("data"),
                new ClassOpeningToken("type"),
                new TypeOpeningToken(),
                new StandaloneStringToken("SCALAR"),
                new ClosingToken(),
                new ClosingToken(),
                new VariableToken("value", 1.1),
                new ClosingToken(),
                new VariableToken("readOnly", 0),
                new ClosingToken(),
                new ClosingToken()
            };

            //Act
            var result = _sut.Convert(inputTokens);

            //Assert
            result.ShouldAllBeEquivalentTo(expectedConfigObjects,
                options =>
                    options.IncludingNestedObjects().RespectingRuntimeTypes());
        }

        [Test]
        public void Convert_GeneralClassWithItemClassWithArrayOfItems_ConvertsAsExpected()
        {
            //Arrange
            var expectedConfigObjects = new List<ConfigObject>
            {
                new GeneralClass
                {
                    ClassName = "ProfileVariables",
                    Content = new List<ConfigObject>
                    {
                        new ItemClass
                        {
                            Name = "Some_config_part",
                            Data = new DataClass
                            {
                                DataType = ConfigDataType.Array,
                                Value = new List<ConfigObject>
                                {
                                    new ConfigVariable
                                    {
                                        Name = "items",
                                        Value = 3
                                    },
                                    new ItemClass()
                                    {
                                        Data = new DataClass
                                        {
                                            DataType = ConfigDataType.Scalar,
                                            Value = 1.1
                                        }
                                    },
                                    new ItemClass()
                                    {
                                        Data = new DataClass
                                        {
                                            DataType = ConfigDataType.Bool,
                                            Value = 1
                                        }
                                    },
                                    new ItemClass()
                                    {
                                        Data = new DataClass
                                        {
                                            DataType = ConfigDataType.String,
                                            Value = "SomeValue"
                                        }
                                    }
                                }
                            },
                            ReadOnly = true
                        }
                    }
                }
            };
            var inputTokens = new List<Token>
            {
                new ClassOpeningToken("ProfileVariables"),
                new ClassOpeningToken("Item0"),
                new VariableToken("name", "Some_config_part"),
                new ClassOpeningToken("data"),
                new ClassOpeningToken("type"),
                new TypeOpeningToken(),
                new StandaloneStringToken("ARRAY"),
                new ClosingToken(),
                new ClosingToken(),
                new ClassOpeningToken("value"),

                new VariableToken("items", 3),

                new ClassOpeningToken("Item0"),
                new ClassOpeningToken("data"),
                new ClassOpeningToken("type"),
                new TypeOpeningToken(),
                new StandaloneStringToken("SCALAR"),
                new ClosingToken(),
                new ClosingToken(),
                new VariableToken("value", 1.1),
                new ClosingToken(),
                new ClosingToken(),

                new ClassOpeningToken("Item1"),
                new ClassOpeningToken("data"),
                new ClassOpeningToken("type"),
                new TypeOpeningToken(),
                new StandaloneStringToken("BOOL"),
                new ClosingToken(),
                new ClosingToken(),
                new VariableToken("value", 1),
                new ClosingToken(),
                new ClosingToken(),

                new ClassOpeningToken("Item2"),
                new ClassOpeningToken("data"),
                new ClassOpeningToken("type"),
                new TypeOpeningToken(),
                new StandaloneStringToken("STRING"),
                new ClosingToken(),
                new ClosingToken(),
                new VariableToken("value", "SomeValue"),
                new ClosingToken(),
                new ClosingToken(),

                new ClosingToken(),
                new ClosingToken(),
                new VariableToken("readOnly", 1),
                new ClosingToken(),
                new ClosingToken()
            };

            //Act
            var result = _sut.Convert(inputTokens);

            //Assert
            result.ShouldAllBeEquivalentTo(expectedConfigObjects,
                options =>
                    options.IncludingNestedObjects().RespectingRuntimeTypes());
        }
    }
}