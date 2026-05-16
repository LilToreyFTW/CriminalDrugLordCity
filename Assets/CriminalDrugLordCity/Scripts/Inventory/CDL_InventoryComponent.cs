using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CriminalDrugLordCity.CDL.Inventory
{
    public class CDL_InventoryComponent : MonoBehaviour
    {
        public List<CDL_InventorySlot> slots = new();
        public int slotLimit = 24;

        private void Awake()
        {
            foreach (var slot in slots)
            {
                CDL_InventoryDatabase.Register(slot.item);
            }
        }

        public bool HasItem(CDL_ItemDefinition item, int amount)
        {
            return item != null && slots.Where(s => s.item == item).Sum(s => s.quantity) >= amount;
        }

        public bool AddItem(CDL_ItemDefinition item, int amount)
        {
            if (item == null || amount <= 0)
            {
                return false;
            }

            CDL_InventoryDatabase.Register(item);

            if (item.stackable)
            {
                foreach (var slot in slots)
                {
                    if (slot.item == item && slot.quantity < item.maxStack)
                    {
                        int transfer = Mathf.Min(amount, item.maxStack - slot.quantity);
                        slot.quantity += transfer;
                        amount -= transfer;
                        if (amount <= 0)
                        {
                            return true;
                        }
                    }
                }
            }

            while (amount > 0 && slots.Count < slotLimit)
            {
                int stackAmount = item.stackable ? Mathf.Min(amount, item.maxStack) : 1;
                slots.Add(new CDL_InventorySlot { item = item, quantity = stackAmount });
                amount -= stackAmount;
            }

            return amount <= 0;
        }

        public bool RemoveItem(CDL_ItemDefinition item, int amount)
        {
            if (!HasItem(item, amount))
            {
                return false;
            }

            for (int i = slots.Count - 1; i >= 0 && amount > 0; i--)
            {
                if (slots[i].item != item)
                {
                    continue;
                }

                int take = Mathf.Min(amount, slots[i].quantity);
                slots[i].quantity -= take;
                amount -= take;
                if (slots[i].quantity <= 0)
                {
                    slots.RemoveAt(i);
                }
            }

            return true;
        }

        public List<CDL_InventoryEntryData> BuildSave()
        {
            return slots.Select(s => new CDL_InventoryEntryData
            {
                itemId = s.item != null ? s.item.itemId : string.Empty,
                quantity = s.quantity
            }).ToList();
        }

        public void ApplySave(List<CDL_InventoryEntryData> data)
        {
            slots.Clear();
            if (data == null)
            {
                return;
            }

            foreach (var entry in data)
            {
                var item = CDL_InventoryDatabase.Get(entry.itemId);
                if (item != null)
                {
                    slots.Add(new CDL_InventorySlot { item = item, quantity = entry.quantity });
                }
            }
        }
    }
}
