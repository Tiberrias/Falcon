using System;
using Falcon.Utilities.Wrappers.Interfaces;

namespace Falcon.Utilities.Wrappers
{
    public class EnvironmentWrapper : IEnvironmentWrapper
    {
        public string ExpandEnvironmentVariables(string name)
        {
            return Environment.ExpandEnvironmentVariables(name);
        }
    }
}