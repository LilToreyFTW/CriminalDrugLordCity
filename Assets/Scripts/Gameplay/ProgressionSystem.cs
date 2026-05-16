using System;
using UnityEngine;

namespace CriminalDrugLordCity.Gameplay
{
    public class ProgressionManager : MonoBehaviour
    {
        public static ProgressionManager Instance { get; private set; }

        public int empireLevel = 1;
        public int currentXP;
        public int xpToNextLevel = 1000;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void AddXP(int amount)
        {
            currentXP += amount;
            if (currentXP >= xpToNextLevel) LevelUp();
        }

        private void LevelUp()
        {
            empireLevel++;
            currentXP -= xpToNextLevel;
            xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.5f);
            Debug.Log("Empire Leveled Up: " + empireLevel);
        }
    }
}
