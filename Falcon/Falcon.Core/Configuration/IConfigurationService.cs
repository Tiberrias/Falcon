using System.Collections.Generic;

namespace Falcon.Core.Configuration
{
    public interface IConfigurationService
    {
        string DefaultProfileVarsFileSearchLocation { get; }

        string OtherProfilesVarsFileSearchLocation { get; }

        string CustomProfileVarsFileSearchLocation { get; }
    }   
}