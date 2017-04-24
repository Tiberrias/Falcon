﻿using System;
using System.Diagnostics;
using ArmaConfigParser.ConfigReader;
using ArmaConfigParser.Configuration;
using Falcon.Utilities.Wrappers.Interfaces;
using Moq;
using NUnit.Framework;

namespace Falcon.Tests.ArmaConfigParser.ConfigReader
{
    [TestFixture]
    public class ConfigDebinarizerTests
    {
        private ConfigDebinarizer _configDebinarizer;
        private Mock<IProcessWrapper> _processWrapper;
        private Mock<IFileWrapper> _fileWrapper;
        private Mock<IConfigurationService> _configurationService;

        [SetUp]
        public void SetUp()
        {
            _processWrapper = new Mock<IProcessWrapper>();
            _fileWrapper = new Mock<IFileWrapper>();
            _configurationService = new Mock<IConfigurationService>();
            _configDebinarizer = new ConfigDebinarizer(_processWrapper.Object, _fileWrapper.Object, _configurationService.Object);
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
        public void DebinarizeConfigFile_ProcessTookTooLong_ThrowsTimeoutException()
        {
            //Arrange
            _fileWrapper.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
            _processWrapper.Setup(x => x.WaitForExit(It.IsAny<int>())).Returns(false);

            //Act/Assert
            Assert.Throws<TimeoutException>(() => _configDebinarizer.DebinarizeConfigFile(@"C:\CoolPath\config.bin", @"C:\CoolPath\debinarized.cpp"));
        }

        [Test]
        public void DebinarizeConfigFile_ValidPaths_StartsProcess()
        {
            //Arrange
            _fileWrapper.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
            _processWrapper.Setup(x => x.WaitForExit(It.IsAny<int>())).Returns(true);

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
            _processWrapper.Setup(x => x.WaitForExit(It.IsAny<int>())).Returns(true);
            var expectedProcessStartInfo = new ProcessStartInfo()
            {
                WindowStyle = ProcessWindowStyle.Normal,
                FileName = @"C:\CoolPath\cfgConvert.exe",
                Arguments = @" -txt -dst ""C:\CoolPath\debinarized.cpp"" ""C:\CoolPath\config.bin"""
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