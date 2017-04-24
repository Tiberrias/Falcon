using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Falcon.Utilities.Wrappers.Interfaces;

namespace Falcon.Utilities.Wrappers
{
    [ExcludeFromCodeCoverage]
    public class ProcessWrapper : IProcessWrapper
    {
        private readonly Process _process;

        public ProcessWrapper()
        {
            _process = new Process();
        }

        public ProcessStartInfo StartInfo
        {
            get { return _process.StartInfo; }
            set { _process.StartInfo = value; }
        }

        public bool Start()
        {
            return _process.Start();
        }

        public bool WaitForExit(int millisecondsTimeout)
        {
            return _process.WaitForExit(millisecondsTimeout);
        }
    }
}