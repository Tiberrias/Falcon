using System;
using System.Collections.Generic;
using System.Linq;
using ArmaConfigParser.ConfigModel;
using Falcon.Core.Converters.Interfaces;
using Falcon.Core.Model.Loadouts;
using Falcon.Core.Services.Extensions;

namespace Falcon.Core.Converters
{
    public class DataClassToLoadoutListConverter : IDataClassToLoadoutListConverter
    {
        public List<Loadout> Convert(DataClass inventoryData)
        {
            var result = new List<Loadout>();
            if (inventoryData == null || inventoryData.DataType != ConfigDataType.Array ||
                (inventoryData.Value as List<ConfigObject>).OfType<ItemClass>().Count() % 2 != 0)
            {
                return result;
            }

            var currentItemIsNameItem = true;
            string currentLoadoutName = null;
            foreach (var item in (inventoryData.Value as List<ConfigObject>).OfType<ItemClass>())
            {
                if (currentItemIsNameItem)
                {
                    currentLoadoutName = GetLoadoutName(item);
                }
                else
                {
                    var loadout = new Loadout
                    {
                        Name = currentLoadoutName,
                        ManEquipment = GetSingleEquipment(item).StackContainerItems()
                    };
                    result.Add(loadout);
                    currentLoadoutName = null;
                }
                currentItemIsNameItem = !currentItemIsNameItem;
            }
            return result;
        }

        private string GetLoadoutName(ItemClass nameItem)
        {
            return nameItem.Data.Value.ToString();
        }

        private ManEquipment GetSingleEquipment(ItemClass dataItem)
        {
            var arsenalItemsList = (dataItem.Data.Value as List<ConfigObject>).OfType<ItemClass>().ToList();

            var eq = new ManEquipment();

            eq.Uniform = GetContainer(arsenalItemsList[0]);
            eq.Vest = GetContainer(arsenalItemsList[1]);
            eq.Backpack = GetContainer(arsenalItemsList[2]);

            eq.Headgear = GetItem(arsenalItemsList[3]);
            eq.Goggles = GetItem(arsenalItemsList[4]);
            eq.Binocular = GetItem(arsenalItemsList[5]);

            eq.PrimaryWeapon = GetWeapon(arsenalItemsList[6]);
            eq.SecondaryWeapon = GetWeapon(arsenalItemsList[7]);
            eq.Sidearm = GetWeapon(arsenalItemsList[8]);

            eq.AssignedItems = GetItems(arsenalItemsList[9]);

            return eq;
        }

        private Container GetContainer(ItemClass containerDataItem)
        {
            var resultContainer = new Container();

            var innerData = (containerDataItem.Data.Value as List<ConfigObject>).OfType<ItemClass>();

            resultContainer.Classname = innerData.First(item => item.Data.DataType == ConfigDataType.String).Data.Value.ToString();
            if (!String.IsNullOrEmpty(resultContainer.Classname))
            {
                resultContainer.Items = GetItems(innerData.First(item => item.Data.DataType == ConfigDataType.Array));
            }
            else
            {
                return null;
            }

            return resultContainer;
        }

        private List<Item> GetItems(ItemClass itemsDataItem)
        {
            if (itemsDataItem.Data.Value == null)
            {
                return new List<Item>();
            }
            return 
                (itemsDataItem.Data.Value as List<ConfigObject>).OfType<ItemClass>()
                .Where(item => !String.IsNullOrEmpty(item.Data.Value as string))
                .Select(result => new Item {Classname = result.Data.Value as string})
                .ToList();
        }

        private Item GetItem(ItemClass itemDataItem)
        {
            var classname = itemDataItem.Data.Value as string;
            if (String.IsNullOrEmpty(classname))
            {
                return null;
            }
            return new Item {Classname = classname};
        }

        private Weapon GetWeapon(ItemClass weaponDataItem)
        {
            var resultWeapon = new Weapon();

            var innerData = (weaponDataItem.Data.Value as List<ConfigObject>).OfType<ItemClass>();

            resultWeapon.Classname = innerData.First(item => item.Data.DataType == ConfigDataType.String).Data.Value.ToString();

            if (!String.IsNullOrEmpty(resultWeapon.Classname))
            {
                resultWeapon.Attachments = GetItems(innerData.First(item => item.Data.DataType == ConfigDataType.Array));

                resultWeapon.Magazine = GetItem(innerData.Last(item => item.Data.DataType == ConfigDataType.String));
            }
            else
            {
                return null;
            }

            return resultWeapon;
        }
    }
}