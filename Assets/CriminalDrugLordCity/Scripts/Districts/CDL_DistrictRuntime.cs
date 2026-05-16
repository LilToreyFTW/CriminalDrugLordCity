using CriminalDrugLordCity.CDL.Core;
using UnityEngine;

namespace CriminalDrugLordCity.CDL.Districts
{
    public class CDL_DistrictRuntime : MonoBehaviour
    {
        public CDL_DistrictDefinition definition;
        public float currentDemand;
        public float currentPolicePresence;
        public float currentRivalPresence;
        public float control;

        private void Start()
        {
            if (definition != null)
            {
                currentDemand = definition.demandMultiplier;
                currentPolicePresence = definition.policePresence;
                currentRivalPresence = definition.rivalPresence;
                control = definition.districtControlValue;
            }

            CDL_GameManager.Instance?.RegisterDistrict(this);
        }
    }
}
