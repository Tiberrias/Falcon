using System.IO;
using ArmaConfigParser.ConfigModel;
using ArmaConfigParser.ConfigReader;
using ArmaConfigParser.Configuration;
using ArmaConfigParser.Modules;
using Falcon.Tests.Configuration;
using Falcon.Tests.Properties;
using Ninject;
using NUnit.Framework;

namespace Falcon.Tests.Falcon.Core.Integration
{
    [TestFixture(Category = "Integration")]
    public class LoadoutExtractionTests
    {
        private ConfigExtractionService _sut;
        private string _configFilePath;

        [SetUp]
        public void SetUp()
        {
            StandardKernel kernel = new StandardKernel(new ArmaConfigParserNinjectModule());
            kernel.Bind<IConfigurationService>().To<ConfigurationServiceTestImplementation>();

            _sut = kernel.Get<ConfigExtractionService>();
        }

        [Ignore("Integration tests offline")]
        [Test]
        public void Extract_BinarizedFileOnDisc_ReturnsConfigObjectData()
        {
            //Arrange
            _configFilePath = Path.GetTempFileName();
            File.WriteAllBytes(_configFilePath, Resources.test_vars);

            //Act
            var result = _sut.Extract(_configFilePath);

            //Assert
            Assert.AreEqual(result.Count, 2);
            Assert.AreEqual((result[1] as GeneralClass).Content.Count, 595);
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