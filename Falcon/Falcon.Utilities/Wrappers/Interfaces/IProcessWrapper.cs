using System.Diagnostics;

namespace Falcon.Utilities.Wrappers.Interfaces
{
    public interface IProcessWrapper
    {
        ProcessStartInfo StartInfo { get; set; }

        bool Start();

        bool WaitForExit(int millisecondsTimeout);
    }
}