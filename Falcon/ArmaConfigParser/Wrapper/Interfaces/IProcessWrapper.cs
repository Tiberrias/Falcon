using System.Diagnostics;

namespace ArmaConfigParser.Wrapper.Interfaces
{
    public interface IProcessWrapper
    {
        ProcessStartInfo StartInfo { get; set; }

        bool Start();

        bool WaitForExit(int millisecondsTimeout);
    }
}