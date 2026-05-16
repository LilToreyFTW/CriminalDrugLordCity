using System;
using System.Collections.Generic;
using UnityEngine;

namespace CriminalDrugLordCity.Gameplay
{
    [Serializable]
    public enum ProductRarity { Common, Rare, Experimental, Legendary }

    [Serializable]
    public class ProductStats
    {
        public float quality;
        public float purity;
        public float risk;
        public float demand;
        public Color productLabelColor = Color.white;
    }

    [Serializable]
    public class ProductInstance
    {
        public string productId;
        public string customName;
        public ProductRarity rarity;
        public ProductStats stats;
        public int quantity;
    }

    public class PlayerInventory : MonoBehaviour
    {
        public static PlayerInventory Instance { get; private set; }
        public List<ProductInstance> products = new List<ProductInstance>();
        public List<string> unlockedRecipes = new List<string>();

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void AddProduct(ProductInstance product)
        {
            var existing = products.Find(p => p.productId == product.productId && p.customName == product.customName);
            if (existing != null) existing.quantity += product.quantity;
            else products.Add(product);
        }

        public bool RemoveProduct(string productId, int amount)
        {
            var existing = products.Find(p => p.productId == productId);
            if (existing != null && existing.quantity >= amount)
            {
                existing.quantity -= amount;
                if (existing.quantity <= 0) products.Remove(existing);
                return true;
            }
            return false;
        }
    }
}
