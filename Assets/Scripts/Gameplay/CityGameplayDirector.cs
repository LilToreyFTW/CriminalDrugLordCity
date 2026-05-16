using CriminalDrugLordCity.Content;
using UnityEngine;

namespace CriminalDrugLordCity.Gameplay
{
    public sealed class CityGameplayDirector : MonoBehaviour
    {
        [SerializeField] private string currentObjective = "Reach the arsenal room and grab a weapon.";
        [SerializeField] private string districtName = "Metro City Block";
        [SerializeField] private bool showDebugOverlay = true;

        private void OnGUI()
        {
            if (!showDebugOverlay)
            {
                return;
            }

            GUI.Box(new Rect(16f, 16f, 420f, 150f), "Criminal Drug Lord City");
            GUI.Label(new Rect(28f, 46f, 380f, 24f), "District: " + districtName);
            GUI.Label(new Rect(28f, 70f, 380f, 24f), "Objective: " + currentObjective);

            string weaponText = "Unarmed";
            if (RuntimeGameState.PlayerState != null && !string.IsNullOrEmpty(RuntimeGameState.PlayerState.EquippedWeapon))
            {
                weaponText = RuntimeGameState.PlayerState.EquippedWeapon;
            }

            GUI.Label(new Rect(28f, 94f, 380f, 24f), "Weapon: " + weaponText);

            int dlcCount = RuntimeGameState.SeasonalDlcs != null ? RuntimeGameState.SeasonalDlcs.Length : 0;
            GUI.Label(new Rect(28f, 118f, 380f, 24f), "Loaded DLC: " + dlcCount);
        }
    }
}
