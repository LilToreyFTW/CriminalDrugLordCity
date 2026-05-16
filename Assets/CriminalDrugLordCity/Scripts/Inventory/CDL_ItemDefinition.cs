using UnityEngine;

namespace CriminalDrugLordCity.CDL.Inventory
{
    [CreateAssetMenu(menuName = "CDL/Inventory/Item Definition", fileName = "CDL_Item_")]
    public class CDL_ItemDefinition : ScriptableObject
    {
        public string itemId;
        public string itemName;
        [TextArea] public string description;
        public Sprite icon;
        public bool stackable = true;
        public int maxStack = 99;
        public bool isWeapon;
    }
}
