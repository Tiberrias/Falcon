namespace ArmaConfigParser.ConfigReader
{
    public interface IConfigDebinarizer
    {
        void Initialize(string cfgConverterFilePath);

        void DebinarizeConfigFile(string sourceFilePath, string destinationFilePath);
    }
}