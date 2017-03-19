using System;
using System.IO;

namespace ArmaConfigParser.ConfigReader
{
    public class ConfigDebinarizer : IConfigDebinarizer
    {
        private string _cfgConverterFilePath;

        public void Initialize(string cfgConverterFilePath)
        {
            if (string.IsNullOrWhiteSpace(cfgConverterFilePath) || !File.Exists(cfgConverterFilePath))
            {
                throw new ArgumentException("Invalid converter tool file path");
            }

            _cfgConverterFilePath = cfgConverterFilePath;
        }

        public void DebinarizeConfigFile(string sourceFilePath, string destinationFilePath)
        {
            if (string.IsNullOrWhiteSpace(sourceFilePath) || !File.Exists(sourceFilePath))
            {
                throw new ArgumentException("Invalid binarized config file source path");
            }
            if (string.IsNullOrWhiteSpace(destinationFilePath))
            {
                throw new ArgumentException("Invalid debinarized config file destination path");
            }

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
            {
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = $@"/C {_cfgConverterFilePath} -bin -dst {sourceFilePath} {destinationFilePath}"
            };
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}