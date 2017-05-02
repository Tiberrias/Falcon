using System.Collections.Generic;

namespace Falcon.Core.Model.Loadouts
{
    public class ManEquipment
    {
        public Item Headgear { get; set; }

        public Item Goggles { get; set; }

        public Item Binocular { get; set; }

        public List<Item> AssignedItems { get; set; }

        public Container Uniform { get; set; }

        public Container Vest { get; set; }

        public Container Backpack { get; set; }

        public Weapon PrimaryWeapon { get; set; }

        public Weapon Sidearm { get; set; }

        public Weapon SecondaryWeapon { get; set; }
    }
}