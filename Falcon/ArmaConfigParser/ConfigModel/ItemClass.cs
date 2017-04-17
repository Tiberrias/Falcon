namespace ArmaConfigParser.ConfigModel
{
    public class ItemClass : ConfigObject
    {
        public string Name { get; set; }

        public DataClass Data { get; set; }

        public bool? ReadOnly { get; set; }
    }
}