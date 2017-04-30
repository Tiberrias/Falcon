using System.Collections.Generic;
using ArmaConfigParser.ConfigModel;
using ArmaConfigParser.ConfigReader.Interfaces;
using Falcon.Core.Converters.Interfaces;
using Falcon.Core.Model;
using Falcon.Core.Services;
using Falcon.Core.Services.Interfaces;
using Moq;
using NUnit.Framework;

namespace Falcon.Tests.Falcon.Core.Services
{
    [TestFixture]
    public class VirtualArsenalLoadoutServiceTests
    {
        private VirtualArsenalLoadoutService _sut;

        private Mock<IConfigExtractionService> _configExtractionService;
        private Mock<IArsenalEquipmentExtractionService> _arsenalEquipmentExtractionService;
        private Mock<IDataClassToLoadoutListConverter> _dataClassToLoadoutListConverter;

        [SetUp]
        public void SetUp()
        {
            _configExtractionService = new Mock<IConfigExtractionService>();
            _arsenalEquipmentExtractionService = new Mock<IArsenalEquipmentExtractionService>();
            _dataClassToLoadoutListConverter =  new Mock<IDataClassToLoadoutListConverter>();

            _sut = new VirtualArsenalLoadoutService(
                _configExtractionService.Object,
                _arsenalEquipmentExtractionService.Object,
                _dataClassToLoadoutListConverter.Object);
        }

        [Test]
        public void ImportLoadouts_ReturnsConvertedLoadouts()
        {
            //Arrange
            var inputPath = "anyPath";

            var extractedConfigObjects = new List<ConfigObject>();
            _configExtractionService.Setup(service => service.Extract(inputPath))
                .Returns(extractedConfigObjects);

            var compiledDataClass = new DataClass();
            _arsenalEquipmentExtractionService.Setup(service => service.GetInventoryData(extractedConfigObjects))
                .Returns(compiledDataClass);

            var convertedLoadouts = new List<Loadout>();
            _dataClassToLoadoutListConverter.Setup(converter => converter.Convert(compiledDataClass))
                .Returns(convertedLoadouts);

            //Act
            var result = _sut.ImportLoadouts(inputPath);

            //Assert
            Assert.AreEqual(convertedLoadouts, result);
        }
    }
}