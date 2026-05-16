using System;
using System.Collections.Generic;
using UnityEngine;

namespace CriminalDrugLordCity.Gameplay
{
    [Serializable]
    public enum ObjectiveType
    {
        ReachLocation,
        EliminateTarget,
        CollectItem,
        DeliverPackage
    }

    [Serializable]
    public class MissionObjective
    {
        public string description;
        public ObjectiveType type;
        public Vector3 targetPosition;
        public string targetId;
        public bool isCompleted;
    }

    [Serializable]
    public class MissionDefinition
    {
        public string id;
        public string title;
        public string description;
        public List<MissionObjective> objectives = new List<MissionObjective>();
        public int cashReward;
        public int respectReward;
        public string targetMapId;
        public bool isCompleted;
    }

    public class MissionManager : MonoBehaviour
    {
        public static MissionManager Instance { get; private set; }

        public List<MissionDefinition> availableMissions = new List<MissionDefinition>();
        public MissionDefinition activeMission;

        private Transform _player;

        public void Start()
        {
            LoadMissions();
            _player = GameObject.FindWithTag("Player")?.transform;
            if (availableMissions.Count > 0) StartMission(availableMissions[0].id);
        }

        private void Update()
        {
            if (activeMission == null || _player == null) return;

            for (int i = 0; i < activeMission.objectives.Count; i++)
            {
                var obj = activeMission.objectives[i];
                if (obj.isCompleted) continue;

                if (obj.type == ObjectiveType.ReachLocation)
                {
                    if (Vector3.Distance(_player.position, obj.targetPosition) < 5f)
                    {
                        CompleteObjective(i);
                    }
                }
            }
        }

        private void LoadMissions()
        {
            string path = System.IO.Path.Combine(Application.streamingAssetsPath, "missions.json");
            if (System.IO.File.Exists(path))
            {
                string json = System.IO.File.ReadAllText(path);
                // Simple workaround since JsonUtility doesn't handle top-level arrays well without a wrapper
                // Or I can use a wrapper class
                MissionList wrapper = JsonUtility.FromJson<MissionList>("{\"missions\":" + json + "}");
                availableMissions = wrapper.missions;
            }
        }

        [Serializable]
        public class MissionList { public List<MissionDefinition> missions; }

        public void StartMission(string missionId)
        {
            activeMission = availableMissions.Find(m => m.id == missionId);
            if (activeMission != null)
            {
                Debug.Log("Mission Started: " + activeMission.title);
                // Update HUD objective via GameplayDirector if needed
            }
        }

        public void CompleteObjective(int index)
        {
            if (activeMission == null || index >= activeMission.objectives.Count) return;
            activeMission.objectives[index].isCompleted = true;
            
            // HUD Update
            if (CrimHUD.Instance != null) CrimHUD.Instance.UpdateMissionText();
            
            CheckMissionCompletion();
        }

        private void CheckMissionCompletion()
        {
            if (activeMission.objectives.TrueForAll(o => o.isCompleted))
            {
                activeMission.isCompleted = true;
                RuntimeGameState.PlayerState.Cash += activeMission.cashReward;
                RuntimeGameState.PlayerState.Respect += activeMission.respectReward;
                
                // Add Empire XP
                if (ProgressionManager.Instance != null) 
                    ProgressionManager.Instance.AddXP(activeMission.respectReward * 10);

                Debug.Log("Mission Completed: " + activeMission.title);
                activeMission = null;
                
                // Start next mission if available
                StartMissionByIndex(availableMissions.FindIndex(m => !m.isCompleted));
            }
        }

        private void StartMissionByIndex(int index)
        {
            if (index >= 0 && index < availableMissions.Count)
            {
                activeMission = availableMissions[index];
                if (activeMission != null)
                {
                    Debug.Log("Next Mission Started: " + activeMission.title);
                    if (PhoneManager.Instance != null) 
                        PhoneManager.Instance.TravelTo(activeMission.targetMapId);
                }
            }
        }
}
}
