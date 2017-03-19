using System;
using System.IO;
using ArmaConfigParser.Wrapper;

namespace ArmaConfigParser.ConfigReader
{
    public class ConfigDebinarizer : IConfigDebinarizer
    {
        private string _cfgConverterFilePath;
        private readonly IProcessWrapper _process;
        private readonly IFileWrapper _file;

        public ConfigDebinarizer(IProcessWrapper process, IFileWrapper file)
        {
            _process = process;
            _file = file;
        }

        public void Initialize(string cfgConverterFilePath)
        {
            if (string.IsNullOrWhiteSpace(cfgConverterFilePath) || !_file.Exists(cfgConverterFilePath))
            {
                throw new ArgumentException("Invalid converter tool file path");
            }

            _cfgConverterFilePath = cfgConverterFilePath;
        }

        public void DebinarizeConfigFile(string sourceFilePath, string destinationFilePath)
        {
            if (string.IsNullOrWhiteSpace(sourceFilePath) || !_file.Exists(sourceFilePath))
            {
                throw new ArgumentException("Invalid binarized config file source path");
            }
            if (string.IsNullOrWhiteSpace(destinationFilePath))
            {
                throw new ArgumentException("Invalid debinarized config file destination path");
            }
            
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
            {
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = $@"/C {_cfgConverterFilePath} -bin -dst {sourceFilePath} {destinationFilePath}"
            };
            _process.StartInfo = startInfo;
            _process.Start();
        }
    }
}