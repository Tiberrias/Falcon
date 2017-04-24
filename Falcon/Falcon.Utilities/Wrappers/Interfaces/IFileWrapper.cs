namespace Falcon.Utilities.Wrappers.Interfaces
{
    public interface IFileWrapper
    {
        bool Exists(string path);

        string ReadAllText(string path);
    }
}