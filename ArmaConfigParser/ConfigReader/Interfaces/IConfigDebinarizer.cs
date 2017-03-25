namespace ArmaConfigParser.ConfigReader.Interfaces
{
    public interface IConfigDebinarizer
    {
        void Initialize(string cfgConverterFilePath);

        void DebinarizeConfigFile(string sourceFilePath, string destinationFilePath);
    }
}