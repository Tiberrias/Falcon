using System;
using System.Collections.Generic;
using ArmaConfigParser.ConfigModel;
using Falcon.Core.Services;
using FluentAssertions;
using NUnit.Framework;

namespace Falcon.Tests.Falcon.Core.Services
{
    [TestFixture]
    public class ArsenalEquipmentExtractionServiceTests
    {
        private ArsenalEquipmentExtractionService _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new ArsenalEquipmentExtractionService();
        }

        [Test]
        public void GetInventoryData_EmptyProfileVars_ThrowsException()
        {
            //Act/Assert
            Assert.Throws<ArgumentException>(() => _sut.GetInventoryData(null));
        }

        [Test]
        public void GetInventoryData_NoProfileVariablesInInput_ThrowsException()
        {
            //Arrange
            var config = new List<ConfigObject>
            {
                new ConfigVariable() {Name = "Variable", Value = "Value"},
                new ConfigVariable() {Name = "Variable2", Value = 1}
            };

            //Act/Assert
            Assert.Throws<ArgumentException>(() => _sut.GetInventoryData(config));
        }

        [Test]
        public void GetInventoryData_NoInventoryDataInInput_ThrowsException()
        {
            //Arrange
            var config = new List<ConfigObject>
            {
                new ConfigVariable() {Name = "version", Value = 2},
                new GeneralClass()
                {
                    ClassName = "ProfileVariables",
                    Content = new List<ConfigObject>
                    {
                     new ItemClass() {Name = "bis_cool_variable"},
                     new ItemClass() {Name = "bis_less_cool_variable"}
                    }
                }
            };

            //Act/Assert
            Assert.Throws<ArgumentException>(() => _sut.GetInventoryData(config));
        }

        [Test]
        public void GetInventoryData_InventoryDataInInput_ReturnsProperDataClass()
        {
            //Arrange
            var expectedData = new DataClass() {DataType = ConfigDataType.Array, Value = new List<ConfigObject>()};

            var config = new List<ConfigObject>
            {
                new ConfigVariable() {Name = "version", Value = 2},
                new GeneralClass()
                {
                    ClassName = "ProfileVariables",
                    Content = new List<ConfigObject>
                    {
                     new ItemClass() {Name = "bis_cool_variable"},
                     new ItemClass()
                     {
                         Name = "bis_fnc_saveinventory_data",
                         Data = expectedData
                     }
                    }
                }
            };

            //Act
            var result = _sut.GetInventoryData(config);

            //Assert
            result.Should().Be(expectedData);
        }

    }
}