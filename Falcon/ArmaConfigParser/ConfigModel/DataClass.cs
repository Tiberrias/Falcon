namespace ArmaConfigParser.ConfigModel
{
    public class DataClass : ConfigObject
    {
        public ConfigDataType DataType { get; set; }

        public object Value { get; set; }
    }
}