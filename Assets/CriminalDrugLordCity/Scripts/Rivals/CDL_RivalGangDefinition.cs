using UnityEngine;

namespace CriminalDrugLordCity.CDL.Rivals
{
    [CreateAssetMenu(menuName = "CDL/Rivals/Rival Gang Definition", fileName = "CDL_RivalGang_")]
    public class CDL_RivalGangDefinition : ScriptableObject
    {
        public string rivalId;
        public string gangName;
        [Range(0f, 1f)] public float aggression = 0.5f;
        [Range(0f, 1f)] public float territoryGrip = 0.5f;
        [Range(0f, 1f)] public float ambushChance = 0.2f;
    }
}
