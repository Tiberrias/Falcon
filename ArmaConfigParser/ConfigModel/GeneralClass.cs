using System.Collections.Generic;

namespace ArmaConfigParser.ConfigModel
{
    public class GeneralClass : ConfigObject
    {
        public string ClassName { get; set; }

        public List<ConfigObject> Content { get; set; }
    }
}