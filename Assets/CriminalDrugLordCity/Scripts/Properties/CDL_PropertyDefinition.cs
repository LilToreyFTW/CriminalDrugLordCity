using UnityEngine;

namespace CriminalDrugLordCity.CDL.Properties
{
    [CreateAssetMenu(menuName = "CDL/Properties/Property Definition", fileName = "CDL_Property_")]
    public class CDL_PropertyDefinition : ScriptableObject
    {
        public string propertyId;
        public string propertyName;
        public float purchasePrice = 1000f;
        public int storageCapacity = 24;
        public int securityLevel = 1;
        public int productionSlots = 1;
        public int upgradeLevelMax = 3;
    }
}
