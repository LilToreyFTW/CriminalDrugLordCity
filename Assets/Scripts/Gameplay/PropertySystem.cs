using System;
using System.Collections.Generic;
using UnityEngine;

namespace CriminalDrugLordCity.Gameplay
{
    public enum PropertyType { Apartment, Garage, Warehouse, Storefront, Farm, LuxuryHouse }

    [Serializable]
    public class PropertyUpgrade
    {
        public string id;
        public string name;
        public int cost;
        public bool isInstalled;
    }

    public class Property : MonoBehaviour
    {
        public string propertyId;
        public PropertyType type;
        public int baseValue;
        public bool isOwned;
        public float suspicionLevel;
        public List<PropertyUpgrade> upgrades = new List<PropertyUpgrade>();

        public void Purchase()
        {
            if (RuntimeGameState.PlayerState.Cash >= baseValue)
            {
                RuntimeGameState.PlayerState.Cash -= baseValue;
                isOwned = true;
            }
        }
    }

    public class PropertyManager : MonoBehaviour
    {
        public static PropertyManager Instance { get; private set; }
        public List<Property> allProperties = new List<Property>();

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }
    }
}
