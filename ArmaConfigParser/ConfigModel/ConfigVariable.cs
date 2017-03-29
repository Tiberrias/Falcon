namespace ArmaConfigParser.ConfigModel
{
    public class ConfigVariable : ConfigObject
    {
        public string Name { get; set; }

        public object Value { get; set; }
    }
}