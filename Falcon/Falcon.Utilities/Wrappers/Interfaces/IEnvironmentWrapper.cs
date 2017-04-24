namespace Falcon.Utilities.Wrappers.Interfaces
{
    public interface IEnvironmentWrapper
    {
        string ExpandEnvironmentVariables(string name);
    }
}