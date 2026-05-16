using CriminalDrugLordCity.CDL.Inventory;
using System;
using UnityEngine;

namespace CriminalDrugLordCity.CDL.Suppliers
{
    [Serializable]
    public class CDL_SupplierStockEntry
    {
        public CDL_ItemDefinition item;
        public int stock = 10;
        public float price = 50f;
    }

    [CreateAssetMenu(menuName = "CDL/Suppliers/Supplier Definition", fileName = "CDL_Supplier_")]
    public class CDL_SupplierDefinition : ScriptableObject
    {
        public string supplierId;
        public string supplierName;
        [Range(0f, 1f)] public float reputation = 0.5f;
        public float bulkDiscountThreshold = 5f;
        public float bulkDiscountPercent = 0.15f;
        public float restockSeconds = 60f;
        public CDL_SupplierStockEntry[] stock;
    }
}
