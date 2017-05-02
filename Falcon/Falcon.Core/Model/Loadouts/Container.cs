using System.Collections.Generic;

namespace Falcon.Core.Model.Loadouts
{
    public class Container
    {
        public string Classname { get; set; }

        public List<Item> Items { get; set; }
    }
}