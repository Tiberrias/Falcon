namespace Falcon.Utilities.Wrappers.Interfaces
{
    public interface IDirectoryWrapper
    {
        string[] GetFiles(string path);

        string[] GetDirectories(string path);
    }
}