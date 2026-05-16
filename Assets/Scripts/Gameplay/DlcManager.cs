using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CriminalDrugLordCity.Content;

namespace CriminalDrugLordCity.Gameplay
{
    public class DlcManager : MonoBehaviour
    {
public static DlcManager Instance { get; private set; }

        public List<DlcDefinition> activeDlcs = new List<DlcDefinition>();

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void ActivateDlc(string dlcId)
        {
            // Implementation...
        }

        public bool IsMapUnlocked(string mapId)
        {
            if (mapId == "metro-city") return true; 
            foreach (var dlc in activeDlcs)
            {
                if (dlc.addsMaps.Contains(mapId)) return true;
            }
            return false;
        }

        public List<string> GetUnlockedWeapons()
        {
            List<string> weapons = new List<string> { "Street Pistol", "Corner SMG", "Raid Shotgun" };
            foreach (var dlc in activeDlcs)
            {
                weapons.AddRange(dlc.addsWeapons);
            }
            return weapons;
        }
    }
}
