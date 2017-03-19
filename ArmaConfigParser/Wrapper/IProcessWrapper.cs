using System.Diagnostics;

namespace ArmaConfigParser.Wrapper
{
    public interface IProcessWrapper
    {
        ProcessStartInfo StartInfo { get; set; }

        bool Start();
    }
}