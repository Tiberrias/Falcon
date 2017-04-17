namespace ArmaConfigParser.Wrapper.Interfaces
{
    public interface IFileWrapper
    {
        bool Exists(string path);

        string ReadAllText(string path);
    }
}