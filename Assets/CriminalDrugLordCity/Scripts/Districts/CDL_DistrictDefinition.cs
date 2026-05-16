using UnityEngine;

namespace CriminalDrugLordCity.CDL.Districts
{
    [CreateAssetMenu(menuName = "CDL/Districts/District Definition", fileName = "CDL_District_")]
    public class CDL_DistrictDefinition : ScriptableObject
    {
        public string districtId;
        public string districtName;
        public float demandMultiplier = 1f;
        public float policePresence = 0.3f;
        public float rivalPresence = 0.3f;
        public float customerDensity = 0.5f;
        public int dealerSlots = 2;
        public float districtControlValue = 0f;
    }
}
