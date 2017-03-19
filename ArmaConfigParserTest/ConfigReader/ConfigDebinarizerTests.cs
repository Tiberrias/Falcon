using System;
using System.Diagnostics;
using ArmaConfigParser.ConfigReader;
using ArmaConfigParser.Wrapper;
using Moq;
using NUnit.Framework;

namespace ArmaConfigParserTest.ConfigReader
{
    [TestFixture]
    public class ConfigDebinarizerTests
    {
        private ConfigDebinarizer _configDebinarizer;
        private Mock<IProcessWrapper> _processWrapper;
        private Mock<IFileWrapper> _fileWrapper;

        [SetUp]
        public void SetUp()
        {
            _processWrapper = new Mock<IProcessWrapper>();
            _fileWrapper = new Mock<IFileWrapper>();
            _configDebinarizer = new ConfigDebinarizer(_processWrapper.Object, _fileWrapper.Object);
        }

        [Test]
        public void Initialize_EmptyFilePath_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => _configDebinarizer.Initialize(string.Empty));
        }

        [Test]
        public void Initialize_NotExistingFilePath_ThrowsException()
        {
            //Arrange
            _fileWrapper.Setup(x => x.Exists(It.IsAny<string>())).Returns(false);

            //Act/Assert
            Assert.Throws<ArgumentException>(() => _configDebinarizer.Initialize(@"C:\CoolPath\cfgConvert.exe"));
        }

        [Test]
        public void Initialize_GoodFilePath_DoesNotThrowException()
        {
            //Arrange
            _fileWrapper.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);

            //Act/Assert
            Assert.DoesNotThrow(() => _configDebinarizer.Initialize(@"C:\CoolPath\cfgConvert.exe"));
        }

        [Test]
        public void DebinarizeConfigFile_EmptySourcePath_ThrowsException()
        {
            //Arrange
            _fileWrapper.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);

            //Act/Assert
            Assert.Throws<ArgumentException>(() => _configDebinarizer.DebinarizeConfigFile(string.Empty, @"C:\CoolPath\debinarized.cpp"));
        }

        [Test]
        public void DebinarizeConfigFile_NotExistingSourcePath_ThrowsException()
        {
            //Arrange
            _fileWrapper.Setup(x => x.Exists(@"C:\CoolPath\config.bin")).Returns(false);

            //Act/Assert
            Assert.Throws<ArgumentException>(() => _configDebinarizer.DebinarizeConfigFile(@"C:\CoolPath\config.bin", @"C:\CoolPath\debinarized.cpp"));
        }

        [Test]
        public void DebinarizeConfigFile_EmptyDestinationPath_ThrowsException()
        {
            //Arrange
            _fileWrapper.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);

            //Act/Assert
            Assert.Throws<ArgumentException>(() => _configDebinarizer.DebinarizeConfigFile(@"C:\CoolPath\config.bin", string.Empty));
        }

        [Test]
        public void DebinarizeConfigFile_ValidPaths_StartsProcess()
        {
            //Arrange
            _fileWrapper.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);

            //Act
            _configDebinarizer.DebinarizeConfigFile(@"C:\CoolPath\config.bin", @"C:\CoolPath\debinarized.cpp");

            //Assert
            _processWrapper.Verify(x => x.Start(), Times.Once);
        }

        [Test]
        public void DebinarizeConfigFile_ValidPaths_SetsExpectedProcessStartInfo()
        {
            //Arrange
            _fileWrapper.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
            _processWrapper.SetupProperty(x => x.StartInfo);
            var expectedProcessStartInfo = new ProcessStartInfo()
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = @"/C ""C:\CoolPath\cfgConvert.exe"" -txt -dst ""C:\CoolPath\debinarized.cpp"" ""C:\CoolPath\config.bin"""
            };
            _configDebinarizer.Initialize(@"C:\CoolPath\cfgConvert.exe");
            
            //Act
            _configDebinarizer.DebinarizeConfigFile(@"C:\CoolPath\config.bin", @"C:\CoolPath\debinarized.cpp");

            //Assert
            Assert.AreEqual(expectedProcessStartInfo.Arguments, _processWrapper.Object.StartInfo.Arguments);
            Assert.AreEqual(expectedProcessStartInfo.FileName, _processWrapper.Object.StartInfo.FileName);
            Assert.AreEqual(expectedProcessStartInfo.WindowStyle, _processWrapper.Object.StartInfo.WindowStyle);
        }
    }
}