using CriminalDrugLordCity.CDL.Core;
using CriminalDrugLordCity.CDL.Inventory;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CriminalDrugLordCity.CDL.Save
{
    [Serializable]
    public class CDL_PropertySaveData
    {
        public string propertyId;
        public bool owned;
        public int upgradeLevel;
    }

    [Serializable]
    public class CDL_DealerSaveData
    {
        public string dealerId;
        public bool hired;
    }

    [Serializable]
    public class CDL_SaveData
    {
        public float money;
        public float reputation;
        public float heat;
        public List<CDL_InventoryEntryData> inventory = new();
        public List<CDL_PropertySaveData> properties = new();
        public List<CDL_DealerSaveData> dealers = new();
        public List<string> employees = new();
    }

    public class CDL_SaveSystem : MonoBehaviour
    {
        public string fileName = "cdl_prototype_save.json";

        private string SavePath => Path.Combine(Application.persistentDataPath, fileName);

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F5))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.F9))
            {
                Load();
            }
        }

        public void Save()
        {
            string json = JsonUtility.ToJson(CDL_GameManager.Instance.BuildSaveData(), true);
            File.WriteAllText(SavePath, json);
            CDL_GameManager.Instance.PushMessage($"Saved to {SavePath}");
        }

        public void Load()
        {
            if (!File.Exists(SavePath))
            {
                CDL_GameManager.Instance.PushMessage("No save found.");
                return;
            }

            var data = JsonUtility.FromJson<CDL_SaveData>(File.ReadAllText(SavePath));
            CDL_GameManager.Instance.ApplySaveData(data);
            CDL_GameManager.Instance.PushMessage("Save loaded.");
        }
    }
}
