using CriminalDrugLordCity.Content;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CriminalDrugLordCity.Gameplay
{
    public sealed class CrimHUD : MonoBehaviour
    {
        public static CrimHUD Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void UpdateMissionText()
        {
            if (MissionManager.Instance != null && MissionManager.Instance.activeMission != null)
            {
                if (missionTitleText != null) missionTitleText.text = MissionManager.Instance.activeMission.title;
                var currentObjective = MissionManager.Instance.activeMission.objectives.Find(o => !o.isCompleted);
                if (objectiveDescText != null && currentObjective != null) 
                    objectiveDescText.text = currentObjective.description;
            }
        }
[Header("UI References")]
        public TextMeshProUGUI cashText;
        public TextMeshProUGUI weaponText;
        public TextMeshProUGUI respectText;
        public TextMeshProUGUI wantedText;
        public Image healthBar;

        [Header("Mission UI")]
        public TextMeshProUGUI missionTitleText;
        public TextMeshProUGUI objectiveDescText;

        private void Update()
        {
            if (RuntimeGameState.PlayerState == null) return;

            var state = RuntimeGameState.PlayerState;
            
            if (cashText != null) cashText.text = "$ " + state.Cash.ToString("N0");
            if (weaponText != null) weaponText.text = "WEAPON: " + (string.IsNullOrEmpty(state.EquippedWeapon) ? "NONE" : state.EquippedWeapon);
            if (respectText != null) respectText.text = "RESPECT: " + state.Respect;
            if (healthBar != null) healthBar.fillAmount = state.Health / 100f;

            // Mission Display
            if (MissionManager.Instance != null && MissionManager.Instance.activeMission != null)
            {
                if (missionTitleText != null) missionTitleText.text = MissionManager.Instance.activeMission.title;
                var currentObjective = MissionManager.Instance.activeMission.objectives.Find(o => !o.isCompleted);
                if (objectiveDescText != null && currentObjective != null) 
                    objectiveDescText.text = currentObjective.description;
            }

            if (wantedText != null)
{
                wantedText.text = WantedSystem.WantedLevel > 0 ? "WANTED: " + new string('*', WantedSystem.WantedLevel) : "";
                wantedText.color = Color.yellow;
            }
        }

        private void OnGUI()
        {
            if (wantedText == null && WantedSystem.WantedLevel > 0)
            {
                GUI.color = Color.red;
                GUI.Label(new Rect(Screen.width / 2 - 50, 50, 200, 30), "WANTED: " + new string('*', WantedSystem.WantedLevel));
            }
        }

        public static void CreateHUD(GameObject canvasPrefab)
        {
            if (canvasPrefab != null)
            {
                Object.Instantiate(canvasPrefab);
            }
            else
            {
                // Simple IMGUI fallback if no prefab provided, but we'll try to use a real one
                GameObject go = new GameObject("HUD_Controller");
                go.AddComponent<CrimHUD>();
            }
        }
    }
}
