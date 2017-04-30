using System.Collections.Generic;
using System.IO;
using System.Linq;
using Falcon.Core.Configuration;
using Falcon.Core.Services.Interfaces;
using Falcon.Utilities.Wrappers.Interfaces;

namespace Falcon.Core.Services
{
    public class VirtualArsenalFilesLocatorService : IVirtualArsenalFilesLocatorService
    {
        private readonly IDirectoryWrapper _directoryWrapper;
        private readonly IEnvironmentWrapper _environmentWrapper;
        private readonly IFileWrapper _fileWrapper;
        private readonly IConfigurationService _configurationService;

        public VirtualArsenalFilesLocatorService(IConfigurationService configurationService, IDirectoryWrapper directoryWrapper, IEnvironmentWrapper environmentWrapper, IFileWrapper fileWrapper)
        {
            _configurationService = configurationService;
            _directoryWrapper = directoryWrapper;
            _environmentWrapper = environmentWrapper;
            _fileWrapper = fileWrapper;
        }

        public List<string> GetPossibleConfigVarsFilepaths()
        {
            if (!string.IsNullOrEmpty(_configurationService.CustomProfileVarsFileSearchLocation))
            {
                var customProfilePath = _environmentWrapper.ExpandEnvironmentVariables(_configurationService.CustomProfileVarsFileSearchLocation);

                if (_fileWrapper.Exists(customProfilePath))
                {
                    var attributes = _fileWrapper.GetAttributes(customProfilePath);
                    if (!attributes.HasFlag(FileAttributes.Directory))
                    {
                        return new List<string> { customProfilePath };
                    }
                    var files = _directoryWrapper.GetFiles(customProfilePath);
                    return files.Where(x => x.EndsWith(".vars.Arma3Profile")).ToList();
                }
            }

            var possibleConfigVarsPaths = new List<string>();

            var defaultProfilePath = _environmentWrapper.ExpandEnvironmentVariables(_configurationService.DefaultProfileVarsFileSearchLocation);
            var defaultPathFiles = _directoryWrapper.GetFiles(defaultProfilePath).Where(x => x.EndsWith(".vars.Arma3Profile")).ToList();

            possibleConfigVarsPaths.AddRange(defaultPathFiles);

            var otherProfilePath = _environmentWrapper.ExpandEnvironmentVariables(_configurationService.OtherProfilesVarsFileSearchLocation);
            var otherProfileDirectories = _directoryWrapper.GetDirectories(otherProfilePath).ToList();

            otherProfileDirectories.ForEach(
                directory =>
                    possibleConfigVarsPaths.AddRange(
                        _directoryWrapper.GetFiles(directory)
                        .Where(x => x.EndsWith(".vars.Arma3Profile")).ToList()));

            return possibleConfigVarsPaths;
        }
    }
}