using UnityEngine;

namespace CriminalDrugLordCity.CDL.Dealers
{
    [CreateAssetMenu(menuName = "CDL/Dealers/Dealer Profile", fileName = "CDL_DealerProfile_")]
    public class CDL_DealerProfile : ScriptableObject
    {
        public string dealerId;
        public string dealerName;
        [Range(0f, 1f)] public float loyalty = 0.5f;
        [Range(0f, 1f)] public float risk = 0.5f;
        [Range(0f, 1f)] public float skill = 0.5f;
    }
}
