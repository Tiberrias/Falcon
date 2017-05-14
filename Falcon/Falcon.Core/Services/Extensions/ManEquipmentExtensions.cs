using System;
using System.Collections.Generic;
using System.Linq;
using Falcon.Core.Model.Loadouts;

namespace Falcon.Core.Services.Extensions
{
    public static class ManEquipmentExtensions
    {
        public static ManEquipment StackContainerItems(this ManEquipment manEquipment)
        {
            manEquipment.Uniform?.StackContainerItems();
            manEquipment.Vest?.StackContainerItems();
            manEquipment.Backpack?.StackContainerItems();

            return manEquipment;
        }

        public static ManEquipment UnstackContainersItems(this ManEquipment manEquipment)
        {
            throw new NotImplementedException();  
        }

        private static void StackContainerItems(this Container container)
        {
            var resultList = new List<Item>();  
            foreach (var uniformItem in container.Items)
            {
                var matchingItem = resultList.FirstOrDefault(item => item.Classname == uniformItem.Classname);
                if (matchingItem == default(Item))
                {
                    resultList.Add(new ItemStack()
                    {
                        Classname = uniformItem.Classname,
                        Count = 1
                    });
                }
                else
                {
                    ((ItemStack)matchingItem).Count++;
                }
            }
            container.Items = resultList;
        }
    }
}