using System.Configuration;
using System.IO;
using ArmaConfigParser.ConfigReader;
using ArmaConfigParser.Wrapper;
using ArmaConfigParserTest.Properties;
using NUnit.Framework;

namespace ArmaConfigParserTest.IntegrationTests
{
    [TestFixture(Category = "Integration")]
    public class FileDebinarizationTests
    {
        private ProcessWrapper _processWrapper;
        private FileWrapper _fileWrapper;
        private ConfigDebinarizer _configDebinarizer;

        private string _configFilePath;
        private string _debinarizedFilePath;

        [SetUp]
        public void SetUp()
        {
            _fileWrapper = new FileWrapper();
            _processWrapper = new ProcessWrapper();
            _configDebinarizer = new ConfigDebinarizer(_processWrapper, _fileWrapper);

            _configDebinarizer.Initialize(ConfigurationManager.AppSettings["ArmaToolsCfgConvertPath"]);

            _configFilePath = Path.GetTempFileName();
            _debinarizedFilePath = Path.GetTempFileName();
        }

        [Test]
        public void ArmaConfigDebinarizer_ValidFile_DebinarizesFileAndSavesCorrectly()
        {
            //Arrange
            File.WriteAllBytes(_configFilePath, Resources.test_vars);

            //Act
            _configDebinarizer.DebinarizeConfigFile(_configFilePath, _debinarizedFilePath);

            //Assert
            FileAssert.Exists(_debinarizedFilePath);
            Assert.AreEqual(432973, File.ReadAllLines(_debinarizedFilePath).Length);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_configFilePath))
            {
                File.Delete(_configFilePath);
            }
            if (File.Exists(_debinarizedFilePath))
            {
                File.Delete(_configFilePath);
            }
        }

    }
}