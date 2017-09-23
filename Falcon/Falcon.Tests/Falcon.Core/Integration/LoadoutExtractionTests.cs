using System;
using System.Diagnostics;
using System.IO;
using ArmaConfigParser.ConfigReader;
using ArmaConfigParser.Configuration;
using ArmaConfigParser.Modules;
using Falcon.Core.Builder;
using Falcon.Core.Converters;
using Falcon.Core.Modules;
using Falcon.Core.Services;
using Falcon.Tests.Configuration;
using Falcon.Tests.Properties;
using Falcon.Utilities.Modules;
using Ninject;
using NUnit.Framework;

namespace Falcon.Tests.Falcon.Core.Integration
{
    [TestFixture(Category = "Integration")]
    public class LoadoutExtractionTests
    {
        private ConfigExtractionService _configExtractionService;
        private ArsenalEquipmentExtractionService _arsenalEquipmentExtractionService;
        private DataClassToLoadoutListConverter _sut;
        private string _configFilePath;
        private GearBuilder _builder;

        [SetUp]
        public void SetUp()
        {
            StandardKernel kernel = new StandardKernel(new ArmaConfigParserNinjectModule(), new FalconCoreNinjectModule(), new UtilitiesNinjectModule());
            kernel.Bind<IConfigurationService>().To<ConfigurationServiceTestImplementation>();

            _configExtractionService = kernel.Get<ConfigExtractionService>();
            _arsenalEquipmentExtractionService = kernel.Get<ArsenalEquipmentExtractionService>();
            _sut = kernel.Get<DataClassToLoadoutListConverter>();
            _builder = new GearBuilder();
        }

        [Test]
        public void ExtractGetConvert_BinarizedFileOnDisc_ReturnsListOfConvertedLoadouts()
        {
            var stopwatch = new Stopwatch();

            //Arrange
            _configFilePath = Path.GetTempFileName();
            File.WriteAllBytes(_configFilePath, Resources.test_vars);

            //Act
            stopwatch.Start();
            var configObjects = _configExtractionService.Extract(_configFilePath);

            var parsingTime = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();

            var arsenalData = _arsenalEquipmentExtractionService.GetInventoryData(configObjects);

            var extractionTime = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();

            var convertedData = _sut.Convert(arsenalData);

            var conversionTime = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();

            var result = _builder.BuildUnitgear(convertedData);

            var unitgearTime = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();

            //Assert
            Assert.NotNull(result);
            Console.WriteLine($@"{parsingTime}, {extractionTime}, {conversionTime}, {unitgearTime}");
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