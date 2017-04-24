using System.IO;
using ArmaConfigParser.ConfigReader;
using ArmaConfigParser.Configuration;
using ArmaConfigParser.Modules;
using Falcon.Core.Converters;
using Falcon.Core.Modules;
using Falcon.Core.Services;
using Falcon.Tests.Configuration;
using Falcon.Tests.Properties;
using Falcon.Utilities.Modules;
using FluentAssertions;
using Ninject;
using NUnit.Framework;

namespace Falcon.Tests.Falcon.Core.Integration
{
    [TestFixture(Category = "Integration")]
    [Ignore("Integration tests ignored")]
    public class LoadoutExtractionTests
    {
        private ConfigExtractionService _configExtractionService;
        private ArsenalEquipmentExtractionService _arsenalEquipmentExtractionService;
        private DataClassToLoadoutListConverter _sut;
        private string _configFilePath;

        [SetUp]
        public void SetUp()
        {
            StandardKernel kernel = new StandardKernel(new ArmaConfigParserNinjectModule(), new FalconCoreNinjectModule(), new UtilitiesNinjectModule());
            kernel.Bind<IConfigurationService>().To<ConfigurationServiceTestImplementation>();

            _configExtractionService = kernel.Get<ConfigExtractionService>();
            _arsenalEquipmentExtractionService = kernel.Get<ArsenalEquipmentExtractionService>();
            _sut = kernel.Get<DataClassToLoadoutListConverter>();
        }
        
        [Test]
        public void ExtractGetConvert_BinarizedFileOnDisc_ReturnsListOfConvertedLoadouts()
        {
            //Arrange
            _configFilePath = Path.GetTempFileName();
            File.WriteAllBytes(_configFilePath, Resources.test_vars);

            //Act
            var configObjects = _configExtractionService.Extract(_configFilePath);
            var arsenalData = _arsenalEquipmentExtractionService.GetInventoryData(configObjects);
            var result = _sut.Convert(arsenalData);

            //Assert
            result.Count.Should().Be(255);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_configFilePath))
            {
                File.Delete(_configFilePath);
            }
        }
    }
    
}