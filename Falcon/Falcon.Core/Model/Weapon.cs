using System.Collections.Generic;

namespace Falcon.Core.Model
{
    public class Weapon
    {
        public string Classname { get; set; }

        public List<Item> Attachments { get; set; }

        public Item Magazine { get; set; }
    }
}