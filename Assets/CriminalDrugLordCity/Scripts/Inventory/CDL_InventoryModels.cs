using System;
using System.Collections.Generic;

namespace CriminalDrugLordCity.CDL.Inventory
{
    [Serializable]
    public class CDL_InventorySlot
    {
        public CDL_ItemDefinition item;
        public int quantity;
    }

    [Serializable]
    public class CDL_InventoryEntryData
    {
        public string itemId;
        public int quantity;
    }

    public static class CDL_InventoryDatabase
    {
        private static readonly Dictionary<string, CDL_ItemDefinition> Cache = new();

        public static void Register(CDL_ItemDefinition item)
        {
            if (item != null && !string.IsNullOrWhiteSpace(item.itemId))
            {
                Cache[item.itemId] = item;
            }
        }

        public static CDL_ItemDefinition Get(string itemId)
        {
            Cache.TryGetValue(itemId, out var item);
            return item;
        }
    }
}
