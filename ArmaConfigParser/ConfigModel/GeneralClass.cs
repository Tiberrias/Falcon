using System.Collections.Generic;

namespace ArmaConfigParser.ConfigModel
{
    public class GeneralClass : ConfigObject
    {
        public List<ConfigObject> Content { get; set; }
    }
}