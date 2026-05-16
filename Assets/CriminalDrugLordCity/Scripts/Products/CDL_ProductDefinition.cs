using UnityEngine;

namespace CriminalDrugLordCity.CDL.Products
{
    public enum CDL_ProductRarity { Common, Uncommon, Rare, Elite }
    public enum CDL_ProductQuality { Low, Standard, High, Premium }

    [CreateAssetMenu(menuName = "CDL/Products/Product Definition", fileName = "CDL_Product_")]
    public class CDL_ProductDefinition : ScriptableObject
    {
        public string productId;
        public string productName;
        public CDL_ProductRarity rarity;
        public CDL_ProductQuality quality;
        public float basePrice = 100f;
        public float heatRisk = 3f;
        public float demandScore = 1f;
        public Sprite icon;
    }
}
